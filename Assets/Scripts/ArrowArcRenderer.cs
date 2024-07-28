using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Settings of arrow while targeting
/// </summary>

public class ArrowArcRenderer : MonoBehaviour
{

    [Header("Arrow objects")]
    public GameObject arrowHeadPrefab;
    public GameObject arrowBodyPrefab;

    [Header("Arrow settings")]
    public int poolSize = 50;
    private List<GameObject> dotPool = new List<GameObject>();
    private GameObject arrowInstance;

    public float spacing = 20;
    public float arrowAngleAdjust = 180;
    public int dotsToSkip = 2;
    private Vector3 arrowDirection;

    void Awake()
    {
        arrowInstance = Instantiate(arrowHeadPrefab, transform);
        arrowInstance.transform.localPosition = Vector3.zero;
        arrowInstance.SetActive(false);
        InitDotPool(poolSize);
    }

    public void OnEnable()
    {
        arrowInstance.SetActive(true);
    }

    public void OnDisable()
    {
        arrowInstance.SetActive(false);
        for (int i = 0; i < dotPool.Count; i++)
        {
            dotPool[i].SetActive(false);
        }
    }

    void InitDotPool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject dot = Instantiate(arrowBodyPrefab, Vector3.zero, Quaternion.identity, transform);
            dot.SetActive(false);
            dotPool.Add(dot);
        }
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 0;

        Vector3 startPos = transform.position;
        Vector3 midPoint = CalculateMidPoint(startPos, mousePos);

        UpdateArc(startPos, midPoint, mousePos);
        PositionAndRotateArrow(mousePos);
    }

    Vector3 CalculateMidPoint(Vector3 start, Vector3 end)
    {
        Vector3 midPoint = (start + end) / 2;
        float arcHeight = Vector3.Distance(start, end) / 3f;
        midPoint.y += arcHeight;
        return midPoint;
    }

    void UpdateArc(Vector3 start, Vector3 mid, Vector3 end)
    {
        int numDots = Mathf.CeilToInt(Vector3.Distance(start, end) / spacing);

        for (int i = 0; i < numDots && i < dotPool.Count; i++)
        {
            float t = i / (float)numDots;
            t = Mathf.Clamp(t, 0f, 1f);  // Should always stay within range [0, 1]

            Vector3 position = QuadraticBezierPoint(start, mid, end, t);

            if (i != numDots - dotsToSkip)
            {
                dotPool[i].transform.position = position;
                dotPool[i].SetActive(true);
            }
            if (i == numDots - (dotsToSkip + 1) && i - dotsToSkip + 1 >= 0)
            {
                arrowDirection = dotPool[i].transform.position;
            }
        }

        // Deactivate unused
        for (int i = numDots - dotsToSkip; i < dotPool.Count; i++)
        {
            if (i > 0)
            {
                dotPool[i].SetActive(false);
            }
        }
    }

    private Vector3 QuadraticBezierPoint(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * start;     // (1 - t)^2 * P0
        point += 2 * u * t * control;   // 2(1 - t)t * P1
        point += tt * end;              // t^2 * P2

        return point;
    }

    void PositionAndRotateArrow(Vector3 position)
    {
        arrowInstance.transform.position = position;
        Vector3 direction = arrowDirection - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += arrowAngleAdjust;
        arrowInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
