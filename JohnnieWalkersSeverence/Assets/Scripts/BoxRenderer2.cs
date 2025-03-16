using UnityEngine;

public class BoxRenderer2 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Reference to SpriteRenderer
    private int defaultSortingOrder; // Store default sorting order

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            defaultSortingOrder = spriteRenderer.sortingOrder; // Store the default order
            spriteRenderer.enabled = false; // Start invisible
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // On left mouse click
        {
            BringToFront();
        }

        if (Input.GetMouseButtonUp(0)) // On mouse release
        {
            HideBox();
        }
    }

    void BringToFront()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true; // Make visible
            spriteRenderer.sortingOrder = 100; // Bring to front (higher sorting order)
        }
    }

    void HideBox()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false; // Make invisible
            spriteRenderer.sortingOrder = defaultSortingOrder; // Reset sorting order
        }
    }
}
