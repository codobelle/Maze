using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    
    private GameObject enemyHealthBar;
    private Image enemyHealthBarFill;
    private Vector3 wantedPos;
    private float enemyHealth = 1, damage = 0.25f;
    private RendererStatus rendererStatus; 

    void Start()
    {
        enemyHealthBar = Instantiate(GameManager.Instance.enemyHealthBarPrefab);
        enemyHealthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        enemyHealthBar.transform.SetSiblingIndex(0);
        enemyHealthBarFill = enemyHealthBar.transform.GetChild(0).GetComponent<Image>();
        rendererStatus = transform.GetChild(0).GetChild(0).GetComponent<RendererStatus>();
        rendererStatus.enemyHealthBar = enemyHealthBar;
        if (!Camera.main.orthographic) { enemyHealthBar.SetActive(false); }
    }

    // Update is called once per frame
    void Update ()
    {
        if (enemyHealthBar != null)
        {
            wantedPos = Camera.main.WorldToScreenPoint(transform.position);
            enemyHealthBar.transform.position = wantedPos;
        }

        if (enemyHealth == 0)
        {
            Destroy(gameObject);
            Destroy(enemyHealthBar);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            enemyHealth -= damage;
            enemyHealthBarFill.fillAmount = enemyHealth;
        }
    }
}
