using UnityEngine;

public class Finish : MonoBehaviour {

    private GameObject congratulationPanel;

    private void Start()
    {
        congratulationPanel = GameManager.Instance.congratulationPanel;
        congratulationPanel.SetActive(false);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Cursor.visible = true;
            congratulationPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
