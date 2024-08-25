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

    public List<bool> selectionTag = new List<bool>();

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

    public Dictionary<string, List<string>> questionsAndOptions;
    public Dictionary<string, string> girlInfo;
    void begin()
    {
        if (!girlFriendInfoInstance)
        {
            girlFriendInfoInstance = Instantiate(PrefabGirlFriendInfo, new Vector3(0, 0, 0), Quaternion.identity);
            girlFriendInfoInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
            girlInfoController = girlFriendInfoInstance.GetComponent<GirlInfoController>();
            girlFriendInfoInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(3000, 100);
            girlInfo = new();

            string value = CsvReader.GirlInfoDictionary["likeFood"][UnityEngine.Random.Range(1, 6)].ToString();
            girlInfo.Add("likeFood",value);

            value = CsvReader.GirlInfoDictionary["hateFood"][UnityEngine.Random.Range(1, 6)].ToString();
            girlInfo.Add("hateFood",value);

            value = CsvReader.GirlInfoDictionary["likeAnimal"][UnityEngine.Random.Range(1, 6)].ToString();
            girlInfo.Add("likeAnimal",value);

            value = CsvReader.GirlInfoDictionary["likeMovie"][UnityEngine.Random.Range(1, 6)].ToString();
            girlInfo.Add("likeMovie",value);

            value = CsvReader.GirlInfoDictionary["todayDo"][UnityEngine.Random.Range(1, 6)].ToString();
            girlInfo.Add("todayDo",value);

            value = CsvReader.GirlInfoDictionary["today"][UnityEngine.Random.Range(1, 6)].ToString();
            girlInfo.Add("today",value);


            // 六個key隨機選三個key當問題
            List<string> keys = new List<string> { "likeFood", "hateFood", "likeAnimal", "likeMovie", "todayDo", "today" };
            List<string> selectedKeys = new List<string>();

            while (selectedKeys.Count < 3)
            {
                string key = keys[UnityEngine.Random.Range(0, keys.Count)];
                if (!selectedKeys.Contains(key))
                {
                    selectedKeys.Add(key);
                }
            }

            // 這三個問題隨機選三個value選項
            questionsAndOptions = new Dictionary<string, List<string>>();

            foreach (string key in selectedKeys)
            {
                List<string> options = new List<string>();
                string correctAnswer = girlInfo[key];
                options.Add(correctAnswer);

                // 添加其他隨機選項直到有三個選項
                while (options.Count < 3)
                {
                    string option = CsvReader.GirlInfoDictionary[key][UnityEngine.Random.Range(1, 6)].ToString();
                    if (!options.Contains(option))
                    {
                        options.Add(option);
                    }
                }

                // 打亂選項順序
                for (int i = options.Count - 1; i > 0; i--)
                {
                    int j = UnityEngine.Random.Range(0, i + 1);
                    string temp = options[i];
                    options[i] = options[j];
                    options[j] = temp;
                }

                questionsAndOptions[key] = options;
            }


            girlFriendInfoInstance.transform.GetComponent<Text>().text = $"女友的資訊如右，請務必記好.....................................................：女友喜歡吃{girlInfo["likeFood"]}和看{girlInfo["likeMovie"]}電影；她今天去做了{girlInfo["todayDo"]}和動物，尤其喜歡{girlInfo["likeAnimal"]}。她討厭{girlInfo["hateFood"]}和擁擠的環境，不喜歡早起。今天是{girlInfo["today"]}她對咖啡過敏，但喜歡喝果汁。她喜歡乾淨整潔的空間，也很注重生活品質，享受簡單卻有品味的生活。\r\n";
        }

        if(girlInfoController.isFinish)
        {
            CurrentGameState = GameState.Conversation;
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

            //隨機產生問題
            var id = UnityEngine.Random.Range(1, CsvReader.ConversationDictionary.Count + 1);
            dialogController.conversationID = id;

        }

        if (selectionTag.Count == currentLevel)
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
