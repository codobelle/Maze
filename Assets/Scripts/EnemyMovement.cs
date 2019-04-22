using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    private NavMeshAgent agent;
    private Vector3 freeMazePath;
    private Vector3 targetPosition, startPosition;
    private GameObject player;
    private float minDistanceToTarget = 0.5f, minDistanceToPlayer = 2, maxDistanceFromPlayer = 3;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        freeMazePath = MazeGenerator.freeMazePath[Random.Range(0, MazeGenerator.freeMazePath.Count)];
        startPosition = transform.position;
        targetPosition = new Vector3(freeMazePath.x, 0, freeMazePath.y);
        agent.SetDestination(targetPosition);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Vector3.Distance(transform.position, targetPosition) < minDistanceToTarget)
        {
            agent.SetDestination(startPosition);
        }

        if (Vector3.Distance(transform.position, startPosition) < minDistanceToTarget)
        {
            agent.SetDestination(targetPosition);
        }

        if (Vector3.Distance(transform.position, player.transform.position) < minDistanceToPlayer && transform.tag == "Enemy1")
        {
            agent.SetDestination(player.transform.position);
        }

        if (agent.velocity == Vector3.zero && transform.tag == "Enemy1")
        {
            agent.SetDestination(startPosition);
        }
    }
}
