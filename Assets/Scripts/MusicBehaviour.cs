using UnityEngine;
using UnityEngine.UI;

public class MusicBehaviour : MonoBehaviour {

    public static bool isMusicOn;

    [SerializeField]
    private GameObject musicCheckbox;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = isMusicOn;
        musicCheckbox.GetComponent<Toggle>().isOn = isMusicOn;
    }
	
    public void SoundState(bool isOn)
    {
        isMusicOn = isOn;
        audioSource.mute = isOn;
    }
}
