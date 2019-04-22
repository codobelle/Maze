using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererStatus : MonoBehaviour {

    [HideInInspector]
    public GameObject enemyHealthBar;
    private Renderer rend;
    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        //if (rend.isVisible)
        //{
        //    enemyHealthBar.SetActive(true);
        //}
        //else
        //{
        //    enemyHealthBar.SetActive(false);
        //}
        RaycastHit hit;
        // Calculate Ray direction
        Vector3 direction = Camera.main.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider.tag == "MainCamera") //hit something else before the camera
            {
                enemyHealthBar.SetActive(true);
            }
        }
    }
}
