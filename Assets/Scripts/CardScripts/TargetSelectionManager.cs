
using UnityEngine;

[RequireComponent(typeof(BezierCurve))]
public class TargetSelectionManager : MonoBehaviour
{

    public BezierCurve bezierCurve;
    public GameObject target;

    private void Start()
    {
        bezierCurve = GetComponent<BezierCurve>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            bezierCurve.point0.position = transform.position;
            bezierCurve.point3.position = Input.mousePosition;
        }
    }
}
