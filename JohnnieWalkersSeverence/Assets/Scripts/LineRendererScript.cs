using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 startMousePosition;
    private bool isDrawing = false;

    void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 5;  // 4 corners + closing point
        lineRenderer.loop = false;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
    }

    void Update()
    {
        if (isDrawing == false && Input.GetMouseButtonDown(0))  // Left mouse button pressed
        {
            startMousePosition = GetWorldMousePosition();
            isDrawing = true;
        }

        if (isDrawing && Input.GetMouseButton(0))  // While holding mouse
        {
            Vector3 currentMousePosition = GetWorldMousePosition();
            DrawBox(startMousePosition, currentMousePosition);
        }

        if (Input.GetMouseButtonUp(0))  // Mouse released
        {
            isDrawing = false;
            lineRenderer.positionCount = 0;  // Hide the box
        }
    }

    private Vector3 GetWorldMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        //mousePos.z = 10f;  // Distance from camera (adjust as needed)
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void DrawBox(Vector3 start, Vector3 end)
    {
        lineRenderer.positionCount = 5;  // 4 corners + closing point

        Vector3 topLeft = new Vector3(start.x, end.y, 0);
        Vector3 topRight = new Vector3(end.x, end.y, 0);
        Vector3 bottomRight = new Vector3(end.x, start.y, 0);
        Vector3 bottomLeft = new Vector3(start.x, start.y, 0);

        lineRenderer.SetPosition(0, topLeft);
        lineRenderer.SetPosition(1, topRight);
        lineRenderer.SetPosition(2, bottomRight);
        lineRenderer.SetPosition(3, bottomLeft);
        lineRenderer.SetPosition(4, topLeft); // Close the loop
    }
}
