
using UnityEngine;

public class BezierArrow : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 20;
        Deactivate();
    }

    public void Deactivate()
    {
        lineRenderer.enabled = false;
    }

    public void Activate(Vector3 start, Vector3 end)
    {
        startPoint = start;
        endPoint = end;
        lineRenderer.enabled = true;
        UpdateEndPoint(end);
    }

    public void UpdateEndPoint(Vector3 end)
    {
        endPoint = end;
        DrawBezierCurve();
    }

    private void DrawBezierCurve()
    {
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float t = i / (lineRenderer.positionCount - 1f);
            points[i] = CalculateBezierPoint(t, startPoint, (startPoint + endPoint) / 2, endPoint);
        }
        lineRenderer.SetPositions(points);
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {

        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;    // u^2 * p0
        p += 2 * u * t * p1;    // 2 *u *t *p1
        p += tt * p2;           // t^2 * p2

        return p;
    }
}
