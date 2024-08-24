
using UnityEngine;

public class GirlInfoController : MonoBehaviour
{
    public float scrollSpeed = 50f; // 滾動速度
    private RectTransform rectTransform;
    private float textWidth;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textWidth = rectTransform.rect.width;
    }

    void Update()
    {
        // 向左移動文字
        rectTransform.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

        // 檢查文字是否完全跑出螢幕外
        if (rectTransform.anchoredPosition.x + textWidth < 0)
        {
            // 停止移動或執行其他邏輯
            enabled = false;
        }
    }
}