using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class UITextTypeWriter : MonoBehaviour
{
    //Thank you: https://unitycoder.com/blog/2015/12/03/ui-text-typewriter-effect-script/

    Text txt;
    string story;

	float speed = 0.125f;

    void Start()
    {
		txt = gameObject.GetComponent<Text>();
    }

    public void typewrite(string story, float speed)
    {
        this.story = story;
		this.speed = speed;
        txt.text = "";

        // TODO: add optional delay when to start
        StartCoroutine("PlayText");
    }

    public void stop()
    {
        StopCoroutine("PlayText");
    }

    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

}