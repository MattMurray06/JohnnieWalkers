using System;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{   
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _baseColor; 
    [SerializeField] private Color _offsetColor;
    [SerializeField] private Color _highlightColor;
    public int score;
    private String tileText;

    [SerializeField] public float wiggleSpeed = 30f;
    public float wiggleAngle = 3f;
    public bool wiggle = false;

    private void Start() {
        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas != null) {
            canvas.worldCamera = Camera.main;
        }
    }

    private void Update()
    {
        /// we Maths
        /// actually Grenvile Parker Turnbull maths
        if (wiggle == true)
        {
            float angle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAngle;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
        } else
        {
            transform.rotation = Quaternion.identity;
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
    public void SetTileText(String text) {
        tileText = text;
        TextMeshProUGUI textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = text;
    }
    
}

