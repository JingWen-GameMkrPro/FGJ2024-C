using Fungus;
using System;
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
    public GameObject PrefabDialogBox;

    [HideInInspector]
    GameObject dialogBoxInstance;


    GameObject girlFriendInfoInstance;
    GirlInfoController girlInfoController;
    [HideInInspector]
    public GameObject playerInstance;
    [HideInInspector]
    public PlayerController playerController;
    [HideInInspector]
    public GameObject monsterInstance;
    [HideInInspector]
    public MonsterController monsterController;

    [HideInInspector]
    public LevelDynamicAttribute levelDynamicAttribute;

    [HideInInspector]
    public DialogController dialogController;

    public static GameController Instance { get; private set; }

    public GameState CurrentGameState = GameState.Conversation;
    public enum GameState
    {
        Begin,
        Start,
        Fight,
        Conversation,
        End,
    }

    public List<string> selectionID = new List<string>();

    //UI
    public Text HintText;
    public Text comboText;
    public int combo = 0;



    [HideInInspector]
    public CSVReader CsvReader = new();

    public int currentLevel = 0;
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
                createConversation();
                break;
            case GameState.End:
                break;
        }
    }
    
    
    void begin()
    {
        if (!girlFriendInfoInstance)
        {
            girlFriendInfoInstance = Instantiate(PrefabGirlFriendInfo, new Vector3(0, 0, 0), Quaternion.identity);
            girlFriendInfoInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
            girlInfoController = girlFriendInfoInstance.GetComponent<GirlInfoController>();
            girlFriendInfoInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(3000, 100);
            List<string> girlInfo = new List<string>();

            string value = CsvReader.GirlInfoDictionary["likeFood"][UnityEngine.Random.Range(1, 6)].ToString();
            girlInfo.Add(value);

            value = CsvReader.GirlInfoDictionary["hateFood"][UnityEngine.Random.Range(1, 6)].ToString();
            girlInfo.Add(value);

            value = CsvReader.GirlInfoDictionary["likeAnimal"][UnityEngine.Random.Range(1, 6)].ToString();
            girlInfo.Add(value);

            value = CsvReader.GirlInfoDictionary["likeMovie"][UnityEngine.Random.Range(1, 6)].ToString();
            girlInfo.Add(value);

            value = CsvReader.GirlInfoDictionary["todayDo"][UnityEngine.Random.Range(1, 6)].ToString();
            girlInfo.Add(value);

            value = CsvReader.GirlInfoDictionary["today"][UnityEngine.Random.Range(1, 6)].ToString();
            girlInfo.Add(value);


            girlFriendInfoInstance.transform.GetComponent<Text>().text = $"女友的資訊如右，請務必記好.....................................................：女友喜歡吃{girlInfo[0]}和看{girlInfo[3]}電影；她今天去做了{girlInfo[4]}和動物，尤其喜歡{girlInfo[2]}。她討厭{girlInfo[1]}和擁擠的環境，不喜歡早起。今天是{girlInfo[5]}她對咖啡過敏，但喜歡喝果汁。她喜歡乾淨整潔的空間，也很注重生活品質，享受簡單卻有品味的生活。\r\n";
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
        createMonster();
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
        currentLevel++;
        showHint("歡迎來到這一關", 2f);
        showHint("這一關非常難", 2f);
        CurrentGameState = GameState.Fight;
    }

    //根據資料決定關卡難易度
    void compundLevel()
    {
        levelDynamicAttribute = new();
    }

    void createConversation()
    {
        if (!dialogBoxInstance)
        {
            currentLevel++; //Test
            dialogBoxInstance = Instantiate(PrefabDialogBox, new Vector3(0, 0, 50), Quaternion.identity);

            var id = UnityEngine.Random.Range(1, CsvReader.ConversationDictionary.Count + 1);
            dialogController.conversationID = 2;

        }

        if (selectionID.Count == currentLevel)
        {
            CurrentGameState = GameState.End;
        }
    }

    //創建怪物
    void createMonster()
    {
        if (!monsterInstance)
        {
            monsterInstance = Instantiate(PrefabBoss, new Vector3(0, 0, 50), Quaternion.identity);
            monsterController = monsterInstance.GetComponent<MonsterController>();
        }

    }

    //創建玩家
    void createPlayer()
    {
        if(!playerInstance)
        {
            playerInstance = Instantiate(PrefabPlayer, new Vector3(0, 0, 0), Quaternion.identity);
            playerController = playerInstance.GetComponent<PlayerController>();
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
