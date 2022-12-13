using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetShaking : MonoBehaviour
{
    public bool inBasket = false;

    [Header("Tween settings")]
    [SerializeField] float time;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;

    bool inMotion = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
