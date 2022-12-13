using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public bool inBasket = false;

    [Header("Tween settings")]
    [SerializeField] float time;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;
    [Space]
    [SerializeField] Transform ballPos;

    bool inMotion = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (inBasket)
        {
            BallController.Singleton.DisableRB();
            collision.gameObject.transform.position = ballPos.position;
            collision.gameObject.transform.SetParent(ballPos.transform, true);
            BallController.Singleton.canPush = true;
        }
        if (!inBasket && !inMotion)
        {
            inMotion = true;
            transform.DOShakeScale(time, strength, vibrato, randomness).OnComplete(() => { inMotion = false; });
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inBasket = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inBasket = false;
    }
}
