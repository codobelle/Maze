using UnityEngine;

public class PlayerDirection : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
        if (Time.timeScale != 0)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}
