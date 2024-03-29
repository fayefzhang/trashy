﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBreakdown : MonoBehaviour
{
    [SerializeField] GameObject GameManager;

    [Header("Text")]
    [SerializeField] GameObject good;
    int goodScore = 0;
    [SerializeField] GameObject bad;
    int badScore = 0;
    [SerializeField] GameObject total;
    int totalscore = 0;
    [SerializeField] GameObject stars;
    int starscore = 0;

    [Space]

    [SerializeField] Image panel;
    [SerializeField] ParticleSystem particles;
    [SerializeField] Button back;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void restart()
    {
        init();
    }

    void init()
    {
        panel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);

        goodScore = GameManager.GetComponent<Trash>().getScore()[0];
        badScore = GameManager.GetComponent<Trash>().getScore()[1];
        totalscore = GameManager.GetComponent<Trash>().getScore()[2];

        if (totalscore < 0)
        {
            totalscore = 0;
        }

        if (goodScore < -1 * badScore & totalscore <= 0)
        {
            starscore = 0;
        }
        else
        {
            starscore = (totalscore / 10) + 1;
        }

        good.GetComponent<TextMeshProUGUI>().text = "";
        bad.GetComponent<TextMeshProUGUI>().text = "";
        total.GetComponent<TextMeshProUGUI>().text = "";
        stars.GetComponent<TextMeshProUGUI>().text = "";

        StartCoroutine(score());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator score()
    {
        while (panel.GetComponent<Image>().color.a < 0.4f)
        {
            panel.GetComponent<Image>().color = new Color(1f, 1f, 1f, panel.GetComponent<Image>().color.a + 0.04f);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);

        good.GetComponent<UITextTypeWriter>().countup(goodScore, 0.05f);
        bad.GetComponent<UITextTypeWriter>().countup(badScore, 0.1f);

        if (goodScore * 0.05f > badScore * 0.1f)
            yield return new WaitForSeconds(goodScore * 0.05f);
        else
            yield return new WaitForSeconds(badScore * 0.05f);


        yield return new WaitForSeconds(1f);
        if (totalscore != 0)
        {
            total.GetComponent<UITextTypeWriter>().countup(totalscore, 2 / totalscore);

            yield return new WaitForSeconds((2 / totalscore) * totalscore + 1f);
        }
        else
        {
            total.GetComponent<TextMeshProUGUI>().text = "0";

            yield return new WaitForSeconds(1f);
        }

        stars.GetComponent<UITextTypeWriter>().countup(starscore, 0.2f);

        yield return new WaitForSeconds(starscore * 0.2f);

        GameManager.GetComponent<GameManager>().playsound("complete");
        if (!particles.isPlaying)
        {
            particles.Play();
        }

        while (back.GetComponent<Image>().color.a < 1f)
        {
            back.GetComponent<Image>().color = new Color(1f, 1f, 1f, back.GetComponent<Image>().color.a + 0.09f);
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    public void switchtomain()
    {
        StartCoroutine(tomain());
        GameManager.GetComponent<GameManager>().playsound("button");
    }

    IEnumerator tomain()
    {
        while (panel.GetComponent<Image>().color.a < 1f)
        {
            panel.GetComponent<Image>().color = new Color(1f, 1f, 1f, panel.GetComponent<Image>().color.a + 0.04f);
            yield return new WaitForSeconds(0.1f);
        }
        GameManager.GetComponent<GameManager>().button("Back2");
        yield return null;
    }
}
