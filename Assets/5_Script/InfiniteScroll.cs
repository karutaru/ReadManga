using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


[RequireComponent(typeof(ScrollRect))]
public class InfiniteScroll : MonoBehaviour
{
    public RectTransform content;
    public int itemCount; // �A�C�e���̑���
    public float itemHeight; // 1�̃A�C�e���̍���
    private ScrollRect scrollRect;
    private int visibleItemCount; // �\�����̃A�C�e����

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(OnScroll);
        visibleItemCount = Mathf.CeilToInt(scrollRect.viewport.rect.height / itemHeight) + 2;
    }

    private void OnScroll(Vector2 position)
    {
        float contentPosition = content.anchoredPosition.y;

        // ������ւ̃X�N���[��
        if (contentPosition >= itemHeight)
        {
            int itemsToMove = Mathf.FloorToInt(contentPosition / itemHeight);
            for (int i = 0; i < itemsToMove; i++)
            {
                MoveItemToBottom();
            }
            content.anchoredPosition -= new Vector2(0, itemsToMove * itemHeight);
        }

        // �������ւ̃X�N���[��
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
        // �ŏ��̃A�C�e�����Ō�̈ʒu�Ɉړ�
        RectTransform firstItem = content.GetChild(0) as RectTransform;
        firstItem.SetAsLastSibling();
        firstItem.anchoredPosition -= new Vector2(0, itemCount * itemHeight);
    }

    private void MoveItemToTop()
    {
        // �Ō�̃A�C�e�����ŏ��̈ʒu�Ɉړ�
        RectTransform lastItem = content.GetChild(content.childCount - 1) as RectTransform;
        lastItem.SetAsFirstSibling();
        lastItem.anchoredPosition += new Vector2(0, itemCount * itemHeight);
    }
}