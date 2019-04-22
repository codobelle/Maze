using UnityEngine;
using UnityEngine.UI;

public class DoorBehaviour : MonoBehaviour {

    [SerializeField]
    private AudioClip usingKey;
    private GameObject keyPanel;

    // Use this for initialization
    void Start()
    {
        keyPanel = GameManager.Instance.doorPanel;
        keyPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerController.playerHasKey)
        {
            keyPanel.SetActive(true);
            keyPanel.GetComponentInChildren<Text>().text = "Door \n Press[e] to use \n the key";
        }
        else
        {
            keyPanel.SetActive(true);
            keyPanel.GetComponentInChildren<Text>().text = "Door \n Find Key";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (PlayerController.playerHasKey)
        {
            keyPanel.SetActive(true);
        }
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) && PlayerController.playerHasKey)
        {
            AudioSource.PlayClipAtPoint(usingKey, transform.position, 1);
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            GameManager.Instance.keyPickedUpImage.GetComponent<Image>().enabled = false;
            keyPanel.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            keyPanel.SetActive(false);
        }
    }
}
