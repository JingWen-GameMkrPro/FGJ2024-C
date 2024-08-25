using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class CSVReader
{
    public string fileConversationName = "conversation.csv";
    public string fileConversationResultName = "conversationResult.csv";
    public string fileGirlInfoName = "girlInfo.csv";

    public Dictionary<string, string[]> ConversationDictionary = new Dictionary<string, string[]>();
    public Dictionary<string, string[]> ConversationResultDictionary = new Dictionary<string, string[]>();
    public Dictionary<string, string[]> GirlInfoDictionary = new Dictionary<string, string[]>();

    public CSVReader()
    {
        LoadCSV(fileConversationName, out ConversationDictionary);
        LoadCSV(fileGirlInfoName, out GirlInfoDictionary);
    }

    public void LoadCSV(string fileName, out Dictionary<string, string[]>  dictionary)
    {
        string filePath = Path.Combine(Application.dataPath, "Excel", fileName);
        dictionary = new();
        if (File.Exists(filePath))
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader reader = new StreamReader(fileStream, System.Text.Encoding.UTF8))

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
                            dictionary[id] = values;

                        }
                    }
                }

                // 輸出字典內容以進行檢查
                foreach (var kvp in dictionary)
                {
                    Debug.Log("ID: " + kvp.Key + ", Values: " + string.Join(", ", kvp.Value));


                }

            }


            catch (IOException ex)
            {
                Debug.LogError("讀取 CSV 檔案時發生錯誤：" + ex.Message);
            }
        }
        else
        {
            Debug.LogError("找不到指定的 CSV 檔案：" + filePath);
        }
    }
}

