using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Singleton.AddStar();
        spriteRenderer.DOColor(new Color(1, 1, 1, 0), .5f);
        transform.DOScale(3f, .5f).OnComplete(() => { gameObject.SetActive(false); });
    }
}
