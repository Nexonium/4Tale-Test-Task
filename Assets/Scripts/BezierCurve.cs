
using UnityEngine;

public class BezierCurve : MonoBehaviour
{

    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;

    public LineRenderer lineRenderer;
    public int segmentCount = 50;

    void Update()
    {
        DrawBezierCurve();
    }

    void DrawBezierCurve()
    {

        lineRenderer.positionCount = segmentCount;
        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 position = CalculateBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
            lineRenderer.SetPosition(i, position);
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {

        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }
}
