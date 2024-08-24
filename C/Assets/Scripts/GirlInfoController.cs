
using UnityEngine;

public class GirlInfoController : MonoBehaviour
{
    public float scrollSpeed = 50f; // �u�ʳt��
    private RectTransform rectTransform;
    private float textWidth;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textWidth = rectTransform.rect.width;
    }

    void Update()
    {
        // �V�����ʤ�r
        rectTransform.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

        // �ˬd��r�O�_�����]�X�ù��~
        if (rectTransform.anchoredPosition.x + textWidth < 0)
        {
            // ����ʩΰ����L�޿�
            enabled = false;
        }
    }
}