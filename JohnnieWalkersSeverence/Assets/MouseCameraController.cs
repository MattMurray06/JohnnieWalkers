using System.Runtime.CompilerServices;
using UnityEngine;

public class MouseCameraController : MonoBehaviour
{
    [SerializeField] public float panSpeed = 20f;
    /// <summary>
    /// JOSEPH I HAVE NO CLUE WHY CHANGING PANBORDERTHICKNESS HAS NO INFLUENCE ON WHEN THE MOUSE STARTS TO SCROLL THE SCREEN UP - IT SEEMS CONSTANT, PLZ FIX
    /// Remember the programmers credo: we do things not because they are easy, but because we thought they were going to be easy.
    /// </summary>
    /// 
    /// Never mind figured it out just go into interpreter in unity
    
    public float panBorderThicknessx = 100f;
    public float panBorderThicknessy = 310f;
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

        if (Input.mousePosition.y >= Screen.height - panBorderThicknessy)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThicknessy)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThicknessx)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThicknessx)
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
