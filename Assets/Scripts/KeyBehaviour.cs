using UnityEngine;
using UnityEngine.UI;

public class KeyBehaviour : MonoBehaviour {
    [SerializeField]
    private AudioClip keyFound;

    private GameObject keyUsageMessage;

	// Use this for initialization
	void Start () {
        keyUsageMessage = GameManager.Instance.keyPanel;
        keyUsageMessage.SetActive(false);
    }
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            keyUsageMessage.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(keyFound, transform.position, 1);
            GameManager.Instance.keyPickedUpImage.GetComponent<Image>().enabled = true;
            PlayerController.playerHasKey = true;
            keyUsageMessage.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            keyUsageMessage.SetActive(false);
        }
    }
}
