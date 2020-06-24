using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBG : MonoBehaviour
{
    BCFC controller;
    public Texture tex;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = BCFC.instance;
    }

    // Update is called once per frame
    void Update()
    {
        BCFC.LAYER layer = null;

        if (Input.GetKey(KeyCode.Q))
        {
            layer = controller.background;
            layer.SetTexture(tex);
            animator.SetTrigger("FadeTrigger");
        } 
    }
}
