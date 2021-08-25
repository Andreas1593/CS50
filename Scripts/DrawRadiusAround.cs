using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class DrawRadiusAround : MonoBehaviour
{

    // The more segments, the smoother the circle
    [Range(0, 200)]
    public int segments = 75;

    private float xradius;
    private float yradius;

    LineRenderer line;
    Turret turret;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        // Set the circles' radius to the turret's attack range
        float range = gameObject.GetComponent<Turret>().range;
        xradius = range;
        yradius = range;

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;

        float angle = 20f;

        line.widthMultiplier = .1f;
        line.startColor = Color.red;
        line.endColor = Color.red;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            // Set horizontal circle
            line.SetPosition(i, new Vector3(x, 1f, y));

            angle += (360f / segments);
        }
    }

}
