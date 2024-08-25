using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControllor : MonoBehaviour
{
    public Slider slidBossHp;           //Boss血量條UI
    public Slider slidPlayerHp;         //玩家血量條UI
    public Slider slidVoice;
    public Button btnSetting;           //設定按鈕
    public Button btnCloseSetting;      //關閉設定按鈕
    public Button btnPause;             //暫停按鈕
    public Button btnContinue;          //繼續遊戲按鈕
    public Button btnExit;              //離開遊戲按鈕

    private AudioSource audio;
    public GameObject settingView;
    public GameObject pauseView;
    
    // Start is called before the first frame update
    void Start()
    {
        //slidBossHp.value = GameController.Instance.monsterController.currentHP;
        //slidBossHp.maxValue = GameController.Instance.monsterController.maxBossHP;
        
        //GameController.Instance.monsterController.currentHP

        audio = GetComponent<AudioSource>();
        audio.volume = slidVoice.value;
        settingView.SetActive(false);
        pauseView.SetActive(false);
        
        btnSetting.onClick.AddListener(OnBtnSettingClick);
        btnCloseSetting.onClick.AddListener(OnBtnCloseSettingClick);
        btnPause.onClick.AddListener(OnBtnPauseClick);
        btnContinue.onClick.AddListener(OnBtnContinueClick);
        btnExit.onClick.AddListener(OnBtnExitClick);
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = slidVoice.value;
    }

    public void OnBtnSettingClick()
    {
        settingView.SetActive(true);
    }
    public void OnBtnCloseSettingClick()
    {
        settingView.SetActive(false);
    }
    public void OnBtnPauseClick()
    {
        Time.timeScale = 0;
        pauseView.SetActive(true);
    }
    public void OnBtnContinueClick()
    {
        Time.timeScale = 1;
        pauseView.SetActive(false);
    }
    public void OnBtnExitClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
