using UnityEngine;
using System.Collections.Generic;

public class CsvParser : MonoBehaviour {
    public TextAsset csvFile; // Assign in Inspector
    
    void Start() {
        string[,] parsedData = SimpleCsvParse(csvFile);
        //LogData(parsedData);
    }

    public static string[,] SimpleCsvParse(TextAsset file) {
        List<string[]> data = new List<string[]>();
        string[] lines = file.text.Split(
            new[] { "\r\n", "\r", "\n" }, 
            System.StringSplitOptions.RemoveEmptyEntries
        );
        
        // First pass: read all rows and track maximum columns
        int maxColumns = 0;
        foreach(string line in lines) {
            if(string.IsNullOrWhiteSpace(line)) continue;
            string[] fields = line.Trim().Split(',');
            data.Add(fields);
            if(fields.Length > maxColumns) 
                maxColumns = fields.Length;
        }

        // Create 2D array with proper dimensions
        string[,] result = new string[data.Count, maxColumns];
        
        // Second pass: fill the 2D array
        for(int row = 0; row < data.Count; row++) {
            for(int col = 0; col < data[row].Length; col++) {
                result[row, col] = data[row][col];
            }
            // Pad remaining columns with empty strings if needed
            for(int col = data[row].Length; col < maxColumns; col++) {
                result[row, col] = "";
            }
        }
        return result;
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
