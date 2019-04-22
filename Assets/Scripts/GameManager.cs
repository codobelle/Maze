using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject doorPanel, keyPanel, congratulationPanel, keyPickedUpImage;
    public GameObject playerBarsPrefab, enemyHealthBarPrefab;
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
