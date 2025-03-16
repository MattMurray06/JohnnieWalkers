using UnityEngine;

public class LineRendererScript2 : MonoBehaviour
{
    private LineRenderer line;
    private Vector2 previousPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = GetComponent<LineRenderer>();
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.positionCount++;
            line.SetPosition(line.positionCount, currentPosition);
        }
    }
}
