using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    private enum State
    {
        fine, notfine
    }

    State state;

    Animator anim;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toggle()
    {
        state = State.notfine;
        anim.SetBool("bool", true);
        sprite.color = new Color(1f, 112f / 255f, 122f / 255f, 1f);
    }

    public void reset()
    {
        anim = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        state = State.fine;
        sprite.color = new Color(1f, 1f, 1f, 1f);
    }

    public bool getState()
    {
        if (state == State.fine)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
