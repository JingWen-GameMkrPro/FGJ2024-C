using Fungus;
using System;
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
    string questionKey = "";

    public int conversationID = 0;
    private void Awake()
    {

        GameController.Instance.dialogController = this;

    }
    private void Update()
    {
        switch(GameController.Instance.currentLevel)
        {
            case 1:
                int count = 1;
                foreach (var question in GameController.Instance.questionsAndOptions)
                {
                    if(count != GameController.Instance.currentLevel)
                    {
                        count++;
                        continue;
                    }
                    Option1.text = question.Value[0];
                    Option2.text = question.Value[1];
                    Option3.text = question.Value[2];
                    Question.text = GameController.Instance.CsvReader.ConversationDictionary[question.Key][3];
                    QuestionTitle.text = GameController.Instance.CsvReader.ConversationDictionary[question.Key][2];
                    questionKey = question.Key;

                    count++;
                }
                break;
            case 2:
                count = 1;
                foreach (var question in GameController.Instance.questionsAndOptions)
                {
                    if (count != GameController.Instance.currentLevel)
                    {
                        count++;
                        continue;
                    }
                    Option1.text = question.Value[0];
                    Option2.text = question.Value[1];
                    Option3.text = question.Value[2];
                    Question.text = GameController.Instance.CsvReader.ConversationDictionary[question.Key][3];
                    QuestionTitle.text = GameController.Instance.CsvReader.ConversationDictionary[question.Key][2];
                    questionKey = question.Key;

                    count++;
                }
                break;
            case 3:
                count = 1;
                foreach (var question in GameController.Instance.questionsAndOptions)
                {
                    if (count != GameController.Instance.currentLevel)
                    {
                        count++;
                        continue;
                    }
                    Option1.text = question.Value[0];
                    Option2.text = question.Value[1];
                    Option3.text = question.Value[2];
                    Question.text = GameController.Instance.CsvReader.ConversationDictionary[question.Key][3];
                    QuestionTitle.text = GameController.Instance.CsvReader.ConversationDictionary[question.Key][2];
                    questionKey = question.Key;

                    count++;
                }
                break;
        }


    }

    public void SaveData1()
    {
        if (Option1.text != GameController.Instance.girlInfo[questionKey])
        {
            GameController.Instance.selectionTag.Add(false);
        }
        else
        {
            GameController.Instance.selectionTag.Add(true);
        }
        Destroy(gameObject);
    }
    public void SaveData2()
    {
        if (Option2.text != GameController.Instance.girlInfo[questionKey])
        {
            GameController.Instance.selectionTag.Add(false);
        }
        else
        {
            GameController.Instance.selectionTag.Add(true);
        }
        Destroy(gameObject);
    }
    public void SaveData3()
    {
        if (Option3.text != GameController.Instance.girlInfo[questionKey])
        {
            GameController.Instance.selectionTag.Add(false);
        }
        else
        {
            GameController.Instance.selectionTag.Add(true);
        }

        Destroy(gameObject);
    }
}
