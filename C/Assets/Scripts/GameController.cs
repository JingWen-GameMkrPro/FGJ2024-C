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

    //��l�����d���
    void InitialLevel()
    {
        showHint("Hello");
    }

    

    //�ھڸ�ƨM�w���d������
    void CompundLevel()
    {

    }

    //�ЫةǪ�
    void CreateBoss()
    {

    }

    //�Ыت��a
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
