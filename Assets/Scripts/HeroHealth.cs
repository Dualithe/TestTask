using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections.Generic;
using System;

public class HeroHealth : MonoBehaviour
{
    private HeartManager hm;

    private void Start()
    {
        hm = GameManager.Get.HeartManager;
    }

    public void animateDeath()
    {
        GetComponent<HeroBehavior>().disableInput();
        GetComponent<Animator>().Play("herochar_death");
    }

    public void die()
    {
        Destroy(gameObject);
    }
}
