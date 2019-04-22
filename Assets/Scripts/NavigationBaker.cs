using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    public NavMeshSurface surface;

    // Use this for initialization
    void Start()
    {
        surface.BuildNavMesh();
    }
}
