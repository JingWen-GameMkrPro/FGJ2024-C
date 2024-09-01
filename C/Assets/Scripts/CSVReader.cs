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
                            dictionary[id] = values;
                        }
                    }
                }

                // ��X�r�夺�e�H�i���ˬd
                foreach (var kvp in dictionary)
                {
                    Debug.Log("ID: " + kvp.Key + ", Values: " + string.Join(", ", kvp.Value));
                }
            }
            catch (IOException ex)
            {
                Debug.LogError("Ū�� CSV �ɮ׮ɵo�Ϳ��~�G" + ex.Message);
            }
        }
        else
        {
            Debug.LogError("�䤣����w�� CSV �ɮסG" + fileName);
        }
    }
}
