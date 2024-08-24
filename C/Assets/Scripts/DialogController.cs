using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogController : MonoBehaviour
{
    public Text Option1;
    public Text Option2;
    public Text Option3;
    public Text Question;
    public Text QuestionTitle;


    public int conversationID = 0;
    private void Awake()
    {

        GameController.Instance.dialogController = this;

    }
    private void Update()
    {

        Option1.text = GameController.Instance.CsvReader.ConversationDictionary[conversationID.ToString()][3];
        Option2.text = GameController.Instance.CsvReader.ConversationDictionary[conversationID.ToString()][4];
        Option3.text = GameController.Instance.CsvReader.ConversationDictionary[conversationID.ToString()][5];
        Question.text = GameController.Instance.CsvReader.ConversationDictionary[conversationID.ToString()][2];
        QuestionTitle.text = GameController.Instance.CsvReader.ConversationDictionary[conversationID.ToString()][1];
    }

    public void SaveData1()
    {
        GameController.Instance.selectionID.Add(GameController.Instance.CsvReader.ConversationDictionary[conversationID.ToString()][6]);
    }
    public void SaveData2()
    {
        GameController.Instance.selectionID.Add(GameController.Instance.CsvReader.ConversationDictionary[conversationID.ToString()][7]);
    }
    public void SaveData3()
    {
        GameController.Instance.selectionID.Add(GameController.Instance.CsvReader.ConversationDictionary[conversationID.ToString()][8]);
    }
}
