using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Guide : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject notebook;

    [Header("Pages")]
    [SerializeField] Sprite page1;
    [SerializeField] Sprite page2;
    [SerializeField] Sprite page3;
    [SerializeField] Sprite page4;

    [Header("Content")]
    [SerializeField] GameObject intro;
    [SerializeField] GameObject compost;
    [SerializeField] GameObject recycle;
    [SerializeField] GameObject trash;

    private enum State
    {
        Intro, Compost, Recycling, Trash
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
            compost.SetActive(false);
            recycle.SetActive(false);
            trash.SetActive(false);
            notebook.GetComponent<Image>().sprite = page1;
        }
        else if (state == State.Compost)
        {
            intro.SetActive(false);
            compost.SetActive(true);
            recycle.SetActive(false);
            trash.SetActive(false);
            notebook.GetComponent<Image>().sprite = page2;
        }
        else if (state == State.Recycling)
        {
            intro.SetActive(false);
            compost.SetActive(false);
            recycle.SetActive(true);
            trash.SetActive(false);
            notebook.GetComponent<Image>().sprite = page3;
        }
        else if (state == State.Trash)
        {
            intro.SetActive(false);
            compost.SetActive(false);
            recycle.SetActive(false);
            trash.SetActive(true);
            notebook.GetComponent<Image>().sprite = page4;
        }
    }

    public void TurnPage()
    {
        if (state == State.Intro)
        {
            state = State.Compost;
        }
        else if (state == State.Compost)
        {
            state = State.Recycling;
        }
        else if (state == State.Recycling)
        {
            state = State.Trash;
        }
        else if (state == State.Trash)
        {
            return;
        }
        gameManager.GetComponent<GameManager>().playsound("page");
    }

    public void TurnPageBack()
    {
        if (state == State.Intro)
        {
            return;
        }
        else if (state == State.Compost)
        {
            state = State.Intro;
        }
        else if (state == State.Recycling)
        {
            state = State.Compost;
        }
        else if (state == State.Trash)
        {
            state = State.Recycling;
        }
        gameManager.GetComponent<GameManager>().playsound("page");
    }

    public void JumpToPage(string s)
    {
        if (s == "Compost")
        {
            state = State.Compost;
        }
        else if (s == "Recycling")
        {
            state = State.Recycling;
        }
        else if (s == "Trash")
        {
            state = State.Trash;
        }
        gameManager.GetComponent<GameManager>().playsound("page");
    }

    public void swipeRight()
    {
        TurnPageBack();
    }

    public void swipeLeft()
    {
        TurnPage();
    }

    public void open(string name)
    {
        if (name == "compost")
        {
            Application.OpenURL("http://livinggreen.ifas.ufl.edu/waste/composting.html");
        }
        if (name == "trash")
        {
            Application.OpenURL("https://www.scientificamerican.com/article/where-does-your-garbage-go/");
        }
        else if (name == "recycle")
        {
            Application.OpenURL("https://earth911.com/recycling-guide/how-to-recycle-tin-or-steel-cans/");
        }
    }
}
