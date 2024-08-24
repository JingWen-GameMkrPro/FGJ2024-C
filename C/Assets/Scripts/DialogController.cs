using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
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
