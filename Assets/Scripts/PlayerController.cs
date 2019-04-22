using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public static float stamina = 1f;
    public static bool playerHasKey = false;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        if (!Camera.main.orthographic && (x!=0 || z != 0))
        {
            rigidBody.MovePosition(rigidBody.position + Camera.main.transform.forward * z);
            rigidBody.MovePosition(rigidBody.position + Camera.main.transform.right * x);
        }
        else
        {
            if (x != 0 || z != 0)
            {
                rigidBody.MovePosition(new Vector3(rigidBody.position.x + x, 0, rigidBody.position.z + z));
            }
        }
            
    }

    private void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        
        //stamina system 
        if (Input.GetKey(KeyCode.LeftShift) && (z != 0 || x != 0))
        {
            if (stamina > 0)
            {
                speed = 3;
                stamina -= Time.deltaTime * 0.5f;
            }
            else
            {
                speed = 1;
            }
        }
        else
        {
            speed = 1;
            if (stamina < 1)
            {
                stamina += 0.1f * Time.deltaTime;
            }
        }
    }
}