using UnityEngine;

public class BulletManager : MonoBehaviour {
    
    private GameObject bullet;
    private int bulletSpeed = 6;

    private void Start()
    {
        bullet = gameObject.transform.GetChild(0).gameObject;
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * bulletSpeed;
        }
    }
}
