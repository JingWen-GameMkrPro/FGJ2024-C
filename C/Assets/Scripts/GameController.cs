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
    [HideInInspector]
    public GameObject playerInstance;
    [HideInInspector]
    public PlayerController playerController;
    [HideInInspector]
    public GameObject monsterInstance;
    [HideInInspector]
    public MonsterController monsterController;

    public LevelDynamicAttribute levelDynamicAttribute;

    public static GameController Instance { get; private set; }

    public GameState CurrentGameState = GameState.Start;
    public enum GameState
    {
        Begin,
        Start,
        Fight,
        Conversation,
        End,
    }

    //UI
    public Text HintText;
    public Text comboText;
    public int combo = 0;

    [HideInInspector]
    public CSVReader csvReader = new();

    public int currentLevel = 111;

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
                break;
            case GameState.End:
                break;
        }
    }

    void begin()
    {
        print("begin");
        if (!girlFriendInfoInstance)
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
        showHint("�w��Ө�o�@��", 2f);
        showHint("�o�@���D�`��", 2f);
        CurrentGameState = GameState.Fight;

    }

    //�ھڸ�ƨM�w���d������
    void compundLevel()
    {
        levelDynamicAttribute = new();
    }

    //�ЫةǪ�
    void createMonster()
    {
        if(!monsterInstance)
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
