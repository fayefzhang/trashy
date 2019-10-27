using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item info;
    float gravity = 15f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fall());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setGravity(float gravity)
    {
        this.gravity = gravity;
    }

    IEnumerator fall() //edgy falling gravity thing
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (gravity/5), 0);
            yield return new WaitForSeconds(0.0001f);
        }
        
    }
}
