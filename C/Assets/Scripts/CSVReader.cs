using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CSVReader : MonoBehaviour
{
    public string fileConversationName = "conversation";
    public string fileConversationResultName = "conversationResult";
    public string fileGirlInfoName = "girlInfo";

    public Dictionary<string, string[]> ConversationDictionary = new Dictionary<string, string[]>();
    public Dictionary<string, string[]> ConversationResultDictionary = new Dictionary<string, string[]>();
    public Dictionary<string, string[]> GirlInfoDictionary = new Dictionary<string, string[]>();

    void Start()
    {
        LoadCSV(fileConversationName, out ConversationDictionary);
        LoadCSV(fileGirlInfoName, out GirlInfoDictionary);
    }

    public void LoadCSV(string fileName, out Dictionary<string, string[]> dictionary)
    {
        dictionary = new Dictionary<string, string[]>();
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);

        if (csvFile != null)
        {
            try
            {
                using (StringReader reader = new StringReader(csvFile.text))
                {
                    bool isFirstLine = true;

                    while (reader.Peek() != -1)
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
            Debug.LogError("找不到指定的 CSV 檔案：" + fileName);
        }
    }
}
