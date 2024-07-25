
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class BezierArrow : MonoBehaviour
{

    private TrailRenderer trailRenderer;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.time = float.MaxValue;
        //trailRenderer.positionCount = 20;
        Deactivate();
    }

    public void Deactivate()
    {
        trailRenderer.enabled = false;
    }

    public void Activate(Vector3 start, Vector3 end)
    {
        startPoint = start;
        endPoint = end;
        trailRenderer.enabled = true;

        trailRenderer.AddPosition(transform.InverseTransformPoint(startPoint));
        trailRenderer.AddPosition(transform.InverseTransformPoint(endPoint));

        //UpdateEndPoint(end);
    }

    public void UpdateEndPoint(Vector3 end)
    {
        endPoint = end;
        DrawBezierCurve();
    }

    private void DrawBezierCurve()
    {
        Vector3[] points = new Vector3[trailRenderer.positionCount];
        for (int i = 0; i < trailRenderer.positionCount; i++)
        {
            float t = i / (trailRenderer.positionCount - 1f);
            points[i] = CalculateBezierPoint(t, startPoint, (startPoint + endPoint) / 2, endPoint);
        }
        trailRenderer.SetPositions(points);
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
