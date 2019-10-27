using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    [SerializeField] GameObject gameManager;

    [Header("Sections")]
    [SerializeField] GameObject intro;
    [SerializeField] GameObject about;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject upgrades;

    [Header("Setting Icons")]
    [SerializeField] Button music;
    [SerializeField] Sprite musicOn;
    [SerializeField] Sprite musicOff;
    [SerializeField] Button sound;
    [SerializeField] Sprite soundOn;
    [SerializeField] Sprite soundOff;

    private bool MusicOn = true;
    private bool SoundOn = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btn(string name)
    {
        if (name == "About")
        {
            intro.SetActive(false);
            about.SetActive(true);
        }
        else if (name == "Settings")
        {
            intro.SetActive(false);
            settings.SetActive(true);
        }
        else if (name == "Tutorial")
        {
            gameManager.GetComponent<GameManager>().tutorial();
        }
        else if (name == "Upgrades")
        {
            intro.SetActive(false);
            upgrades.SetActive(true);
        }
        else if (name == "ToggleMusic")
        {
            if (MusicOn)
            {
                gameManager.GetComponent<GameManager>().button("MusicOff");
                music.GetComponent<Image>().sprite = musicOff;
                MusicOn = false;
            }
            else
            {
                gameManager.GetComponent<GameManager>().button("MusicOn");
                music.GetComponent<Image>().sprite = musicOn;
                MusicOn = true;
            }
        }
        else if (name == "ToggleSound")
        {
            if (SoundOn)
            {
                gameManager.GetComponent<GameManager>().button("SoundOff");
                sound.GetComponent<Image>().sprite = soundOff;
                SoundOn = false;
            }
            else
            {
                gameManager.GetComponent<GameManager>().button("SoundOn");
                sound.GetComponent<Image>().sprite = soundOn;
                SoundOn = true;
            }
        }
        else if (name == "Back")
        {
            if (intro.activeSelf)
            {
                gameManager.GetComponent<GameManager>().button("Back5");
            }
            else if (about.activeSelf)
            {
                about.SetActive(false);
                intro.SetActive(true);
            }
            else if (settings.activeSelf)
            {
                intro.SetActive(true);
                settings.SetActive(false);
            }
            else if (upgrades.activeSelf)
            {
                intro.SetActive(true);
                upgrades.SetActive(false);
            }
        }
        gameManager.GetComponent<GameManager>().playsound("button");
    }
}
