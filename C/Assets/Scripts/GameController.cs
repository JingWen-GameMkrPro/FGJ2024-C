using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject PrefabGirlFriendInfo;
    public GameObject PrefabPlayer;
    public GameObject PrefabBoss;

    GameObject girlFriendInfoInstance;
    GirlInfoController girlInfoController;
    GameObject playerInstance;
    GameObject bossInstance;


    public static GameController Instance { get; private set; }

    public GameState CurrentGameState = GameState.Begin;
    public enum GameState
    {
        Begin,
        Start,
        Fight,
        Conversation,
        End,
    }

    public Text HintText;
    public Text comboText;
    public int combo = 0;

    [HideInInspector]
    public CSVReader csvReader = new();

    public int currentLevel = 111;

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

    // Update is called once per frame
    void Update()
    {
        switch(CurrentGameState)
        {
            case GameState.Begin:
                begin();
                break;
           case GameState.Start:
                loadLevel();
                break;
            case GameState.Fight:
                updateCombo();
                break;
            case GameState.Conversation:
                break;
            case GameState.End:
                break;
        }
    }

    void begin()
    {
        if(!girlFriendInfoInstance)
        {
            girlFriendInfoInstance = Instantiate(PrefabGirlFriendInfo, new Vector3(0, 0, 0), Quaternion.identity);
            girlFriendInfoInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
            girlInfoController = girlFriendInfoInstance.GetComponent<GirlInfoController>();
            girlFriendInfoInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(3000, 100);
        }

        if(girlInfoController.isFinish)
        {
            CurrentGameState = GameState.Start;
        }
    }

    void loadLevel()
    {
        initialLevel();
        compundLevel();
        createPlayer();
        createBoss();
    }


    void showHint()
    {

    }

    void updateCombo()
    {
        comboText.text = combo.ToString();
    }

    //初始化關卡資料
    void initialLevel()
    {
        showHint("歡迎來到這一關", 2f);
        showHint("這一關非常難", 2f);
        CurrentGameState = GameState.Fight;

    }



    //根據資料決定關卡難易度
    void compundLevel()
    {

    }

    //創建怪物
    void createBoss()
    {
        bossInstance = Instantiate(PrefabBoss, new Vector3(0, 0, 50), Quaternion.identity);

    }

    //創建玩家
    void createPlayer()
    {
        if(!playerInstance)
        {
            playerInstance = Instantiate(PrefabPlayer, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    void showHint(string hint, float time)
    {
        HintText.text = hint;
        StartCoroutine(HintTimerCoroutine(time));
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
