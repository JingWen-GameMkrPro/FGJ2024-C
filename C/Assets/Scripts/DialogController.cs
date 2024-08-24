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


    private void Awake()
    {
        GameController.Instance.dialogController = this;


    }
    private void Update()
    {
        Option1.text = "������";
        Option2.text = "����";
        Option3.text = "����";
        Question.text = "�_�_�A�A���\�Q�Y�ƻ�";
        print(Question.text);
        QuestionTitle.text = "�A���k��u.3";
    }

    public void SaveData1()
    {
        GameController.Instance.selectionID.Add("1");
    }
    public void SaveData2()
    {
        GameController.Instance.selectionID.Add("2");
    }
    public void SaveData3()
    {
        GameController.Instance.selectionID.Add("3");
    }
}
