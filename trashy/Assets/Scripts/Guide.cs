using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Guide : MonoBehaviour
{
    [Header("Pages")]
    [SerializeField] Sprite page1;
    [SerializeField] Sprite page2;
    [SerializeField] Sprite page3;
    [SerializeField] Sprite page4;

    [Header("Content")]
    [SerializeField] GameObject content;

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
            content.GetComponent<TextMeshProUGUI>().text = "yikes this works";
        }
    }

    public void TurnPage()
    {
        if (state == State.Intro)
        {
            this.gameObject.GetComponent<Image>().sprite = page2;
            state = State.Compost;
        }
        else if (state == State.Compost)
        {
            this.gameObject.GetComponent<Image>().sprite = page3;
            state = State.Recycling;
        }
        else if (state == State.Recycling)
        {
            this.gameObject.GetComponent<Image>().sprite = page4;
            state = State.Trash;
        }
    }

    public void JumpToPage(string s)
    {
        if (s == "Compost")
        {
            this.gameObject.GetComponent<Image>().sprite = page2;
            state = State.Compost;
        }
        else if (s == "Recycling")
        {
            this.gameObject.GetComponent<Image>().sprite = page3;
            state = State.Recycling;
        }
        else if (s == "Trash")
        {
            this.gameObject.GetComponent<Image>().sprite = page4;
            state = State.Trash;
        }
    }
}
