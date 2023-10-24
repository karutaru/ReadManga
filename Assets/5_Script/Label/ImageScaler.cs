using UnityEngine;
using UnityEngine.EventSystems;

public class ImageScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor = 1.5f;  // 拡大する倍率
    private Vector3 originalScale;    // 元のサイズを保存する変数

    void Start()
    {
        originalScale = transform.localScale;  // 元のサイズを保存
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * scaleFactor;  // マウスが乗ったときに拡大
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;  // マウスが離れたときに元のサイズに戻る
    }
}