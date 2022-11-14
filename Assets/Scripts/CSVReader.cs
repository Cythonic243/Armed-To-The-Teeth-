/*
	CSVReader by Dock. (24/8/11)
	http://starfruitgames.com
 
	usage: 
	CSVReader.SplitCsvGrid(textString)
 
	returns a 2D string array. 
 
	Drag onto a gameobject for a demo of CSV parsing.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile;
    public Dictionary<string, string> keyValuePairs;
    public void Start()
    {
        keyValuePairs = SplitCsvGrid(csvFile.text);
        //foreach(var p in keyValuePairs)
        //{
        //    Debug.Log(p.Key + "->" + p.Value);
        //}
    }

    // splits a CSV file into a 2D string array
    static public Dictionary<string, string> SplitCsvGrid(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);

        // finds the max width of row
        int width = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);
            width = Mathf.Max(width, row.Length);
        }

        // creates new 2D string grid to output to
        var keyValuePairs = new Dictionary<string, string>(width + 1);
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = SplitCsvLine(lines[y]);
            var key = row[0].Replace("\"\"", "\"");
            var val = row[1].Replace("\"\"", "\"");
            keyValuePairs[key] = val;
        }

        return keyValuePairs;
    }

    // splits a CSV row 
    static public string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
        @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
        System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }

}