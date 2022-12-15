using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject inGame;
    [SerializeField] BallController ballController;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayGame()
    {
        animator.Play("MenuOut");
        ballController.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        ballController.enabled = true;
        inGame.SetActive(true);
    }

    public void EndAnimation()
    { 
        gameObject.SetActive(false);
    }
}
