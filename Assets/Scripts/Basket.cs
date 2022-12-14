using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public bool inBasket = false;
    public int id;

    [Header("Tween settings")]
    [SerializeField] float time;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;
    [Header("References")]
    [SerializeField] Transform ballPos;
    [SerializeField] Transform net;
    public GameObject star;

    Vector3 baseBallPos;
    bool inMotion = false;

    private void Start()
    {
        baseBallPos = ballPos.localPosition;
    }

    private void Update()
    {
        if (transform.position.y <= -5f)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //net shaking
        if (!inBasket && !inMotion)
        {
            inMotion = true;
            net.DOShakeScale(time, strength, vibrato, randomness).OnComplete(() => { inMotion = false; });
        }

        if (inBasket)
        {
            BallController.Singleton.currentBasket = this;
            GameManager.Singleton.Catch(id);

            BallController.Singleton.DisableRB();
            collision.gameObject.transform.position = ballPos.position;
            collision.gameObject.transform.SetParent(ballPos.transform, true);
            BallController.Singleton.canPush = true;
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

    public void RotateTo(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    public void PullNet(float strength)
    {
        net.localScale = new Vector3(1, strength, 1);
        ballPos.localPosition = new Vector3(ballPos.localPosition.x, baseBallPos.y - (strength-1)/2, 0);
    }

    public void NormalizeNet()
    {
        net.DOScaleY(1f, .5f).SetEase(Ease.OutBounce);
        ballPos.localPosition = baseBallPos;
    }
}
