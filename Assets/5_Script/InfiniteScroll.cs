using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


[RequireComponent(typeof(ScrollRect))]
public class InfiniteScroll : MonoBehaviour
{
    public RectTransform content;
    public int itemCount; // アイテムの総数
    public float itemHeight; // 1つのアイテムの高さ
    private ScrollRect scrollRect;
    private int visibleItemCount; // 表示中のアイテム数

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(OnScroll);
        visibleItemCount = Mathf.CeilToInt(scrollRect.viewport.rect.height / itemHeight) + 2;
    }

    private void OnScroll(Vector2 position)
    {
        float contentPosition = content.anchoredPosition.y;

        // 上方向へのスクロール
        if (contentPosition >= itemHeight)
        {
            int itemsToMove = Mathf.FloorToInt(contentPosition / itemHeight);
            for (int i = 0; i < itemsToMove; i++)
            {
                MoveItemToBottom();
            }
            content.anchoredPosition -= new Vector2(0, itemsToMove * itemHeight);
        }

        // 下方向へのスクロール
        if (contentPosition < 0)
        {
            int itemsToMove = Mathf.FloorToInt(-contentPosition / itemHeight) + 1;
            for (int i = 0; i < itemsToMove; i++)
            {
                MoveItemToTop();
            }
            content.anchoredPosition += new Vector2(0, itemsToMove * itemHeight);
        }
    }

    private void MoveItemToBottom()
    {
        // 最初のアイテムを最後の位置に移動
        RectTransform firstItem = content.GetChild(0) as RectTransform;
        firstItem.SetAsLastSibling();
        firstItem.anchoredPosition -= new Vector2(0, itemCount * itemHeight);
    }

    private void MoveItemToTop()
    {
        // 最後のアイテムを最初の位置に移動
        RectTransform lastItem = content.GetChild(content.childCount - 1) as RectTransform;
        lastItem.SetAsFirstSibling();
        lastItem.anchoredPosition += new Vector2(0, itemCount * itemHeight);
    }
}