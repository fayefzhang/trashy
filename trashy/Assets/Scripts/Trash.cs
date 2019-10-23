using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trash : MonoBehaviour
{
    [Header("Game")]
    int score = 0;
    int good = 0;
    int bad = 0;

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

    [Header("Item Properties")]
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

    private enum TrashState
    {
        Recycling, Trash, Compost
    }

    [SerializeField] private TrashState trashstate;
    [SerializeField] private TrashState ptrashstate;

    private bool swapped = true;
    private float buffer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Sorting;
        swapped = true;

        //assigning variables into items[]
        items[0] = trash1;
        items[1] = trash2;
        items[2] = rec1;

        ptrashstate = TrashState.Trash; //setting ptrashstate as trash (when starting)
        trashstate = TrashState.Trash;
        tbin.SetActive(true);

        StartCoroutine(SpawnTrash());
        lArrow.GetComponent<Image>().color = new Color(0.5f, 0.5f, 1f, 1f); //left is blue
        rArrow.GetComponent<Image>().color = new Color(0.5f, 1f, 0.5f, 1f); //right is green
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.GetComponent<Text>().text = "" + score;

        if (trashstate == TrashState.Recycling)
        {
            rbin.transform.position = new Vector3(160f, rbin.transform.position.y, 0);
            lArrow.GetComponent<Image>().color = new Color(0.5f, 1f, 0.5f, 1f); //left is green
            rArrow.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f); //right is black
        }
        else if (trashstate == TrashState.Compost)
        {
            cbin.transform.position = new Vector3(160f, rbin.transform.position.y, 0);
            lArrow.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f); //left is black
            rArrow.GetComponent<Image>().color = new Color(0.5f, 0.5f, 1f, 1f); //right is blue
        }
        else if (trashstate == TrashState.Trash)
        {
            tbin.transform.position = new Vector3(160f, rbin.transform.position.y, 0);
            lArrow.GetComponent<Image>().color = new Color(0.5f, 0.5f, 1f, 1f); //left is blue
            rArrow.GetComponent<Image>().color = new Color(0.5f, 1f, 0.5f, 1f); //right is green
        }

        if (!swapped)
        {
            cbin.SetActive(false);
            tbin.SetActive(false);
            rbin.SetActive(false);
            
            swapped = true;

            //ptrashstate -> trashstate
            if (ptrashstate == TrashState.Recycling && trashstate == TrashState.Compost)
            {
                rbin.transform.position = new Vector3(160f, rbin.transform.position.y, 0);
                rbin.SetActive(true);
                StopCoroutine("exchangeBin");
                StartCoroutine(exchangeBin(rbin, cbin, false));
            }
            else if (ptrashstate == TrashState.Compost && trashstate == TrashState.Trash)
            {
                cbin.transform.position = new Vector3(160f, rbin.transform.position.y, 0);
                cbin.SetActive(true);
                StopCoroutine("exchangeBin");
                StartCoroutine(exchangeBin(cbin, tbin, false));
            }
            else if (ptrashstate == TrashState.Trash && trashstate == TrashState.Recycling)
            {
                tbin.transform.position = new Vector3(160f, rbin.transform.position.y, 0);
                tbin.SetActive(true);
                StopCoroutine("exchangeBin");
                StartCoroutine(exchangeBin(tbin, rbin, false));
            }
            else if (ptrashstate == TrashState.Recycling && trashstate == TrashState.Trash)
            {
                rbin.transform.position = new Vector3(158.5f, rbin.transform.position.y, 0);
                rbin.SetActive(true);
                StopCoroutine("exchangeBin");
                StartCoroutine(exchangeBin(rbin, tbin, true));
            }
            else if (ptrashstate == TrashState.Trash && trashstate == TrashState.Compost)
            {
                tbin.transform.position = new Vector3(158.5f, rbin.transform.position.y, 0);
                tbin.SetActive(true);
                StopCoroutine("exchangeBin");
                StartCoroutine(exchangeBin(tbin, cbin, true));
            }
            else if (ptrashstate == TrashState.Compost && trashstate == TrashState.Recycling)
            {
                cbin.transform.position = new Vector3(158.5f, rbin.transform.position.y, 0);
                cbin.SetActive(true);
                StopCoroutine("exchangeBin");
                StartCoroutine(exchangeBin(cbin, rbin, true));
            }
        }
        buffer += Time.deltaTime;
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
            //print(score);
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

    public void changeScore(string name, int points)
    {
        if (name == "score")
        {
            score += points;
        }
        else if (name == "good")
        {
            good += points;
        }
        else if (name == "bad")
        {
            bad += points;
        }
    }

    public int[] getScore()
    {
        return new int[] {good, bad, score};
    }

    public void swipeRight()
    {
        if (buffer > 0.1f)
        {
            if (trashstate == TrashState.Recycling)
            {
                trashstate = TrashState.Compost;
                ptrashstate = TrashState.Recycling;
                swapped = false;
            }
            else if (trashstate == TrashState.Compost)
            {
                trashstate = TrashState.Trash;
                ptrashstate = TrashState.Compost;
                swapped = false;
            }
            else if (trashstate == TrashState.Trash)
            {
                trashstate = TrashState.Recycling;
                ptrashstate = TrashState.Trash;
                swapped = false;
            }
        }
        buffer = 0f;
    }

    public void swipeLeft()
    {
        if (buffer > 0.1f)
        {
            if (trashstate == TrashState.Recycling)
            {
                trashstate = TrashState.Trash;
                ptrashstate = TrashState.Recycling;
                swapped = false;
            }
            else if (trashstate == TrashState.Compost)
            {
                trashstate = TrashState.Recycling;
                ptrashstate = TrashState.Compost;
                swapped = false;
            }
            else if (trashstate == TrashState.Trash)
            {
                trashstate = TrashState.Compost;
                ptrashstate = TrashState.Trash;
                swapped = false;
            }
        }
        buffer = 0f;
    }

    IEnumerator exchangeBin(GameObject binOff, GameObject binOn, bool isLeft)
    {
        swapped = true;
        //if swipe is left
        if (isLeft)
        {
            //while bin is not off screen to the left yet
            while (binOff.transform.position.x > -5f)
            {
                //move bin to the left, yeet it off screen
                binOff.transform.position = new Vector3(binOff.transform.position.x - 35f, binOff.transform.position.y, 0);
                yield return new WaitForSeconds(0.05f);
            }
            binOff.SetActive(false);
            //binOff.transform.position = new Vector3(-16.5f, binOff.transform.position.y, 0);

            binOn.transform.position = new Vector3(400f, binOn.transform.position.y, 0);
            binOn.SetActive(true);
            while (binOn.transform.position.x > 160f)
            {
                //move bin to the left
                binOn.transform.position = new Vector3(binOn.transform.position.x - 35f, binOn.transform.position.y, 0);
                yield return new WaitForSeconds(0.05f);
            }
            //binOn.transform.position = new Vector3(160f, binOn.transform.position.y, 0);
        }
        else
        {
            //while bin is not off screen to the right yet
            while (binOff.transform.position.x < 400f)
            {
                //move bin to the left, yeet it off screen
                binOff.transform.position = new Vector3(binOff.transform.position.x + 35f, binOff.transform.position.y, 0);
                yield return new WaitForSeconds(0.05f);
            }
            binOff.SetActive(false);
            //binOff.transform.position = new Vector3(0, binOff.transform.position.y, 0);

            binOn.transform.position = new Vector3(-15f, binOn.transform.position.y, 0);
            binOn.SetActive(true);
            while (binOn.transform.position.x < 160f)
            {
                //move bin to the left
                binOn.transform.position = new Vector3(binOn.transform.position.x + 35f, binOn.transform.position.y, 0);
                yield return new WaitForSeconds(0.05f);
            }
        }
        yield return null;
    }
}