using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Sections")]
    [SerializeField] GameObject cutscene;
    [SerializeField] GameObject main;
    [SerializeField] GameObject sort;
    [SerializeField] GameObject score;
    [SerializeField] GameObject guide;
    [SerializeField] GameObject map;
    [SerializeField] GameObject help;
    [SerializeField] GameObject robot;

    [Header("Cutscenes")]
    [SerializeField] GameObject cutsceneChar;
    [SerializeField] Text cutsceneCharName;
    [SerializeField] Text cutsceneDialogue;
    [SerializeField] GameObject cutsceneDialogueBg;
    [SerializeField] Text cutsceneTap2Continue;
    [SerializeField] GameObject cutsceneBg;
    [SerializeField] GameObject touch;
    [SerializeField] GameObject touch2;

    [Header("Main")]
    [SerializeField] GameObject mainBg;
    [SerializeField] GameObject guideSilh;
    [SerializeField] GameObject mapSilh;
    [SerializeField] GameObject CBOISilh;
    [SerializeField] GameObject TrashSilh;
    [SerializeField] GameObject Stars;
    [SerializeField] GameObject HelpBtn;

    [Header("Music")]
    [SerializeField] AudioSource musicource;
    [SerializeField] AudioClip bgmainclip;
    [SerializeField] AudioClip bgsortclip;
    [SerializeField] AudioClip bgmenuclip;

    [Header("Sound")]
    [SerializeField] AudioSource sfxsource;
    [SerializeField] AudioClip buttonsound;
    [SerializeField] AudioClip goodsound;
    [SerializeField] AudioClip badsound;
    [SerializeField] AudioClip completesound;
    [SerializeField] AudioClip pagesound;

    private bool tapped = false;
    private string btn = "";
    private int intro = 0;

    // Start is called before the first frame update
    void Start()
    {
        //set everything to inactive (none of them should be active, it will cause things to break)
        cutscene.SetActive(false);
        main.SetActive(false);
        sort.SetActive(false);
        score.SetActive(false);
        guide.SetActive(false);
        map.SetActive(false);
        help.SetActive(false);
        robot.SetActive(false);

        if (PlayerPrefs.GetInt("hasPlayed", 0) == 1)
        {
            main.SetActive(true);
            cutscene.SetActive(true);
            cutsceneChar.GetComponent<SpriteRenderer>().flipX = true;
            cutsceneChar.transform.localScale = new Vector3(10.95f, 10.95f, 0f);
            cutsceneChar.transform.position = new Vector3(130.67f, 140.2f, 0f);
        }
        else
        {
            PlayerPrefs.SetInt("hasPlayed", 1);

            StartCoroutine(introduction());
            PlayerPrefs.SetInt("stars", 100);
        }

        //AUDIO
        musicource.clip = bgmainclip;
        if (!musicource.isPlaying)
        {
            musicource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("stars", 0) / 10 < 10)
        {
            Stars.GetComponent<TextMeshProUGUI>().text = "0000" + PlayerPrefs.GetInt("stars", 0);
        }
        else if (PlayerPrefs.GetInt("stars", 0) / 100 < 10)
        {
            Stars.GetComponent<TextMeshProUGUI>().text = "000" + PlayerPrefs.GetInt("stars", 0);
        }
        else if (PlayerPrefs.GetInt("stars", 0) / 1000 < 10)
        {
            Stars.GetComponent<TextMeshProUGUI>().text = "00" + PlayerPrefs.GetInt("stars", 0);
        }
        else if (PlayerPrefs.GetInt("stars", 0) / 10000 < 10)
        {
            Stars.GetComponent<TextMeshProUGUI>().text = "0" + PlayerPrefs.GetInt("stars", 0);
        }
        else
        {
            Stars.GetComponent<TextMeshProUGUI>().text = "" + PlayerPrefs.GetInt("stars", 0);
        }
    }

    public void tap()
    {
        tapped = true;
    }

    public void button(string btn)
    {
        this.btn = btn;

        if (btn == "Trash")
        {
            if (intro == 4)
            {
                StartCoroutine(trashIntro());
            }
            else if (intro == 1 || intro == 2 || intro == 3)
            {
                return;
            }
            else
            {
                gameObject.GetComponent<Trash>().restart();
            }

            cutscene.SetActive(false);
            main.SetActive(false);
            sort.SetActive(true);
            playsound("button");

            musicource.clip = bgsortclip;
            if (!musicource.isPlaying)
                musicource.Play();
        }
        else if (btn == "Guide")
        {
            if (intro == 2 || intro == 3 || intro == 4)
            {
                return;
            }
            guide.SetActive(true);
            playsound("button");
        }
        else if (btn == "Back")
        {
            guide.SetActive(false);
            playsound("button");
        }
        else if (btn == "Back2")
        {
            main.SetActive(true);
            sort.SetActive(false);
            score.SetActive(false);
            cutscene.SetActive(true);

            intro = 5;

            musicource.clip = bgmainclip;
            if (!musicource.isPlaying)
                musicource.Play();
        }
        else if (btn == "Back3")
        {
            map.SetActive(false);
            playsound("button");
        }
        else if (btn == "Back4")
        {
            help.SetActive(false);
            playsound("button");
        }
        else if (btn == "Back5")
        {
            robot.SetActive(false);
            playsound("button");
        }
        else if (btn == "Map")
        {
            if (intro == 1 || intro == 3 || intro == 4)
            {
                return;
            }
            map.SetActive(true);
            playsound("button");
        }
        else if (btn == "Map2")
        {
            Application.OpenURL("https://www.google.com/maps/search/Recycling+Centers+Near+Me");
        }
        else if (btn == "Help")
        {
            help.SetActive(true);
            playsound("button");
        }
        else if (btn == "CBOI")
        {
            if (intro == 1 || intro == 2 || intro == 4)
            {
                return;
            }
            robot.SetActive(true);
            playsound("button");
        }
        else if (btn == "MusicOn")
        {
            musicource.mute = false;
        }
        else if (btn == "MusicOff")
        {
            musicource.mute = true;
        }
        else if (btn == "SoundOn")
        {
            sfxsource.mute = false;
        }
        else if (btn == "SoundOff")
        {
            sfxsource.mute = true;
        }
    }

    public void swipeRight()
    {
        if (guide.activeSelf)
            guide.GetComponent<Guide>().swipeRight();
    }

    public void swipeLeft()
    {
        if (guide.activeSelf)
            guide.GetComponent<Guide>().swipeLeft();
    }

    public void startScore()
    {
        score.SetActive(true);
        score.GetComponent<ScoreBreakdown>().restart();
    }

    public void playsound(string soundname)
    {
        if (soundname == "button")
            sfxsource.clip = buttonsound;
        else if (soundname == "good")
            sfxsource.clip = goodsound;
        else if (soundname == "bad")
            sfxsource.clip = badsound;
        else if (soundname == "complete")
            sfxsource.clip = completesound;
        else if (soundname == "page")
            sfxsource.clip = pagesound;
        else
            return;

        if (!sfxsource.isPlaying)
            sfxsource.Play();
        else
        {
            sfxsource.Stop();
            sfxsource.Play();
        }
    }

    public void tutorial()
    {
        musicource.clip = bgmainclip;
        if (!musicource.isPlaying)
        {
            musicource.Play();
        }

        StartCoroutine(introduction());
    }

    IEnumerator introduction()
    {
        cutscene.SetActive(false);
        main.SetActive(false);
        sort.SetActive(false);
        score.SetActive(false);
        guide.SetActive(false);
        map.SetActive(false);
        help.SetActive(false);
        robot.SetActive(false);

        intro = 1;
        float buffer = 0f;

        robot.SetActive(false);

        cutscene.SetActive(true);
        cutsceneChar.SetActive(false);
        cutsceneCharName.enabled = false;
        cutsceneDialogue.enabled = false;
        cutsceneTap2Continue.enabled = false;

        mapSilh.SetActive(false);
        guideSilh.SetActive(false);
        CBOISilh.SetActive(false);
        TrashSilh.SetActive(false);
        touch.SetActive(false);
        touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0);

        cutsceneCharName.gameObject.SetActive(true);
        cutsceneDialogue.gameObject.SetActive(true);
        cutsceneDialogueBg.SetActive(true);
        cutsceneBg.SetActive(true);

        yield return new WaitForSeconds(1f);

        //resetting all the basics
        cutsceneChar.transform.localScale = new Vector3(17f, 17f, 0f);
        mainBg.transform.position = new Vector3(768.5f, mainBg.transform.position.y, mainBg.transform.position.z);
        cutsceneChar.GetComponent<SpriteRenderer>().flipX = false;
        CBOISilh.GetComponent<SpriteRenderer>().flipX = false;

        cutsceneCharName.enabled = true;
        cutsceneCharName.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0f);
        cutsceneCharName.GetComponent<Text>().text = "???";

        //fade-in name
        while (cutsceneCharName.GetComponent<Text>().color.a < 1f)
        {
            cutsceneCharName.GetComponent<Text>().color = new Color(1f, 1f, 1f, cutsceneCharName.GetComponent<Text>().color.a + 0.1f);
            yield return new WaitForSeconds(0.05f);
        }

        cutsceneDialogue.enabled = true;
        cutsceneDialogue.GetComponent<UITextTypeWriter>().typewrite("bzzt- bzzt- hello?", 0.125f);

        //skip when tapped
        tapped = false;
        while (!tapped && buffer < 2f)
        {
            yield return new WaitForSeconds(0.1f);
            buffer += 0.1f;
        }
        buffer = 0f;
        cutsceneDialogue.GetComponent<UITextTypeWriter>().stop();
        cutsceneDialogue.text = "bzzt- bzzt- hello?";

        cutsceneTap2Continue.gameObject.SetActive(true);
        cutsceneTap2Continue.enabled = true;
        cutsceneTap2Continue.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0f);

        //fade-in tap2continue
        while (cutsceneTap2Continue.GetComponent<Text>().color.a < 1f)
        {
            cutsceneTap2Continue.GetComponent<Text>().color = new Color(1f, 1f, 1f, cutsceneTap2Continue.GetComponent<Text>().color.a + 0.1f);
            yield return new WaitForSeconds(0.01f);
        }

        tapped = false;
        while (!tapped)
        {
            yield return new WaitForSeconds(0.1f);
        }

        cutsceneChar.SetActive(true);
        cutsceneChar.transform.position = new Vector3(188.5f, 285f, 0f);
        cutsceneChar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

        //fade-in char
        while (cutsceneChar.GetComponent<SpriteRenderer>().color.a < 1f)
        {
            cutsceneChar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, cutsceneChar.GetComponent<SpriteRenderer>().color.a + 0.1f);
            yield return new WaitForSeconds(0.05f);
        }

        cutsceneTap2Continue.enabled = false;
        cutsceneDialogue.GetComponent<UITextTypeWriter>().typewrite("Hello! I'm CBOI, your helper robot and, * help and options menu *", 0.125f);

        //skip when tapped
        tapped = false;
        while (!tapped && buffer < 5f)
        {
            yield return new WaitForSeconds(0.1f);
            buffer += 0.1f;
        }
        buffer = 0f;
        cutsceneDialogue.GetComponent<UITextTypeWriter>().stop();
        cutsceneDialogue.text = "Hello! I'm CBOI, your helper robot and, * help and options menu *";

        cutsceneCharName.GetComponent<Text>().text = "CBOI";
        yield return new WaitForSeconds(0.5f);

        cutsceneTap2Continue.enabled = true;
        cutsceneTap2Continue.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0f);

        //fade-in tap2continue
        while (cutsceneTap2Continue.GetComponent<Text>().color.a < 1f)
        {
            cutsceneTap2Continue.GetComponent<Text>().color = new Color(1f, 1f, 1f, cutsceneTap2Continue.GetComponent<Text>().color.a + 0.1f);
            yield return new WaitForSeconds(0.01f);
        }

        tapped = false;
        while (!tapped)
        {
            yield return new WaitForSeconds(0.1f);
        }

        cutsceneTap2Continue.enabled = false;
        cutsceneDialogue.GetComponent<UITextTypeWriter>().typewrite("now I'm going to give you an intoduction about what you can interact with in your room!", 0.025f);

        //skip when tapped
        tapped = false;
        while (!tapped && buffer < 4f)
        {
            yield return new WaitForSeconds(0.1f);
            buffer += 0.1f;
        }
        buffer = 0f;
        cutsceneDialogue.GetComponent<UITextTypeWriter>().stop();
        cutsceneDialogue.text = "now I'm going to give you an intoduction about what you can interact with in your room!";

        cutsceneTap2Continue.enabled = true;
        cutsceneTap2Continue.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0f);

        //fade-in tap2continue
        while (cutsceneTap2Continue.GetComponent<Text>().color.a < 1f)
        {
            cutsceneTap2Continue.GetComponent<Text>().color = new Color(1f, 1f, 1f, cutsceneTap2Continue.GetComponent<Text>().color.a + 0.1f);
            yield return new WaitForSeconds(0.01f);
        }

        tapped = false;
        while (!tapped)
        {
            yield return new WaitForSeconds(0.1f);
        }

        main.SetActive(true);
        HelpBtn.SetActive(false);
        Stars.SetActive(false);

        while (cutsceneBg.GetComponent<SpriteRenderer>().color.a > 0f)
        {
            cutsceneBg.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, cutsceneBg.GetComponent<SpriteRenderer>().color.a - 0.1f);
            cutsceneCharName.GetComponent<Text>().color = new Color(1f, 1f, 1f, cutsceneBg.GetComponent<SpriteRenderer>().color.a - 0.1f);
            cutsceneDialogue.GetComponent<Text>().color = new Color(1f, 1f, 1f, cutsceneBg.GetComponent<SpriteRenderer>().color.a - 0.1f);
            cutsceneTap2Continue.GetComponent<Text>().color = new Color(1f, 1f, 1f, cutsceneBg.GetComponent<SpriteRenderer>().color.a - 0.1f);
            cutsceneDialogueBg.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, cutsceneBg.GetComponent<SpriteRenderer>().color.a - 0.1f);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);

        while (mainBg.transform.position.x >= -440f)
        {
            mainBg.transform.position = new Vector3(mainBg.transform.position.x - 10f, mainBg.transform.position.y, mainBg.transform.position.z);

            if (cutsceneChar.transform.localScale.x > 10f)
            {
                cutsceneChar.transform.localScale = new Vector3(cutsceneChar.transform.localScale.x - 0.05f, cutsceneChar.transform.localScale.x - 0.05f, 0f);
                cutsceneChar.transform.position = new Vector3(cutsceneChar.transform.position.x - 0.5f, cutsceneChar.transform.position.y - 1.2f, 0f);
            }
            yield return new WaitForSeconds(0.0001f);
        }

        cutsceneChar.GetComponent<SpriteRenderer>().flipX = true;
        CBOISilh.GetComponent<SpriteRenderer>().flipX = true;
        CBOISilh.transform.position = new Vector3(135.67f, CBOISilh.transform.position.y, CBOISilh.transform.position.z);

        Stars.SetActive(true);
        HelpBtn.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        //introduction to room through different flashy bits for you to press on and then switching to that place and introducting
        //kinda complicated but dw

        //COLOR IS IN DECIMALS

        //guide
        guideSilh.SetActive(true);
        guideSilh.GetComponent<SpriteRenderer>().color = new Color(guideSilh.GetComponent<SpriteRenderer>().color.r, guideSilh.GetComponent<SpriteRenderer>().color.g, guideSilh.GetComponent<SpriteRenderer>().color.b, 0f);

        //continue after guide is pressed
        bool positive = false;
        //is false when number going up
        //is true when number going down
        btn = "";
        float timer = 0f;
        while (!(btn == "Guide"))
        {
            timer += Time.deltaTime;
            if (positive && guideSilh.GetComponent<SpriteRenderer>().color.a < 0.4f)
            {
                guideSilh.GetComponent<SpriteRenderer>().color = new Color(guideSilh.GetComponent<SpriteRenderer>().color.r, guideSilh.GetComponent<SpriteRenderer>().color.g, guideSilh.GetComponent<SpriteRenderer>().color.b, guideSilh.GetComponent<SpriteRenderer>().color.a + 0.015f);
                yield return new WaitForSeconds(0.0001f);
            }
            else if (guideSilh.GetComponent<SpriteRenderer>().color.a > 0f)
            {
                positive = false;
                guideSilh.GetComponent<SpriteRenderer>().color = new Color(guideSilh.GetComponent<SpriteRenderer>().color.r, guideSilh.GetComponent<SpriteRenderer>().color.g, guideSilh.GetComponent<SpriteRenderer>().color.b, guideSilh.GetComponent<SpriteRenderer>().color.a - 0.015f);
                yield return new WaitForSeconds(0.0001f);
            }
            else
            {
                positive = true;
                yield return new WaitForSeconds(0.0001f);
            }
            if (timer >= 4f)
            {
                touch.SetActive(true);
                touch.transform.position = new Vector3(77.9f, 383.8f, 0f);
                if (touch.GetComponent<SpriteRenderer>().color.a < 175f / 255f)
                    touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (timer - 4f) / 5f);
                else
                    touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 175f / 255f);
            }
        }
        touch.SetActive(false);
        touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0);

        //GUIDE introduction (either in this ienumerator or in seperate ienumerator)
        guideSilh.GetComponent<SpriteRenderer>().color = new Color(guideSilh.GetComponent<SpriteRenderer>().color.r, guideSilh.GetComponent<SpriteRenderer>().color.g, guideSilh.GetComponent<SpriteRenderer>().color.b, 0f);
        guideSilh.SetActive(false);

        yield return new WaitForSeconds(1f);

        //MAP introduction
        mapSilh.SetActive(true);
        mapSilh.GetComponent<SpriteRenderer>().color = new Color(mapSilh.GetComponent<SpriteRenderer>().color.r, mapSilh.GetComponent<SpriteRenderer>().color.g, mapSilh.GetComponent<SpriteRenderer>().color.b, 0f);

        //continue after map is pressed
        positive = false;
        timer = 0f;
        btn = "";
        intro = 2;
        while (!(btn == "Map"))
        {
            timer += Time.deltaTime;
            if (positive && mapSilh.GetComponent<SpriteRenderer>().color.a < 0.4f)
            {
                mapSilh.GetComponent<SpriteRenderer>().color = new Color(mapSilh.GetComponent<SpriteRenderer>().color.r, mapSilh.GetComponent<SpriteRenderer>().color.g, mapSilh.GetComponent<SpriteRenderer>().color.b, mapSilh.GetComponent<SpriteRenderer>().color.a + 0.015f);
                yield return new WaitForSeconds(0.0001f);
            }
            else if (mapSilh.GetComponent<SpriteRenderer>().color.a > 0f)
            {
                positive = false;
                mapSilh.GetComponent<SpriteRenderer>().color = new Color(mapSilh.GetComponent<SpriteRenderer>().color.r, mapSilh.GetComponent<SpriteRenderer>().color.g, mapSilh.GetComponent<SpriteRenderer>().color.b, mapSilh.GetComponent<SpriteRenderer>().color.a - 0.015f);
                yield return new WaitForSeconds(0.0001f);
            }
            else
            {
                positive = true;
                yield return new WaitForSeconds(0.0001f);
            }
            if (timer >= 4f)
            {
                touch.SetActive(true);
                touch.transform.position = new Vector3(228.1f, 458.6f, 0f);
                if (touch.GetComponent<SpriteRenderer>().color.a < 175f / 255f)
                    touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (timer - 4f) / 5f);
                else
                    touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 175f / 255f);
            }
        }
        touch.SetActive(false);
        touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0);

        //MAP introduction (either in this ienumerator or in seperate ienumerator)
        mapSilh.GetComponent<SpriteRenderer>().color = new Color(mapSilh.GetComponent<SpriteRenderer>().color.r, mapSilh.GetComponent<SpriteRenderer>().color.g, mapSilh.GetComponent<SpriteRenderer>().color.b, 0f);
        mapSilh.SetActive(false);

        yield return new WaitForSeconds(1f);

        //continue after introduction is pressed
        CBOISilh.SetActive(true);
        CBOISilh.GetComponent<SpriteRenderer>().color = new Color(CBOISilh.GetComponent<SpriteRenderer>().color.r, CBOISilh.GetComponent<SpriteRenderer>().color.g, CBOISilh.GetComponent<SpriteRenderer>().color.b, 0f);

        positive = false;
        btn = "";
        intro = 3;
        while (!(btn == "CBOI"))
        {
            timer += Time.deltaTime;
            if (positive && CBOISilh.GetComponent<SpriteRenderer>().color.a < 0.4f)
            {
                CBOISilh.GetComponent<SpriteRenderer>().color = new Color(CBOISilh.GetComponent<SpriteRenderer>().color.r, CBOISilh.GetComponent<SpriteRenderer>().color.g, CBOISilh.GetComponent<SpriteRenderer>().color.b, CBOISilh.GetComponent<SpriteRenderer>().color.a + 0.015f);
                yield return new WaitForSeconds(0.0001f);
            }
            else if (CBOISilh.GetComponent<SpriteRenderer>().color.a > 0f)
            {
                positive = false;
                CBOISilh.GetComponent<SpriteRenderer>().color = new Color(CBOISilh.GetComponent<SpriteRenderer>().color.r, CBOISilh.GetComponent<SpriteRenderer>().color.g, CBOISilh.GetComponent<SpriteRenderer>().color.b, CBOISilh.GetComponent<SpriteRenderer>().color.a - 0.015f);
                yield return new WaitForSeconds(0.0001f);
            }
            else
            {
                positive = true;
                yield return new WaitForSeconds(0.0001f);
            }
            if (timer >= 4f)
            {
                touch.SetActive(true);
                touch.transform.position = new Vector3(150.2f, 106.9f, 0f);
                if (touch.GetComponent<SpriteRenderer>().color.a < 175f / 255f)
                    touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (timer - 4f) / 5f);
                else
                    touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 175f / 255f);
            }
        }
        touch.SetActive(false);
        touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0);

        //SORT (cboi) introduction (either in this ienumerator or in seperate ienumerator)
        CBOISilh.GetComponent<SpriteRenderer>().color = new Color(CBOISilh.GetComponent<SpriteRenderer>().color.r, CBOISilh.GetComponent<SpriteRenderer>().color.g, CBOISilh.GetComponent<SpriteRenderer>().color.b, 0f);
        CBOISilh.SetActive(false);

        yield return new WaitForSeconds(1f);

        //continue after upgrades (cboi) is pressed
        TrashSilh.SetActive(true);
        TrashSilh.GetComponent<SpriteRenderer>().color = new Color(TrashSilh.GetComponent<SpriteRenderer>().color.r, TrashSilh.GetComponent<SpriteRenderer>().color.g, TrashSilh.GetComponent<SpriteRenderer>().color.b, 0f);

        positive = false;
        timer = 0f;
        btn = "";
        intro = 4;
        while (!(btn == "Trash"))
        {
            timer += Time.deltaTime;
            if (positive && TrashSilh.GetComponent<SpriteRenderer>().color.a < 0.4f)
            {
                TrashSilh.GetComponent<SpriteRenderer>().color = new Color(TrashSilh.GetComponent<SpriteRenderer>().color.r, TrashSilh.GetComponent<SpriteRenderer>().color.g, TrashSilh.GetComponent<SpriteRenderer>().color.b, TrashSilh.GetComponent<SpriteRenderer>().color.a + 0.015f);
                yield return new WaitForSeconds(0.0001f);
            }
            else if (TrashSilh.GetComponent<SpriteRenderer>().color.a > 0f)
            {
                positive = false;
                TrashSilh.GetComponent<SpriteRenderer>().color = new Color(TrashSilh.GetComponent<SpriteRenderer>().color.r, TrashSilh.GetComponent<SpriteRenderer>().color.g, TrashSilh.GetComponent<SpriteRenderer>().color.b, TrashSilh.GetComponent<SpriteRenderer>().color.a - 0.015f);
                yield return new WaitForSeconds(0.0001f);
            }
            else
            {
                positive = true;
                yield return new WaitForSeconds(0.0001f);
            }
            if (timer >= 4f)
            {
                touch.SetActive(true);
                touch.transform.position = new Vector3(287.3f, 27.4f, 0f);
                if (touch.GetComponent<SpriteRenderer>().color.a < 175f / 255f)
                    touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (timer - 4f) / 5f);
                else
                    touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 175f / 255f);
            }
        }
        touch.SetActive(false);
        touch.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0);

        TrashSilh.GetComponent<SpriteRenderer>().color = new Color(TrashSilh.GetComponent<SpriteRenderer>().color.r, TrashSilh.GetComponent<SpriteRenderer>().color.g, TrashSilh.GetComponent<SpriteRenderer>().color.b, 0f);
        TrashSilh.SetActive(false);

        //SORT Introduction (either in this ienumerator or in seperate ienumerator)


        cutscene.SetActive(false);
        sort.SetActive(true);
        //tried fade-out, it didnt work TT
        main.SetActive(false);
        gameObject.GetComponent<Trash>().enabled = true;

        tapped = false;
        btn = "";

        yield return null;
    }

    IEnumerator trashIntro()
    {
        touch2.SetActive(true);
        touch2.transform.position = new Vector3(60f, 50f, 0f);
        touch2.GetComponent<Animator>().SetInteger("motion", 0);
        yield return new WaitForSeconds(2f);

        touch2.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        touch2.SetActive(true);
        touch2.GetComponent<Animator>().SetInteger("motion", 1);
        yield return new WaitForSeconds(1.5f);
        touch2.GetComponent<Animator>().SetInteger("motion", 2);
        yield return new WaitForSeconds(1.5f);
        touch2.GetComponent<Animator>().SetInteger("motion", 1);
        yield return new WaitForSeconds(1.5f);
        touch2.GetComponent<Animator>().SetInteger("motion", 2);
        yield return new WaitForSeconds(1.5f);

        touch2.SetActive(false);
        yield return new WaitForSeconds(1f);

        gameObject.GetComponent<Trash>().restart();
        yield return null;
    }
}