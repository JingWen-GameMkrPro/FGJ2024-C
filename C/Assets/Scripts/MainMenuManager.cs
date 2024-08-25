using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{
    public Button btnPlay;
    public Button btnExit;
    
    // Start is called before the first frame update
    void Start()
    {
        btnPlay.onClick.AddListener(OnBtnPlayClick);
        btnExit.onClick.AddListener(OnBtnExitClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBtnPlayClick()
    {
        SceneManager.LoadScene("S0");
    }
    
    void OnBtnExitClick()
    {
        Application.Quit();
    }
}
