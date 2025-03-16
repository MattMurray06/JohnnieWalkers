using System;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{   
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _highlightColor;
    public int score;
    private String tileText;

    [SerializeField] public float wiggleSpeed = 30f;
    public float wiggleAngle = 3f;
    public bool wiggle = false;

    [SerializeField] private Vector3 _baseScale = new Vector3(1.5127f, 1.5127f, 1.5127f);
    [SerializeField] private Vector3 _highlightScale = new Vector3(2.5f, 2.5f, 2.5f);

    private void Start() {
        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas != null) {
            canvas.worldCamera = Camera.main;
        }
        SetColorBase();
    }

    private void Update()
    {
        if (wiggle == true)
        {
            float angle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAngle;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        } else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void SetColorHighlighted() {
        TextMeshProUGUI textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        if (textMeshPro != null) {
            textMeshPro.color = _highlightColor;
            textMeshPro.transform.localScale = _highlightScale;
        }
    }

    public void SetColorBase() {
        TextMeshProUGUI textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        if (textMeshPro != null) {
            textMeshPro.color = _baseColor;
            textMeshPro.transform.localScale = _baseScale;
        }
    }

    public void SetTileText(String text) {
        tileText = text.ToLower();
        TextMeshProUGUI textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = text.ToLower();
    }
}
