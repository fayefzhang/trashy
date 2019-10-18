using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UITextTypeWriter : MonoBehaviour
{
    //Thank you: https://unitycoder.com/blog/2015/12/03/ui-text-typewriter-effect-script/

    Text txt;
    TextMeshProUGUI count;
    string story;
    int score;

    float speed = 0.125f;

    void Start()
    { 
    }

    public void typewrite(string story, float speed)
    {
        txt = gameObject.GetComponent<Text>();
        this.story = story;
        this.speed = speed;
        txt.text = "";

        // TODO: add optional delay when to start
        StartCoroutine("PlayText");
    }

    public void stop()
    {
        StopAllCoroutines();
    }

    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(speed);
        }
        yield return null;
    }

    public void countup(int score, float speed)
    {
        count = gameObject.GetComponent<TextMeshProUGUI>();
        this.score = score;
        this.speed = speed;
        count.text = "";

        StartCoroutine("PlayTextCount");
    }

    IEnumerator PlayTextCount()
    {
        for (int i = 1; i <= score; i++)
        {
            count.text = "" + i;
            yield return new WaitForSeconds(speed);
        }
        yield return null;
    }

}