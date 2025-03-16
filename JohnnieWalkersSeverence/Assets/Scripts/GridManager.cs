using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public int _width;
    public int _height;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Transform _cameraFollowerTransform;
    [SerializeField] private Transform _cameraLeaderTransform;

    private Vector2Int start;
    private Vector2Int end;
    private List<Vector2Int> boxes = new List<Vector2Int>();
    private Dictionary<Vector2Int, GameObject> tileStorage = new Dictionary<Vector2Int, GameObject>();
    [SerializeField] private TextAsset csvFileInitial;
    [SerializeField] private TextAsset scoreFileInitial;
    [SerializeField] private TMP_Text _scoreUI;
    [SerializeField] private TMP_Text _labelUI;
    [SerializeField] private TMP_Text _popUpBox;
    [SerializeField] private TMP_Text _levelPopUp;

    // Seed word to generate with - WE DON'T USE THIS YET, MAKE IN USE
    private string seed = "Finance";
    private Color light_blue = new Color(162f / 255f, 246f / 255f, 244f / 255f);
    private int RoundScore = 0;
    private bool mouseDown = false;
    private bool out_of_time = false;

    public NextLevelManager nextLevelManager;

    void Start() 
    {
        GenerateGrid(csvFileInitial, scoreFileInitial);
        _cameraFollowerTransform.position = new Vector3((_width - 1) / 2f, (_height - 1) / 2f, -10);
        _cameraLeaderTransform.position = new Vector3((_width - 1) / 2f, (_height - 1) / 2f, -10);
        _labelUI.color = light_blue;
        _scoreUI.color = light_blue;
        
        _labelUI.text = "Level: " + seed;
        _popUpBox.text = "";
        HideLevelPopUp();
        Timer.OnTimerEnd += TimesUp;
    }

    void Update() {
        // When mouse is initially clicked
        ////////////////////////////////////// DRAGGING-TO-CREATE-BOX CODE 
        Vector2Int mouseGridPos = GetMouseGridPosition();
        if (Input.GetMouseButtonDown(0) && mouseDown == false && out_of_time == false) {
            mouseDown = true;
            start = mouseGridPos;
            boxes.Clear();
        }

        // When mouse is released
        if (Input.GetMouseButtonUp(0) && mouseDown == true && out_of_time == false) {
            mouseDown = false;
            end = mouseGridPos;
            CalculateScore();

            _scoreUI.text = "Score: " + RoundScore.ToString();
        }
        ////////////////////////////////////////// 
        UnHighlightAndRemoveWiggleAllBoxes();

        /////////////////////////////////////////// CALCULATES BOXES TO HIGHLIGHT / WIGGLE
        if (mouseDown == true && out_of_time == false)
        {
            CalculateBoxes(mouseGridPos);
            HighlightBoxes();
        }


        //////////////////////////////// ONCE TIME OUT, EITHER NEXT LEVEL OR QUIT
        if (out_of_time == true)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                MakeNextLevel();
            } else if (Input.GetKeyDown(KeyCode.N))
            {
                QuitApplication();
            }
        }
    }

    void MakeNextLevel()
    {
        seed = "AI";
        _labelUI.text = "Level: " + seed;
        _scoreUI.text = "Score: 0";
        // insert new words
        HideLevelPopUp();
        
    }
    void CalculateScore()
    {
        int sub_score = 0;
        //// CHANGE THIS CALCULATION IF YOU WANT
        foreach (Vector2Int boxPos in boxes)
        {
            sub_score += tileStorage[boxPos].GetComponent<Tile>().score;
        }
        //// SO I'VE INSENTIVISED FINDING ALL 4 OF THE RELATED FINANCE TERMS. NONE IS -2 POINTS, ONLY 1 IS A -1 !!!PENALTY!!!, 2 IS NEUTRAL, 3 IS + 2, 4 IS + 4
        /// PLEASE PLEASE PLEASE CHANGE THIS IN THE MORNING, JUST VERY ROUGHS
        if (sub_score == 0)
        {
            ShowNope();
            RoundScore -= 2;
        }
        else if (sub_score == 1)
        {
            RoundScore -= 1;
        }
        else if (sub_score == 2)
        {
            // Do nothing, left in for yous
        }
        else if (sub_score == 3)
        {
            RoundScore += 2;
        }
        else if (sub_score == 4)
        {
            RoundScore += 4;
        }
        // Round score declared above so no need to return.
    }
    
    void ShowLevelPopUp()
    {
        _levelPopUp.text = "Congratulations, you made it onto the next level. Start Next Level (y) or Quit (n).";
    }
    void HideLevelPopUp()
    {
        _levelPopUp.text = "";
    }
    void ShowNope()
    {
        _popUpBox.text = "Nope";
        Invoke("ClearNope", 1);
    }
    void ClearNope()
    {
        _popUpBox.text = "";
    }
    void TimesUp()
    {
        /// This works!!!! That was entirely unexpected. Unity working first try? improbable. Wohooo
        
        // sorry for the jank implementation, it works
        // as microsoft says about all it's products, if it ain't broke, break it
        out_of_time = true;

        Timer.OnTimerEnd -= TimesUp;
        Debug.Log("First Step reached");

        ShowLevelPopUp();
        
    }


    void CalculateBoxes(Vector2Int mouseGridPos)
    {
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

    void UnHighlightAndRemoveWiggleAllBoxes(){
        foreach (var tile in tileStorage.Values) {
            tile.GetComponent<Tile>().SetColorUnHighlighted();
            tile.GetComponent<Tile>().wiggle = false;
        }
    }

    void HighlightBoxes(){
        foreach (Vector2Int boxPos in boxes){
            if (tileStorage.ContainsKey(boxPos)){
                //tileStorage[boxPos].GetComponent<Tile>().SetColorHighlighted();
                tileStorage[boxPos].GetComponent<Tile>().wiggle = true;
                //tileStorage[boxPos].GetComponent<Tile>().

            }
        }
    }
    void GenerateGrid(TextAsset csvFile, TextAsset scoreFile) {

        //Load CSV file using function
        String[][] namesArray = CSVreader.SimpleCsvParse(csvFile);
        String[][] scoreArray = CSVreader.SimpleCsvParse(scoreFile);

        _height = namesArray.Length;
        _width = namesArray[0].Length;

        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {

                CreateTile(x, _height - y - 1, namesArray[y][x], int.Parse(scoreArray[y][x]));
            }
        }

        //PrintNamesArray(namesArray);
    }

    void CreateTile(int x, int y, String name, int score) {
        GameObject tileObj = Instantiate(_tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
        tileObj.name = "Tile_" + x + "_" + y;

        tileStorage.Add(new Vector2Int(x, y), tileObj);
        tileObj.GetComponent<Tile>().SetTileText(name);
        tileObj.GetComponent<Tile>().score = score;
    }

    void PrintNamesArray(String[][] namesArray) {
        for (int y = 0; y < namesArray.Length; y++) {
            for (int x = 0; x < namesArray[y].Length; x++) {
                Debug.Log($"namesArray[{y}][{x}] = {namesArray[y][x]}");
            }
        }
    }

    void QuitApplication()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

