using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    [SerializeField] GameObject gameManager;

    [Header("Drop Speed")]
    [SerializeField] GameObject dropName;
    [SerializeField] GameObject dropCost;
    [SerializeField] GameObject dropNumbers;

    [Header("Multiplier")]
    [SerializeField] GameObject multName;
    [SerializeField] GameObject multCost;
    [SerializeField] GameObject multNumbers;

    [Header("Level Upgrade")]
    [SerializeField] GameObject lvlName;
    [SerializeField] GameObject lvlCost;
    [SerializeField] Image lvlImage;
    int lvlupgrade;

    [Header("Item Icons")]
    [SerializeField] Sprite cardboard;
    [SerializeField] Sprite applecore;
    [SerializeField] Sprite crumpledpaper;
    [SerializeField] Sprite papers;
    [SerializeField] Sprite flower;
    [SerializeField] Sprite teabag;
    [SerializeField] Sprite pizza;
    [SerializeField] Sprite mail;
    [SerializeField] Sprite xigua;
    [SerializeField] Sprite wrappingpaper;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<Trash>().getLevel() == 1)
        {
            lvlImage.GetComponent<Image>().sprite = cardboard;
            lvlCost.GetComponent<TextMeshProUGUI>().text = "    10";
            lvlName.GetComponent<TextMeshProUGUI>().text = "     Cardboard";
            lvlupgrade = 10;
        }
        else if (gameManager.GetComponent<Trash>().getLevel() == 2)
        {
            lvlImage.GetComponent<Image>().sprite = applecore;
            lvlCost.GetComponent<TextMeshProUGUI>().text = "    20";
            lvlName.GetComponent<TextMeshProUGUI>().text = "  Apple Core";
            lvlupgrade = 20;
        }
        else if (gameManager.GetComponent<Trash>().getLevel() == 3)
        {
            lvlImage.GetComponent<Image>().sprite = crumpledpaper;
            lvlCost.GetComponent<TextMeshProUGUI>().text = "    40";
            lvlName.GetComponent<TextMeshProUGUI>().text = "  Crumpled Paper";
            lvlupgrade = 40;
        }
        else if (gameManager.GetComponent<Trash>().getLevel() == 4)
        {
            lvlImage.GetComponent<Image>().sprite = papers;
            lvlCost.GetComponent<TextMeshProUGUI>().text = "    75";
            lvlName.GetComponent<TextMeshProUGUI>().text = "  Papers";
            lvlupgrade = 75;
        }
        else if (gameManager.GetComponent<Trash>().getLevel() == 5)
        {
            lvlImage.GetComponent<Image>().sprite = flower;
            lvlCost.GetComponent<TextMeshProUGUI>().text = "   120";
            lvlName.GetComponent<TextMeshProUGUI>().text = "  Flower";
            lvlupgrade = 120;
        }
        else if (gameManager.GetComponent<Trash>().getLevel() == 6)
        {
            lvlImage.GetComponent<Image>().sprite = teabag;
            lvlCost.GetComponent<TextMeshProUGUI>().text = "   200";
            lvlName.GetComponent<TextMeshProUGUI>().text = "  Tea Bag";
            lvlupgrade = 200;
        }
        else if (gameManager.GetComponent<Trash>().getLevel() == 7)
        {
            lvlImage.GetComponent<Image>().sprite = pizza;
            lvlCost.GetComponent<TextMeshProUGUI>().text = "    300";
            lvlName.GetComponent<TextMeshProUGUI>().text = "  Pizza Box";
            lvlupgrade = 300;
        }
        else if (gameManager.GetComponent<Trash>().getLevel() == 8)
        {
            lvlImage.GetComponent<Image>().sprite = mail;
            lvlCost.GetComponent<TextMeshProUGUI>().text = "   450";
            lvlName.GetComponent<TextMeshProUGUI>().text = "  Mail";
            lvlupgrade = 450;
        }
        else if (gameManager.GetComponent<Trash>().getLevel() == 9)
        {
            lvlImage.GetComponent<Image>().sprite = xigua;
            lvlCost.GetComponent<TextMeshProUGUI>().text = "    600";
            lvlName.GetComponent<TextMeshProUGUI>().text = "  Watermelon";
            lvlupgrade = 600;
        }
        else if (gameManager.GetComponent<Trash>().getLevel() == 10)
        {
            lvlImage.GetComponent<Image>().sprite = wrappingpaper;
            lvlCost.GetComponent<TextMeshProUGUI>().text = "   1000";
            lvlName.GetComponent<TextMeshProUGUI>().text = "  Wrapping Paper";
            lvlupgrade = 1000;
        }

        dropName.GetComponent<TextMeshProUGUI>().text = "  Item Drop Speed LV " + gameManager.GetComponent<Trash>().getDropLevel();
        if (gameManager.GetComponent<Trash>().getDropLevel() < 10)
        {
            dropCost.GetComponent<TextMeshProUGUI>().text = "    " + (gameManager.GetComponent<Trash>().getDropLevel() * 10);
        }
        else
        {
            dropCost.GetComponent<TextMeshProUGUI>().text = "   " + (gameManager.GetComponent<Trash>().getDropLevel() * 10);
        }
        dropNumbers.GetComponent<TextMeshProUGUI>().text = "x 1.2 item fall speed";
    }

    public void btn(string name)
    {
        if (name == "level")
        {
            if (lvlupgrade <= PlayerPrefs.GetInt("stars", 0))
            {
                PlayerPrefs.SetInt("stars", PlayerPrefs.GetInt("stars", 0) - lvlupgrade);
                gameManager.GetComponent<Trash>().increaseLevel();
            }
        }
        else if (name == "drop")
        {
            if (gameManager.GetComponent<Trash>().getDropLevel() * 10 <= PlayerPrefs.GetInt("stars", 0))
            {
                PlayerPrefs.SetInt("stars", PlayerPrefs.GetInt("stars", 0) - (gameManager.GetComponent<Trash>().getDropLevel() * 10));
                gameManager.GetComponent<Trash>().editGravity(1.2f);
            }
        }
        gameManager.GetComponent<GameManager>().playsound("button");
    }
}
