using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    private GameObject playerBars;
    private float playerHealth = 1f;
    private Image playerHealthBar, staminaBar;
    private float damage = 0.25f;

    private void Start()
    {
        playerBars = Instantiate(GameManager.Instance.playerBarsPrefab, GameObject.Find("Canvas").transform);
        playerBars.transform.SetSiblingIndex(0);
        staminaBar = playerBars.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>();
        playerHealthBar = playerBars.transform.GetChild(1).GetChild(0).GetComponentInChildren<Image>();
    }
    // Update is called once per frame
    private void Update () {

        staminaBar.fillAmount = PlayerController.stamina;

        if (playerHealth == 0)
        {
            PlayerPreference.playerIsDead = true;
        }

        if (Camera.main.orthographic)
        {
            Vector3 wantedPos = Camera.main.WorldToScreenPoint(transform.position);
            playerBars.transform.position = wantedPos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (collision.gameObject.tag == "Enemy1" || collision.gameObject.tag == "Enemy2")
        {
            playerHealth -= damage;
            playerHealthBar.fillAmount = playerHealth;
        }
    }

}
