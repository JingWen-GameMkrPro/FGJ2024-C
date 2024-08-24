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
        Option1.text = "牛肉麵";
        Option2.text = "披薩";
        Option3.text = "輕食";
        Question.text = "寶寶，你晚餐想吃甚麼";
        print(Question.text);
        QuestionTitle.text = "你的女友u.3";
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
