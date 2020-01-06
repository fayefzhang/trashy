using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //THANK YOU https://forum.unity.com/threads/simple-swipe-and-tap-mobile-input.376160/

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    // Start is called before the first frame update
    void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance) //big drag dist. boi
                {
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y)) //bigger horizontal movement
                    {
                        if ((lp.x > fp.x))
                        {
                            //Debug.Log("Right Swipe");
                            if (gameObject.GetComponent<Trash>().enabled)
                                gameObject.GetComponent<Trash>().swipeRight();
                            gameObject.GetComponent<GameManager>().swipeRight();
                        }
                        else
                        {
                            //Debug.Log("Left Swipe");
                            if (gameObject.GetComponent<Trash>().enabled)
                                gameObject.GetComponent<Trash>().swipeLeft();
                            gameObject.GetComponent<GameManager>().swipeLeft();
                        }
                    }
                    else //big vertical moment
                    {
                        if (lp.y > fp.y)
                        {
                            //Debug.Log("Up Swipe");
                        }
                        else
                        {
                            //Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    //print("tap at : (" + lp.x + ", " + lp.y + ")");

                    gameObject.GetComponent<GameManager>().tap();
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<GameManager>().tap();
        }
    }
}
