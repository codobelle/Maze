using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPreference : MonoBehaviour {

    public GameObject panel;
    public ToggleGroup tGroup;
    public Slider slider;
    public MazeGenerator mg;
    public Text sliderValueText;
    public GameObject settingsPanel;
    public GameObject losePanel;
    public GameObject settingsButton;
    public GameObject aboutControls;

    private int sliderValue = 10;
    private string toogleName;

    static public bool playerIsDead = false;

    // Use this for initialization
    void Start () {
        GetActiveToggle();
        slider.minValue = 10;
        slider.maxValue = 50;
        settingsPanel.SetActive(false);
        losePanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            settingsPanel.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0.0f;
        }

        if (playerIsDead)
        {
            playerIsDead = false;
            settingsButton.SetActive(false);
            losePanel.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.visible = true;
        }
    }
    
    public void Play()
    {
        playerIsDead = false;
        Time.timeScale = 1.0f;
        if (toogleName == "isOrthographic")
        {
            settingsButton.SetActive(true);
            Camera.main.orthographic = true;
        }
        else
        {
            Camera.main.orthographic = false;
            settingsButton.SetActive(false);
        }
        panel.SetActive(false);
        losePanel.SetActive(false);
        mg.size = sliderValue;
    }

    public void GetActiveToggle()
    {
        foreach (var item in tGroup.ActiveToggles())
        {
            toogleName = item.name;
            break;
        }
    }

    public void SetSliderValue()
    {
        sliderValue = (int)slider.value;
        sliderValueText.text = sliderValue.ToString();
    }

    public void PlayAgain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        Time.timeScale = 0.0f;
        settingsPanel.SetActive(true);
        Cursor.visible = true;
    }

    public void CloseSettings()
    {
        if (!Camera.main.orthographic)
        {
            Cursor.visible = false;
        }
        Time.timeScale = 1.0f;
        settingsPanel.SetActive(false);
    }

    public void OpenAboutControls()
    {
        aboutControls.SetActive(true);
    }
    public void CloseAboutControls()
    {
        aboutControls.SetActive(false);
    }
}
