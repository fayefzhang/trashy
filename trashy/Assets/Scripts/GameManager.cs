using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool debugSkip = true;

    [Header("Game Sections")]
    [SerializeField] GameObject cutscene;
    [SerializeField] GameObject main;
    [SerializeField] GameObject sort;
    [SerializeField] GameObject score;
    [SerializeField] GameObject guide;

    [Header("Cutscenes")]
    [SerializeField] GameObject cutsceneChar;
    [SerializeField] Text cutsceneCharName;
    [SerializeField] Text cutsceneDialogue;
    [SerializeField] GameObject cutsceneDialogueBg;
    [SerializeField] Text cutsceneTap2Continue;
    [SerializeField] GameObject cutsceneBg;
    [SerializeField] GameObject touch;

    [Header("Main")]
    [SerializeField] GameObject mainBg;
    [SerializeField] GameObject guideSilh;
    [SerializeField] GameObject mapSilh;
    [SerializeField] GameObject CBOISilh;
    [SerializeField] GameObject TrashSilh;

    [Header("AudioClips")]
    [SerializeField] AudioClip bgmainclip;
    [SerializeField] AudioClip bgsortclip;
    [SerializeField] AudioClip bgmenuclip;
    AudioSource musicource;
    AudioSource sfxsource;

    private bool tapped = false;
    private string btn = "";

    private bool switchedScene = false;

    // Start is called before the first frame update
    void Start()
    {
        //set everything to inactive (none of them should be active, it will cause things to break)
        cutscene.SetActive(false);
        main.SetActive(false);
        sort.SetActive(false);
        score.SetActive(false);
        guide.SetActive(false);

        if (!debugSkip)
        {
            if (PlayerPrefs.GetInt("hasPlayed") == 1)
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

                cutsceneChar.transform.localScale = new Vector3(17f, 17f, 0f);
                cutsceneChar.transform.position = new Vector3(30f, 0f, 0f);

                StartCoroutine(introduction());
            }
        }

        main.SetActive(true);
        cutscene.SetActive(true);
        cutsceneChar.GetComponent<SpriteRenderer>().flipX = true;
        cutsceneChar.transform.localScale = new Vector3(10.95f, 10.95f, 0f);
        cutsceneChar.transform.position = new Vector3(130.67f, 140.2f, 0f);

        //AUDIO


        //StartCoroutine(introduction());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void tap()
    {
        tapped = true;
    }

    public void button(string btn)
    {
        this.btn = btn;

        if (!switchedScene)
        {
            if (btn == "Trash")
            {
                cutscene.SetActive(false);
                main.SetActive(false);
                gameObject.GetComponent<Trash>().enabled = true;
                sort.SetActive(true);
            }
            else if (btn == "Guide")
            {
                guide.SetActive(true);
            }
            else if (btn == "Back")
            {
                guide.SetActive(false);
            }
            else if (btn == "Back2")
            {
                main.SetActive(true);
                sort.SetActive(false);
                score.SetActive(false);
                cutscene.SetActive(true);
            }
        }
    }

    public void startScore()
    {
        score.SetActive(true);
    }

    IEnumerator introduction()
    {
        float buffer = 0f;

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
        cutsceneChar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

        //fade-in char
        while (cutsceneChar.GetComponent<SpriteRenderer>().color.a < 1f)
        {
            cutsceneChar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, cutsceneChar.GetComponent<SpriteRenderer>().color.a + 0.1f);
            yield return new WaitForSeconds(0.05f);
        }

        cutsceneTap2Continue.enabled = false;
        cutsceneDialogue.GetComponent<UITextTypeWriter>().typewrite("herro~ I'M C-BOi. your bestest pal! :V", 0.125f);

        //skip when tapped
        tapped = false;
        while (!tapped && buffer < 5f)
        {
            yield return new WaitForSeconds(0.1f);
            buffer += 0.1f;
        }
        buffer = 0f;
        cutsceneDialogue.GetComponent<UITextTypeWriter>().stop();
        cutsceneDialogue.text = "herro~ I'M C-BOi. your bestest pal! :V";

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
}