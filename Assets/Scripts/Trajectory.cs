using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] float spacing;
    [SerializeField, Range(0.01f, 0.3f)] float dotMinScale;
    [SerializeField, Range(0.3f, 1f)] float dotMaxScale;
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotPrefab;

    Transform[] dotsList;

    Vector2 pos;
    float timeStep;

    private void Start()
    {
        Hide();
        InitializeDots();
    }

    void InitializeDots() 
    {
        dotsList = new Transform[amount];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scaleFactor = scale / amount;

        for (int i = 0; i < amount; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, dotsParent.transform).transform;
            dotsList[i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
                scale -= scaleFactor;
        }
    }

    public void UpdateDots(Vector2 ballPos, Vector2 forceApplied) 
    {
        timeStep = spacing;
        for (int i = 0; i < amount; i++)
        {
            pos.x = (ballPos.x + forceApplied.x * timeStep);
            pos.y = (ballPos.y + forceApplied.y * timeStep) - (Physics2D.gravity.magnitude * timeStep * timeStep) / 2f;

            dotsList[i].position = pos;
            timeStep += spacing;
        }
    }

    public void Show() 
    {
        dotsParent.SetActive(true);
    }

    public void Hide() 
    {
        dotsParent.SetActive(false);
    }
}
