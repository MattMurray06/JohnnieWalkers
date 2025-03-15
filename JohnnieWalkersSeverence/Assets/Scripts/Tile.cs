using UnityEngine;

public class Tile : MonoBehaviour
{   
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _baseColor; 
    
    [SerializeField] private Color _offsetColor;
    
    [SerializeField] private Color _highlightColor;

    private void Start() {
        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas != null) {
            canvas.worldCamera = Camera.main;
        }
    }
    public void SetColorOffset() {
        _normalColor = _offsetColor;
        GetComponent<SpriteRenderer>().color = _offsetColor;
    }

    public void SetColorHighlighted() {
        GetComponent<SpriteRenderer>().color = _highlightColor;
    }

    public void SetColorUnHighlighted() {
        GetComponent<SpriteRenderer>().color = _normalColor;
    }

    public void SetColorBase() {
        _normalColor = _baseColor;
        GetComponent<SpriteRenderer>().color = _baseColor;
    }

    
}

