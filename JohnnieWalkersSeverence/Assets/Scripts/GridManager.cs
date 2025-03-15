using UnityEngine;
using System.Collections.Generic;
using System;

public class GridManager : MonoBehaviour
{
    public int _width;
    public int _height;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Transform _cameraFollowerTransform;
    [SerializeField] private Transform _cameraLeaderTransform;

    private bool mouseDown = false;
    private Vector2Int start;
    private Vector2Int end;
    private List<Vector2Int> boxes = new List<Vector2Int>();
    private Dictionary<Vector2Int, GameObject> tileStorage = new Dictionary<Vector2Int, GameObject>();
    [SerializeField] private TextAsset csvFile;

    void Start() {

        GenerateGrid();
        _cameraFollowerTransform.position = new Vector3((_width - 1) / 2f, (_height - 1) / 2f, -10);
        _cameraLeaderTransform.position = new Vector3((_width - 1) / 2f, (_height - 1) / 2f, -10);
    }

    void Update() {
        Vector2Int mouseGridPos = GetMouseGridPosition();

        if (Input.GetMouseButtonDown(0) && mouseDown == false) {
            mouseDown= true;
            start = mouseGridPos;
            boxes.Clear();
        }

        if (Input.GetMouseButtonUp(0) && mouseDown == true) {
            mouseDown = false;
            end = mouseGridPos;
            
        }
        UnHighlightAllBoxes();

        if (mouseDown == true){
            CalculateBoxes(mouseGridPos);
            HighlightBoxes();
        }
    }

    void CalculateBoxes(Vector2Int mouseGridPos){
        boxes.Clear();
        int stepx = start.x < mouseGridPos.x ? 1 : -1;
        int stepy = start.y < mouseGridPos.y ? 1 : -1;
        for (int i = 0; i < Math.Abs(mouseGridPos.x - start.x) + 1; i++){
            for (int j = 0; j < Math.Abs(mouseGridPos.y - start.y) + 1; j++){
                boxes.Add(new Vector2Int(start.x + i * stepx, start.y + j * stepy));
            }
        }
    }
    Vector2Int GetMouseGridPosition() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int mouseGridPos = new Vector2Int(Mathf.FloorToInt(mousePos.x + 0.5f), Mathf.FloorToInt(mousePos.y + 0.5f));
        return mouseGridPos;
    }

    void UnHighlightAllBoxes(){
        foreach (var tile in tileStorage.Values) {
            tile.GetComponent<Tile>().SetColorUnHighlighted();
        }
    }

    void HighlightBoxes(){
        foreach (Vector2Int boxPos in boxes){
            if (tileStorage.ContainsKey(boxPos)){
                tileStorage[boxPos].GetComponent<Tile>().SetColorHighlighted();
            }
        }
    }
    void GenerateGrid() {

        //Load CSV file using function
        // String[][] namesArray = SimpleCsvParse(csvFile); :: String[][]   
        _height = namesArray.Length;
        _width = namesArray[0].Length;

        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {

                CreateTile(x, y, namesArray[y][x]);
            }
        }

    }

    void CreateTile(int x, int y, String name) {
        GameObject tileObj = Instantiate(_tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
        tileObj.name = "Tile_" + x + "_" + y;

        tileStorage.Add(new Vector2Int(x, y), tileObj);
        tileObj.GetComponent<Tile>().SetTileText(name);
    }
}
