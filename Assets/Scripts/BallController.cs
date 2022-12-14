using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BallController : MonoBehaviour
{
    public static BallController Singleton;

    public bool canPush = false;
    public Basket currentBasket;

    [SerializeField] Trajectory trajectory;
    [SerializeField] float pushForce = 4f;

    [HideInInspector] public bool wallBounce = false;
    [HideInInspector] public int hitCount = 0;

    #region private hidden
    bool isDragging = false;
    Camera cam;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;
    #endregion

    void Start()
    {
        if(!Singleton) Singleton = this;

        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            isDragging = true;
            OnDragStart();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if(isDragging)
        {
            OnDrag();
        }

        if (transform.position.y <= -5f)
        {
            GameManager.Singleton.GameOver();
        }
    }

    public void DisableRB()
    {
        rb.bodyType = RigidbodyType2D.Static;
        circleCollider.enabled = false;
    }

    public void EnableRB()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void EnableCollision()
    { 
        circleCollider.enabled = true;
    }

    void Push(Vector2 force)
    {
        if (canPush)
        {
            canPush = false;
            rb.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(Timer(.1f, EnableCollision));
            currentBasket.NormalizeNet();
        }
    }

    public void ClearHitData()
    {
        wallBounce = false;
        hitCount = 0;
    }

    IEnumerator Timer(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }

    //-Drag--------------------------------------
    void OnDragStart()
    {
        if (!canPush) return;
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        trajectory.Show();
    }

    void OnDrag() 
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        trajectory.UpdateDots(transform.position, force);
        currentBasket.RotateTo(direction);
        currentBasket.PullNet(Mathf.Clamp(distance, 1, 2));

        Debug.DrawLine(startPoint, endPoint);
    }

    void OnDragEnd()
    {
        EnableRB();
        transform.SetParent(null);
        Push(force);
        trajectory.Hide();
    }

    //-Colliders---------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Wall")
            wallBounce = true;
        else if (collision.collider.tag == "Ring")
            hitCount++;
    }
}
