using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    private string bin;

    // Start is called before the first frame update
    void Start()
    {
        bin = transform.parent.gameObject.name;
        //bin = "Trash" or "Recycling" or "Compost"
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager.GetComponent<Trash>().gameState() == "sorting")
        {
            if (collision.gameObject.GetComponent<ItemController>().info.type == bin)
            {
                gameManager.GetComponent<Trash>().score += 20;
                gameManager.GetComponent<Trash>().good++;
            }
            else
            {
                if (bin == "Compost" || bin == "Recycling")
                {
                    gameManager.GetComponent<Trash>().endGame();
                }
                else if (bin == "Trash")
                {
                    gameManager.GetComponent<Trash>().score -= 5;
                    gameManager.GetComponent<Trash>().bad++;
                }
                else
                {
                    Debug.Log("u named something wrong, check if 'recycling' is named correctly dude");
                }
            }
        }

        Destroy(collision.gameObject, 0.5f);
    }
}
