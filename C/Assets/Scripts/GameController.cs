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

    //��l�����d���
    void initialLevel()
    {
        currentLevel++;
        showHint("�w��Ө�o�@��", 2f);
        showHint("�o�@���D�`��", 2f);
        CurrentGameState = GameState.Fight;
    }

    //�ھڸ�ƨM�w���d������
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

            //�H�����Ͱ��D
            var id = UnityEngine.Random.Range(1, CsvReader.ConversationDictionary.Count + 1);
            dialogController.conversationID = id;

        }

        if (selectionTag.Count == currentLevel)
        {
            CurrentGameState = GameState.End;
        }
    }

    //�ЫةǪ�
    void createMonster()
    {
        if (!monsterInstance)
        {
            monsterInstance = Instantiate(PrefabBoss, new Vector3(0, 0, 50), Quaternion.identity);
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
