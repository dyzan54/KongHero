using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndexController : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        PlayAnimation();
        Invoke("StopAnimation", 1f);
        PlayAnimation();

    }

    public void PlayAnimation()
    {
        anim.SetBool("Action", true);
    }

    public void StopAnimation()
    {
        anim.SetBool("Action", false);
    }
}
