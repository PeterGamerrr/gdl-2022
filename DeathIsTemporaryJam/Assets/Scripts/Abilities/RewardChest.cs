using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardChest : MonoBehaviour
{

    private bool isOpen;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }



    void OpenChest()
    {
        //TODO: open chest and give rewards
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isOpen", true);
            OpenChest();
        }
    }
}
