using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Panning speed and tolerance
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    // Scroll speed for mouse wheel
    public float scrollSpeed = 5f;
    // Clamp scrolling
    public float minY = 10f;
    public float maxY = 80f;

    // Clamp camera movement
    public float minX = -40f;
    public float maxX = 120f;

    public float minZ = -80f;
    public float maxZ = 7f;

    // Update is called once per frame
    void Update()
    {

        // Disable controls when game over
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        // Moving the camera with WASD or mouse
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            // forward = shorthand for Vector3(0, 0, 1)
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            // back = shorthand for Vector3(0, 0, -1)
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }


        // Axis value for the scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Camera's current position
        Vector3 pos = transform.position;

        // Compute new height for the camera when scrolling with mouse wheel
        // Large factor required since the scroll wheel's value is very small (lower than 0)
        pos.y -= scroll * 700 * scrollSpeed * Time.deltaTime;

        // Clamp scrolling (y-axis movement)
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Clamp x- and z-axis camera movement
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        // Set the cameras new position and reset x / y movement if it was out of clamp range
        transform.position = pos;

    }
}
