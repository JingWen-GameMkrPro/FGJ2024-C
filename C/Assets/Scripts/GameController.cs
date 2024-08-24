using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    void Awake()
    {
        // 確保只有一個實例存在
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持物件在場景切換時不被銷毀
        }
        else
        {
            Destroy(gameObject); // 如果已經有一個實例存在，銷毀新的物件
        }
    }

    public Text HintText;
    public Text comboText;
    public int combo = 0;

    [HideInInspector]
    public CSVReader csvReader = new();

    public int currentLevel = 111;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        updateCombo();
    }

    void LoadLevel(int level)
    {
        InitialLevel();
        CompundLevel();
        CreatePlayer();
        CreateBoss();
    }


    void ShowHint()
    {

    }

    void updateCombo()
    {
        comboText.text = combo.ToString();
    }

    //初始化關卡資料
    void InitialLevel()
    {
        showHint("Hello");
    }

    

    //根據資料決定關卡難易度
    void CompundLevel()
    {

    }

    //創建怪物
    void CreateBoss()
    {

    }

    //創建玩家
    void CreatePlayer()
    {

    }

    void showHint(string hint)
    {
        HintText.text = hint;
        StartCoroutine(HintTimerCoroutine(3f));
    }

    private IEnumerator HintTimerCoroutine(float duration)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        HintText.text = "";
    }



}
