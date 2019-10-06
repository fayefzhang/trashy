using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trash : MonoBehaviour
{
    [Header("Game")]
    public int score = 0;

    [SerializeField] Text scoreDisplay;

    [Header("Items")]
    public Item trash1;
    public Item trash2;
    public Item rec1;

    [Header("Trash Cans")]
    //(left swipes) recycle > trash > compost > recycle
    public GameObject rbin;
    public GameObject tbin;
    public GameObject cbin;

    [Header("Arrows")]
    public Button rArrow;
    public Button lArrow;

    [Header("Properties")]
    public float spacermin = 1f;
    public float spacermax = 2.5f;

    [Space]

    private Item[] items = new Item[3];

    //state machine ajdl;kasjfk
    private enum State
    {
        Asleep, Sorting, Ended
    }

    [SerializeField] private State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Sorting;

        //assigning variables into items[]
        items[0] = trash1;
        items[1] = trash2;
        items[2] = rec1;

        StartCoroutine(SpawnTrash());
        lArrow.GetComponent<Image>().color = new Color(0.5f, 0.5f, 1f, 1f); //left is blue
        rArrow.GetComponent<Image>().color = new Color(0.5f, 1f, 0.5f, 1f); //right is green
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.GetComponent<Text>().text = "" + score;
    }

    //spawns trash prefabs
    void SpawnPrefab(GameObject obj)
    {
        GameObject em = Instantiate(obj, new Vector3(Random.Range(155f, 168f), 600f, 0f), Quaternion.identity);
    }

    IEnumerator SpawnTrash()
    {
        while (state == State.Sorting)
        {
            float rand = Random.Range(spacermin, spacermax); //space between items
            int randomInt = Random.Range(0, 3); //random item to spawn

            SpawnPrefab(items[randomInt].prefab); //spawns that item

            yield return new WaitForSeconds(rand); //repeats
        }
        yield return null;
    }

    public void endGame()
    {
        if (state == State.Sorting)
        {
            state = State.Ended;
            StopCoroutine(SpawnTrash());
            print("score: " + score);
            state = State.Asleep;
            PlayerPrefs.SetInt("game", score);
        }
    }

    public string gameState()
    {
        if (state == State.Asleep)
            return "asleep";
        else if (state == State.Sorting)
            return "sorting";
        else
            return "ended";
    }

    public void swipeRight()
    {
        if (rbin.activeSelf)
            StartCoroutine(exchangeBin(rbin, cbin, false));
        else if (cbin.activeSelf)
            StartCoroutine(exchangeBin(cbin, tbin, false));
        else
            StartCoroutine(exchangeBin(tbin, rbin, false));
    }

    public void swipeLeft()
    {
        if (rbin.activeSelf)
            StartCoroutine(exchangeBin(rbin, tbin, true));
        else if (tbin.activeSelf)
            StartCoroutine(exchangeBin(tbin, cbin, true));
        else
            StartCoroutine(exchangeBin(cbin, rbin, true));
    }

    IEnumerator exchangeBin(GameObject binOff, GameObject binOn, bool isLeft)
    {
        //if swipe is left
        if (isLeft)
        {
            //while bin is not off screen to the left yet
            while (binOff.transform.position.x > -5f)
            {
                //move bin to the left, yeet it off screen
                binOff.transform.position = new Vector3(binOff.transform.position.x - 35f, binOff.transform.position.y, 0);
                yield return new WaitForSeconds(0.005f);
            }
            binOff.SetActive(false);
            //binOff.transform.position = new Vector3(0, binOff.transform.position.y, 0);

            binOn.transform.position = new Vector3(400f, binOn.transform.position.y, 0);
            binOn.SetActive(true);
            while (binOn.transform.position.x > 158.5f)
            {
                //move bin to the left
                binOn.transform.position = new Vector3(binOn.transform.position.x - 35f, binOn.transform.position.y, 0);
                yield return new WaitForSeconds(0.005f);
            }
        }
        else
        {
            //while bin is not off screen to the right yet
            while (binOff.transform.position.x < 400f)
            {
                //move bin to the left, yeet it off screen
                binOff.transform.position = new Vector3(binOff.transform.position.x + 35f, binOff.transform.position.y, 0);
                yield return new WaitForSeconds(0.005f);
            }
            binOff.SetActive(false);
            //binOff.transform.position = new Vector3(0, binOff.transform.position.y, 0);

            binOn.transform.position = new Vector3(-15f, binOn.transform.position.y, 0);
            binOn.SetActive(true);
            while (binOn.transform.position.x < 158.5f)
            {
                //move bin to the left
                binOn.transform.position = new Vector3(binOn.transform.position.x + 35f, binOn.transform.position.y, 0);
                yield return new WaitForSeconds(0.005f);
            }
        }

        if (rbin.activeSelf)
        {
            lArrow.GetComponent<Image>().color = new Color(0.5f, 1f, 0.5f, 1f); //left is green
            rArrow.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f); //right is black
        }
        else if (cbin.activeSelf)
        {
            lArrow.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f); //left is black
            rArrow.GetComponent<Image>().color = new Color(0.5f, 0.5f, 1f, 1f); //right is blue
        }
        else
        {
            lArrow.GetComponent<Image>().color = new Color(0.5f, 0.5f, 1f, 1f); //left is blue
            rArrow.GetComponent<Image>().color = new Color(0.5f, 1f, 0.5f, 1f); //right is green
        }
        yield return null;
    }
}