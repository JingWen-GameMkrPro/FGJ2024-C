using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIControllor : MonoBehaviour
{
    [NonSerialized]public int bossHp;
    [NonSerialized]public int playerHp;
    public Slider slidBossHp;
    public Slider slidPlayerHp;
    public Button btnSetting;
    public Button btnCloseSetting;
    public GameObject settingView;

    public String menuIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        settingView.SetActive(false);
        btnCloseSetting.onClick.AddListener(OnBtnCloseSettingClick);
        btnSetting.onClick.AddListener(OnBtnSettingClick);
        playerHp = (int)slidPlayerHp.maxValue;
        bossHp = (int)slidBossHp.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        playerHp = (int)slidPlayerHp.value;
        bossHp = (int)slidBossHp.value;
    }

    public void OnBtnSettingClick()
    {
        settingView.SetActive(true);
    }
    public void OnBtnCloseSettingClick()
    {
        settingView.SetActive(false);
    }
}
