using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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



    public CSVReader CsvReader;

    public int currentLevel = 0;
    void Awake()
    {
        // �T�O�u���@�ӹ�Ҧs�b
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �O������b���������ɤ��Q�P��
        }
        else
        {
            Destroy(gameObject); // �p�G�w�g���@�ӹ�Ҧs�b�A�P���s������
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
            case GameState.GameEnd:
                SceneManager.LoadScene("MainMenu");
                Application.Quit();


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


            // ����key�H����T��key����D
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

            // �o�T�Ӱ��D�H����T��value�ﶵ
            questionsAndOptions = new Dictionary<string, List<string>>();

            foreach (string key in selectedKeys)
            {
                List<string> options = new List<string>();
                string correctAnswer = girlInfo[key];
                options.Add(correctAnswer);

                // �K�[��L�H���ﶵ���즳�T�ӿﶵ
                while (options.Count < 3)
                {
                    string option = CsvReader.GirlInfoDictionary[key][UnityEngine.Random.Range(1, 6)].ToString();
                    if (!options.Contains(option))
                    {
                        options.Add(option);
                    }
                }

                // ���ÿﶵ����
                for (int i = options.Count - 1; i > 0; i--)
                {
                    int j = UnityEngine.Random.Range(0, i + 1);
                    string temp = options[i];
                    options[i] = options[j];
                    options[j] = temp;
                }

                questionsAndOptions[key] = options;
            }


            girlFriendInfoInstance.transform.GetComponent<Text>().text = $"�k�ͪ���T�p�k�A�аȥ��O�n.....................................................�G�k�ͳ��w�Y{girlInfo["likeFood"]}�M��{girlInfo["likeMovie"]}�q�v�F�o���ѥh���F{girlInfo["todayDo"]}�M�ʪ��A�ר���w{girlInfo["likeAnimal"]}�C�o�Q��{girlInfo["hateFood"]}�M���������ҡA�����w���_�C���ѬO{girlInfo["today"]}�o��@�عL�ӡA�����w�ܪG�ġC�o���w���b��䪺�Ŷ��A�]�ܪ`���ͬ��~��A�ɨ�²��o���~�����ͬ��C\r\n";
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
            showHint("���D��Ǫ��Y���y���ˮ`", 2f);
            return;
        }
        switch(UnityEngine.Random.Range(1, 4))
        {
            case 1:
                if(levelType < LevelDynamicAttribute.LevelType.Normal)
                {
                    showHint("�o�@�����u���ʡv�����ĪG", 2f);
                    //Player�i�H����y
                    StartCoroutine(ChangeColorOverTime(2f, Color.red));

                }
                else
                {
                    showHint("�o�@�����u�r�����v�t���ĪG", 2f);
                    //Boss�i�H�񳴨�
                    monsterController.CanPoison = true;
                    StartCoroutine(ChangeColorOverTime(2f, new Color(0.5f, 0f, 0.5f)));

                }
                break;
            case 2:
                if (levelType < LevelDynamicAttribute.LevelType.Normal)
                {
                    showHint("�o�@�����u�ʷRPunch�v�����ĪG", 2f);
                    StartCoroutine(ChangeColorOverTime(2f, Color.yellow));

                    //Boss��q*1/2
                }
                else
                {
                    showHint("�o�@�����u�N�ԡv�t���ĪG", 2f);
                    //���ʳt�פU��
                    // �}�l��{
                    StartCoroutine(ChangeColorOverTime(2f, Color.blue));
                }
                break;
            case 3:
                if (levelType < LevelDynamicAttribute.LevelType.Normal)
                {
                    showHint("�o�@�����u���b�v�����ĪG", 2f);
                    StartCoroutine(ChangeColorOverTime(2f, Color.magenta));
                    playerController.ShowGF();
                    //���D���פW��
                }
                else
                {
                    showHint("�o�@�����u����F�v�t���ĪG", 2f);
                    playerController.ShowComplain();
                    StartCoroutine(ChangeColorOverTime(2f, Color.gray));
                    //���D���פU��
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

    //��l�����d���
    void initialLevel()
    {
        currentLevel++;
        showHint("�w��Ө�o�@��", 2f);
        showHint("�o�@���D�`��", 2f);
        CurrentGameState = GameState.Fight;
    }

    public LevelDynamicAttribute.LevelType levelType;
    //�ھڸ�ƨM�w���d������
    void compundLevel()
    {
        levelDynamicAttribute = new();
        levelDynamicAttribute.SetDataByLevelType();
        
    }
    IEnumerator ChangeColorOverTime(float duration, Color color)
    {
        float halfDuration = duration / 2f;
        float timer = 0f;

        // �q�զ���Ŧ�
        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            float t = timer / halfDuration;
            spriteRenderer.color = Color.Lerp(Color.white, color, t);
            yield return null;
        }

        timer = 0f;

        // �q�Ŧ��զ�
        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            float t = timer / halfDuration;
            spriteRenderer.color = Color.Lerp(color, Color.white, t);
            yield return null;
        }

        // �T�O�̲��C��O�զ�
        spriteRenderer.color = Color.white;
    }
    void createConversation()
    {
        if (!dialogBoxInstance)
        {
            //currentLevel++; 
            dialogBoxInstance = Instantiate(PrefabDialogBox, new Vector3(0, 0, 0), Quaternion.identity);

            //�H�����Ͱ��D
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

    //�ЫةǪ�
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

    //�Ыت��a
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
