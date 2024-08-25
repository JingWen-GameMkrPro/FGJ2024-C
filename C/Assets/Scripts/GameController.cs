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
    public GameObject PrefabBoss1;
    public GameObject PrefabBoss2;
    public Slider BossSlider;
    public GameObject PrefabDialogBox;
    public SpriteRenderer spriteRenderer;

    [HideInInspector]
    public GameObject dialogBoxInstance;


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
        GameEnd,
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
                updateUI();
                checkFight();
                break;
            case GameState.Conversation:
                createConversation();
                break;
            case GameState.End:
                checkEnd();
                break;
        }
    }

    public Dictionary<string, List<string>> questionsAndOptions;
    public Dictionary<string, string> girlInfo;
    void checkFight()
    {
        if(monsterController.currentHP <=0)
        {

            Destroy(monsterInstance);
            Destroy(playerInstance);

            if(currentLevel == 3)
            {
                CurrentGameState = GameState.GameEnd;
            }
            else
            {
                CurrentGameState = GameState.Conversation;
            }
        }
    }

    void checkEnd()
    {
        CurrentGameState = GameState.Start;
    }

    void begin()
    {
        levelType = LevelDynamicAttribute.LevelType.Normal;


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
            CurrentGameState = GameState.Start;
        }
    }

    void loadLevel()
    {
        

        initialLevel();
        compundLevel();
        createPlayer();
        createMonster();
        createEffect();
    }

    void createEffect()
    {
        if(levelType == LevelDynamicAttribute.LevelType.Normal)
        {
            showHint("跳躍到怪物頭頂造成傷害", 2f);
            return;
        }
        switch(UnityEngine.Random.Range(1, 4))
        {
            case 1:
                if(levelType < LevelDynamicAttribute.LevelType.Normal)
                {
                    showHint("這一關有「熱戀」正面效果", 2f);
                    //Player可以放火球
                    StartCoroutine(ChangeColorOverTime(2f, Color.red));

                }
                else
                {
                    showHint("這一關有「毒雞湯」負面效果", 2f);
                    //Boss可以放陷阱
                    monsterController.CanPoison = true;
                    StartCoroutine(ChangeColorOverTime(2f, new Color(0.5f, 0f, 0.5f)));

                }
                break;
            case 2:
                if (levelType < LevelDynamicAttribute.LevelType.Normal)
                {
                    showHint("這一關有「戀愛Punch」正面效果", 2f);
                    StartCoroutine(ChangeColorOverTime(2f, Color.yellow));

                    //Boss血量*1/2
                }
                else
                {
                    showHint("這一關有「冷戰」負面效果", 2f);
                    //移動速度下降
                    // 開始協程
                    StartCoroutine(ChangeColorOverTime(2f, Color.blue));
                }
                break;
            case 3:
                if (levelType < LevelDynamicAttribute.LevelType.Normal)
                {
                    showHint("這一關有「撒嬌」正面效果", 2f);
                    StartCoroutine(ChangeColorOverTime(2f, Color.magenta));
                    playerController.ShowGF();
                    //跳躍高度上升
                }
                else
                {
                    showHint("這一關有「抱怨靈」負面效果", 2f);
                    playerController.ShowComplain();
                    StartCoroutine(ChangeColorOverTime(2f, Color.gray));
                    //跳躍高度下降
                }
                break;
                
        }
    }
    void updateUI()
    {
        comboText.text = combo.ToString();

        BossSlider.value = monsterController.currentHP;
        BossSlider.maxValue = monsterController.maxBossHP;
    }

    //初始化關卡資料
    void initialLevel()
    {
        currentLevel++;
        showHint("歡迎來到這一關", 2f);
        showHint("這一關非常難", 2f);
        CurrentGameState = GameState.Fight;
    }

    public LevelDynamicAttribute.LevelType levelType;
    //根據資料決定關卡難易度
    void compundLevel()
    {
        levelDynamicAttribute = new();
        levelDynamicAttribute.SetDataByLevelType();
        
    }
    IEnumerator ChangeColorOverTime(float duration, Color color)
    {
        float halfDuration = duration / 2f;
        float timer = 0f;

        // 從白色到藍色
        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            float t = timer / halfDuration;
            spriteRenderer.color = Color.Lerp(Color.white, color, t);
            yield return null;
        }

        timer = 0f;

        // 從藍色到白色
        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            float t = timer / halfDuration;
            spriteRenderer.color = Color.Lerp(color, Color.white, t);
            yield return null;
        }

        // 確保最終顏色是白色
        spriteRenderer.color = Color.white;
    }
    void createConversation()
    {
        if (!dialogBoxInstance)
        {
            //currentLevel++; 
            dialogBoxInstance = Instantiate(PrefabDialogBox, new Vector3(0, 0, 0), Quaternion.identity);

            //隨機產生問題
            var id = UnityEngine.Random.Range(1, CsvReader.ConversationDictionary.Count + 1);
            dialogController.conversationID = id;

        }

        if (selectionTag.Count == currentLevel)
        {
            if (selectionTag[selectionTag.Count-1] == false)
            {
                levelType++;
                if(levelType == LevelDynamicAttribute.LevelType.Normal)
                {
                    levelType++;
                }
            }
            else
            {
                levelType--;
                if (levelType == LevelDynamicAttribute.LevelType.Normal)
                {
                    levelType--;
                }
            }
            Destroy(dialogBoxInstance);

            CurrentGameState = GameState.End;
        }
    }

    //創建怪物
    void createMonster()
    {
        if (!monsterInstance)
        {
            switch(currentLevel)
            {
                case 1:
                    monsterInstance = Instantiate(PrefabBoss, new Vector3(0, 0, 0), Quaternion.identity);

                    break;
                case 2:
                    monsterInstance = Instantiate(PrefabBoss1, new Vector3(0, 0, 0), Quaternion.identity);

                    break;
                case 3:
                    monsterInstance = Instantiate(PrefabBoss2, new Vector3(0, 0, 0), Quaternion.identity);

                    break;
            }
            
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
        HintText.gameObject.SetActive(true);
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
        HintText.gameObject.SetActive(false);

    }



}
