using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float pushForce = 4f;

    #region private hidden
    bool isDragging = false;
    Camera cam;
    Rigidbody2D rb;
    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance; 
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    //-Drag--------------------------------------
    void OnDragStart()
    {
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnDrag() 
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        Debug.DrawLine(startPoint, endPoint);
    }
    void OnDragEnd()
    {
        Push(force);
    }

}