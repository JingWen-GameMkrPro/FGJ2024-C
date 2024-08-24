using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class CSVReader
{
    public string fileName = "data.csv";
    private Dictionary<string, string[]> dataDictionary = new Dictionary<string, string[]>();

    public CSVReader()
    {
        string filePath = Path.Combine(Application.dataPath, "Excel", fileName);

        if (File.Exists(filePath))
        {
            ReadCSV(filePath);
        }
        else
        {
            Debug.LogError("找不到指定的 CSV 檔案：" + filePath);
        }
    }

    void ReadCSV(string filePath)
    {
        try
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                bool isFirstLine = true;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    // 忽略第一行
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    string[] values = line.Split(',');

                    if (values.Length > 0)
                    {
                        string id = values[0];
                        dataDictionary[id] = values;
                    }
                }
            }

            // 輸出字典內容以進行檢查
            foreach (var kvp in dataDictionary)
            {
                //Debug.Log("ID: " + kvp.Key + ", Values: " + string.Join(", ", kvp.Value));
            }
        }
        catch (IOException ex)
        {
            //Debug.LogError("讀取 CSV 檔案時發生錯誤：" + ex.Message);
        }
    }
}

