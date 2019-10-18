using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBreakdown : MonoBehaviour
{
    [SerializeField] GameObject GameManager;

    [Header("Text")]
    [SerializeField] GameObject good;
    [SerializeField] GameObject bad;
    [SerializeField] GameObject total;
    [SerializeField] GameObject stars;

    [Space]

    [SerializeField] ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        good.GetComponent<TextMeshProUGUI>().text = "";
        bad.GetComponent<TextMeshProUGUI>().text = "";
        total.GetComponent<TextMeshProUGUI>().text = "";
        stars.GetComponent<TextMeshProUGUI>().text = "";
        StartCoroutine("score");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator score()
    {
        good.GetComponent<UITextTypeWriter>().countup(GameManager.GetComponent<Trash>().good, 0.05f);
        bad.GetComponent<UITextTypeWriter>().countup(GameManager.GetComponent<Trash>().bad, 0.1f);

        if (GameManager.GetComponent<Trash>().good * 0.05f > GameManager.GetComponent<Trash>().bad * 0.1f)
            yield return new WaitForSeconds(GameManager.GetComponent<Trash>().good * 0.05f);
        else
            yield return new WaitForSeconds(GameManager.GetComponent<Trash>().bad * 0.05f);


        yield return new WaitForSeconds(1f);

        total.GetComponent<UITextTypeWriter>().countup(GameManager.GetComponent<Trash>().good + GameManager.GetComponent<Trash>().bad, 0.0625f);

        yield return new WaitForSeconds((GameManager.GetComponent<Trash>().good + GameManager.GetComponent<Trash>().bad) * 0.0625f + 1);

        stars.GetComponent<UITextTypeWriter>().countup((GameManager.GetComponent<Trash>().good + GameManager.GetComponent<Trash>().bad) / 10, 0.2f);

        yield return new WaitForSeconds((GameManager.GetComponent<Trash>().good + GameManager.GetComponent<Trash>().bad) / 10 * 0.2f);

        if (!particles.isPlaying)
        {
            particles.Play();
        }
        yield return null;
    }
}
