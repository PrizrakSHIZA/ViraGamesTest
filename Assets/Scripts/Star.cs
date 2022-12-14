using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        transform.rotation = Quaternion.identity;
        audioSource = GetComponent<AudioSource>();
    }

    public void Init()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        circleCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        circleCollider.enabled = false;
        GameManager.Singleton.AddStar();
        audioSource.Play();
        spriteRenderer.DOColor(new Color(1, 1, 1, 0), 1f);
        transform.DOScale(3f, 1f);
    }
}
