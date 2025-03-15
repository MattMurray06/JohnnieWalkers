using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Transform _cameraTransform;

    private Dictionary<Vector2Int, GameObject> tileStorage = new Dictionary<Vector2Int, GameObject>();

    void Start() {
        GenerateGrid();
    }

    void Update() {
        CheckMouseOver();
    }

    void CheckMouseOver() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int mouseGridPos = new Vector2Int(Mathf.FloorToInt(mousePos.x), Mathf.FloorToInt(mousePos.y));

        // Define the range around the mouse position
        int range = 1;

        // Loop through the tiles around the mouse position
        // Reset all tiles to their base color
        foreach (var tile in tileStorage.Values) {
            tile.GetComponent<Tile>().SetColorUnHighlighted();
        }

        // Highlight the tiles around the mouse position
        for (int x = -range; x <= range; x++) {
            for (int y = -range; y <= range; y++) {
                Vector2Int gridPos = new Vector2Int(mouseGridPos.x + x, mouseGridPos.y + y);
                if (tileStorage.ContainsKey(gridPos)) {
                    tileStorage[gridPos].GetComponent<Tile>().SetColorHighlighted();
                }
            }
        }
    }

    void GenerateGrid() {
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                GameObject tileObj = Instantiate(_tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tileObj.name = "Tile_" + x + "_" + y;

                if ((x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0)) {
                    tileObj.GetComponent<Tile>().SetColorOffset();
                } else {
                    tileObj.GetComponent<Tile>().SetColorBase();
                }

                tileStorage.Add(new Vector2Int(x, y), tileObj);
            }
        }

        _cameraTransform.position = new Vector3((_width - 1) / 2f, (_height - 1) / 2f, -10);
    }
}
