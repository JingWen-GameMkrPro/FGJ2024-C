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
            Debug.LogError("�䤣����w�� CSV �ɮסG" + filePath);
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

                    // �����Ĥ@��
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

            // ��X�r�夺�e�H�i���ˬd
            foreach (var kvp in dataDictionary)
            {
                //Debug.Log("ID: " + kvp.Key + ", Values: " + string.Join(", ", kvp.Value));
            }
        }
        catch (IOException ex)
        {
            //Debug.LogError("Ū�� CSV �ɮ׮ɵo�Ϳ��~�G" + ex.Message);
        }
    }
}

