using UnityEngine;
using System.Collections.Generic;

public class CsvParser : MonoBehaviour {
    public TextAsset csvFile; // Assign in Inspector
    
    void Start() {
        List<string[]> parsedData = SimpleCsvParse(csvFile);
        LogData(parsedData);
    }

    public static List<string[]> SimpleCsvParse(TextAsset file) {
        List<string[]> data = new List<string[]>();
        string[] lines = file.text.Split('\n');
        
        foreach(string line in lines) {
            if(!string.IsNullOrWhiteSpace(line)) {
                data.Add(line.Trim().Split(','));
            }
        }
        return data;
    }

    void LogData(List<string[]> data) {
        foreach(string[] row in data) {
            Debug.Log(string.Join(" | ", row));
        }
    }
}
