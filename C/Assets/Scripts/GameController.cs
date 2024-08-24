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


            girlFriendInfoInstance.transform.GetComponent<Text>().text = $"�k�ͪ���T�p�k�A�аȥ��O�n.....................................................�G�k�ͳ��w�Y{girlInfo[0]}�M��{girlInfo[3]}�q�v�F�o���ѥh���F{girlInfo[4]}�M�ʪ��A�ר���w{girlInfo[2]}�C�o�Q��{girlInfo[1]}�M���������ҡA�����w���_�C���ѬO{girlInfo[5]}�o��@�عL�ӡA�����w�ܪG�ġC�o���w���b��䪺�Ŷ��A�]�ܪ`���ͬ��~��A�ɨ�²��o���~�����ͬ��C\r\n";
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

            var id = UnityEngine.Random.Range(1, CsvReader.ConversationDictionary.Count + 1);
            dialogController.conversationID = 2;

        }

        if (selectionID.Count == currentLevel)
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
