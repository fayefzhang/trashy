using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item info;
    [SerializeField] float gravity = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fall());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator fall() //edgy falling gravity thing
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (gravity), 0);
            yield return new WaitForSeconds(0.01f);
        }

    }
}
