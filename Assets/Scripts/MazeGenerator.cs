using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.AI;

public class MazeGenerator : MonoBehaviour
{
    public int size;
    public static List<Vector3> freeMazePath;
    [SerializeField]
    private GameObject plane, key, player, door;
    [SerializeField]
    private GameObject[] stones;
    [SerializeField]
    private GameObject enemy1, enemy2;
    [SerializeField]
    private Transform parent;

    private Transform playerTransform;
    private int width, height;
    private int[,] maze;
    private List<Vector3> pathMazes = new List<Vector3>();
    private Stack<Vector2> visitedCell = new Stack<Vector2>();
    private List<Vector2> offsets = new List<Vector2> { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
    private System.Random rnd = new System.Random();
    private Vector2 currentTile;

    public Vector2 CurrentTile
    {
        get { return currentTile; }
        private set
        {
            if (value.x < 1 || value.x >= this.width - 1 || value.y < 1 || value.y >= this.height - 1)
            {
                throw new ArgumentException("CurrentTile must be within the one tile border all around the maze");
            }
            if (value.x % 2 == 1 || value.y % 2 == 1)
            { currentTile = value; }
            else
            {
                throw new ArgumentException("The current square must not be both on an even X-axis and an even Y-axis, to ensure we can get walls around all tunnels");
            }
        }
    }

    public void Done()
    {
        GameObject planeInstance = Instantiate(plane, new Vector3((float)size/2.0f - 0.5f, 0.05f, (float)size/2.0f - 0.5f), Quaternion.identity, transform);
        planeInstance.transform.localScale = new Vector3((float)size / 10.0f, 1, (float)size / 10.0f);
        player = Instantiate(player, new Vector3(1, .5f, 1), Quaternion.identity);
        Instantiate(door, new Vector3(size - 2, 0, size - 1), Quaternion.identity, transform);
        if (!Camera.main.orthographic)
        {
            player.GetComponent<MouseLook>().enabled = true;
            player.GetComponentInChildren<PlayerDirection>().enabled = false;
            Cursor.visible = false;
            playerTransform = player.transform;
            Camera.main.transform.SetParent(playerTransform);
            Camera.main.transform.position = new Vector3(1, .5f, 1);
        }
        else
        {
            playerTransform = player.transform;
            player.GetComponent<MouseLook>().enabled = false;
            player.GetComponentInChildren<PlayerDirection>().enabled = true;
            Camera.main.transform.SetParent(playerTransform);
            Camera.main.transform.eulerAngles = new Vector3(90, 0, 0);
            Camera.main.transform.position = new Vector3(1, Camera.main.transform.position.y, 1);
            Camera.main.GetComponent<MouseLook>().enabled = false;
        }
        width = size;
        height = size;
        GenerateMaze();
    }
    

    public int[,] CreateMaze()
    {
        //local variable to store neighbors to the current square
        //as we work our way through the maze
        List<Vector2> neighbors;
        //as long as there are still tiles to try
        while (visitedCell.Count > 0)
        {
            //excavate the square we are on
            maze[(int)CurrentTile.x, (int)CurrentTile.y] = 0;

            //get all valid neighbors for the new tile
            neighbors = GetValidNeighbors(CurrentTile);

            //if there are any interesting looking neighbors
            if (neighbors.Count > 0)
            {
                //remember this tile, by putting it on the stack
                visitedCell.Push(CurrentTile);
                //move on to a random of the neighboring tiles
                CurrentTile = neighbors[rnd.Next(neighbors.Count)];
            }
            else
            {
                //if there were no neighbors to try, we are at a dead-end
                //toss this tile out
                //(thereby returning to a previous tile in the list to check).
                CurrentTile = visitedCell.Pop();
            }
        }

        return maze;
    }

    private void GenerateMaze()
    {
        maze = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1;
            }
        }
        CurrentTile = Vector2.one;
        visitedCell.Push(CurrentTile);
        CreateMaze();
        GameObject ptype = null;
        for (int i = 0; i <= maze.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= maze.GetUpperBound(1); j++)
            {
                if (maze[i, j] == 1)
                {
                    ptype = stones[0];
                    Vector3 stonePosition = new Vector3(i * ptype.transform.localScale.x, 0, j * ptype.transform.localScale.y);
                    GameObject wall = Instantiate(
                            stones[UnityEngine.Random.Range(0, stones.Length)],
                            stonePosition,
                            Quaternion.Euler(0, 0, 0), parent);

                    if (wall.transform.position.x == (size - 2) && wall.transform.position.z == (size - 1))
                    {
                        Destroy(wall);
                    }
                    if (wall.transform.position.x == (size - 2) && wall.transform.position.z == (size - 2))
                    {
                        Destroy(wall);
                    }
                }
                else if (maze[i, j] == 0)
                {
                    pathMazes.Add(new Vector3(i, j, 0));
                }
            }
        }
        freeMazePath = pathMazes;
        Vector3 enemyInstancePosition1 = new Vector3(pathMazes[UnityEngine.Random.Range(10, pathMazes.Count)].x, 0,
            pathMazes[UnityEngine.Random.Range(10, pathMazes.Count)].y);
        GameObject enemyInstance1 = Instantiate(enemy1);
        enemyInstance1.GetComponent<NavMeshAgent>().Warp(enemyInstancePosition1);

        Vector3 enemyInstancePosition2 = new Vector3(pathMazes[UnityEngine.Random.Range(10, pathMazes.Count)].x, 0,
            pathMazes[UnityEngine.Random.Range(10, pathMazes.Count)].y);
        GameObject enemyInstance2 = Instantiate(enemy2);
        enemyInstance2.GetComponent<NavMeshAgent>().Warp(enemyInstancePosition2);

        Instantiate(key, new Vector3(pathMazes[pathMazes.Count / 3].x * key.transform.localScale.x, 0.3f,
            pathMazes[pathMazes.Count / 3].y * key.transform.localScale.y),
            Quaternion.Euler(-90, 0, 0), parent);
    }

    /// <summary>
    /// Get all the prospective neighboring tiles
    /// </summary>
    /// <param name="centerTile">The tile to test</param>
    /// <returns>All and any valid neighbors</returns>
    private List<Vector2> GetValidNeighbors(Vector2 centerTile)
    {

        List<Vector2> validNeighbors = new List<Vector2>();

        //Check all four directions around the tile
        foreach (var offset in offsets)
        {
            //find the neighbor's position
            Vector2 toCheck = new Vector2(centerTile.x + offset.x, centerTile.y + offset.y);

            //make sure the tile is not on both an even X-axis and an even Y-axis
            //to ensure we can get walls around all tunnels
            if (toCheck.x % 2 == 1 || toCheck.y % 2 == 1)
            {
                //if the potential neighbor is unexcavated (==1)
                //and still has three walls intact (new territory)
                if (maze[(int)toCheck.x, (int)toCheck.y] == 1 && HasThreeWallsIntact(toCheck))
                        {
                    //add the neighbor
                    validNeighbors.Add(toCheck);
                }
            }
        }

        return validNeighbors;
    }


    /// <summary>
    /// Counts the number of intact walls around a tile
    /// </summary>
    /// <param name="Vector2ToCheck">The coordinates of the tile to check</param>
    /// <returns>Whether there are three intact walls (the tile has not been dug into earlier.</returns>
    private bool HasThreeWallsIntact(Vector2 Vector2ToCheck)
    {
        int intactWallCounter = 0;

        //Check all four directions around the tile
        foreach (var offset in offsets)
        {
            //find the neighbor's position
            Vector2 neighborToCheck = new Vector2(Vector2ToCheck.x + offset.x, Vector2ToCheck.y + offset.y);

            //make sure it is inside the maze, and it hasn't been dug out yet
            if (IsInside(neighborToCheck) && maze[(int)neighborToCheck.x, (int)neighborToCheck.y] == 1)
                    {
                intactWallCounter++;
            }
        }

        //tell whether three walls are intact
        return intactWallCounter == 3;

    }

    private bool IsInside(Vector2 p)
    {
        return p.x >= 0 && p.y >= 0 && p.x < width && p.y < height;
    }
}