using UnityEngine;

public class MouseCameraController : MonoBehaviour
{
    [SerializeField] public float panSpeed = 20f;
    [SerializeField] public float panBorderThickness = 10f;
    [SerializeField] public float zoomSpeed = 2f;
    [SerializeField] public float minZoom = 0f;
    [SerializeField] public float maxZoom = 20f;
    [SerializeField] public GridManager gridManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        float cameraHalfHeight = Camera.main.orthographicSize;
        float cameraHalfWidth = Camera.main.aspect * cameraHalfHeight;

        float minX = cameraHalfWidth - 0.5f;
        float maxX = gridManager._width - cameraHalfWidth- 0.5f;
        float minY = cameraHalfHeight- 0.5f;
        float maxY = gridManager._height - cameraHalfHeight- 0.5f;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scroll * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, Mathf.Min(maxZoom, Mathf.Min(gridManager._width / (2 * Camera.main.aspect), gridManager._height / 2)));
    }
}
