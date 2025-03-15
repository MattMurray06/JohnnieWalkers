using UnityEngine;
using System.Collections.Generic;

public class CSVreader : MonoBehaviour {
    public TextAsset csvFile; // Assign in Inspector
    
    void Start() {
        //string[,] parsedData = SimpleCsvParse(csvFile);
        //LogData(parsedData);
    }
    public static string[][] SimpleCsvParse(TextAsset file) {
        List<string[]> data = new List<string[]>();
        string[] lines = file.text.Split(
            new[] { "\r\n", "\r", "\n" }, 
            System.StringSplitOptions.RemoveEmptyEntries
        );
        
        foreach(string line in lines) {
            if(string.IsNullOrWhiteSpace(line)) continue;
            string[] fields = line.Trim().Split(',');
            data.Add(fields);
        }

        return data.ToArray();
    }
    

    void LogData(string[,] data) {
        int rowCount = data.GetLength(0);
        int colCount = data.GetLength(1);
        
        for(int row = 0; row < rowCount; row++) {
            string[] rowValues = new string[colCount];
            for(int col = 0; col < colCount; col++) {
                rowValues[col] = data[row, col] ?? "null";
            }
            Debug.Log(string.Join(" | ", rowValues));
        }
    }
}
