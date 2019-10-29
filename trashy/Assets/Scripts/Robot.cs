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

    private enum State
    {
        Intro, About, Settings, Upgrades
    }

    private State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Intro;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Intro)
        {
            intro.SetActive(true);
            about.SetActive(false);
            settings.SetActive(false);
            upgrades.SetActive(false);
        }
        else if (state == State.About)
        {
            intro.SetActive(false);
            about.SetActive(true);
            settings.SetActive(false);
            upgrades.SetActive(false);
        }
        else if (state == State.Settings)
        {
            intro.SetActive(false);
            about.SetActive(false);
            settings.SetActive(true);
            upgrades.SetActive(false);
        }
        else if (state == State.Upgrades)
        {
            intro.SetActive(false);
            about.SetActive(false);
            settings.SetActive(false);
            upgrades.SetActive(true);
        }
    }

    public void btn(string name)
    {
        if (name == "About")
        {
            state = State.About;
        }
        else if (name == "Settings")
        {
            state = State.Settings;
        }
        else if (name == "Tutorial")
        {
            gameManager.GetComponent<GameManager>().tutorial();
        }
        else if (name == "Upgrades")
        {
            state = State.Upgrades;
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
            if (state == State.Intro)
            {
                gameManager.GetComponent<GameManager>().button("Back5");
            }
            else if (state == State.About)
            {
                state = State.Intro;
            }
            else if (state == State.Settings)
            {
                state = State.Intro;
            }
            else if (state == State.Upgrades)
            {
                state = State.Intro;
            }
        }
        gameManager.GetComponent<GameManager>().playsound("button");
    }
}
