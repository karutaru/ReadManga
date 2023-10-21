using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InfiniteScroll : MonoBehaviour
{
    public GameObject itemPrefab;
    public int totalItemCount;
    public int maxVisibleItems;

    private ScrollRect scrollRect;
    private List<GameObject> itemPool = new List<GameObject>();
    private int firstItemIndex = 0;

    private float itemHeight;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        itemHeight = itemPrefab.GetComponent<RectTransform>().sizeDelta.y;

        InitializeItemPool();
        UpdateItems();
    }

    void InitializeItemPool()
    {
        for (int i = 0; i < maxVisibleItems + 2; i++)
        {
            GameObject item = Instantiate(itemPrefab, scrollRect.content);
            itemPool.Add(item);
        }
    }

    void Update()
    {
        CheckScrollPosition();
    }

    void CheckScrollPosition()
    {
        if (scrollRect.content.anchoredPosition.y > itemHeight * 2)
        {
            firstItemIndex = (firstItemIndex + 1) % totalItemCount;
            MoveItemToLast(itemPool[0]);
            UpdateItems();
        }
        else if (scrollRect.content.anchoredPosition.y < 0)
        {
            firstItemIndex--;
            if (firstItemIndex < 0) firstItemIndex = totalItemCount - 1;
            MoveItemToFirst(itemPool[itemPool.Count - 1]);
            UpdateItems();
        }
    }

    void MoveItemToLast(GameObject item)
    {
        item.transform.SetAsLastSibling();
        itemPool.RemoveAt(0);
        itemPool.Add(item);
    }

    void MoveItemToFirst(GameObject item)
    {
        item.transform.SetAsFirstSibling();
        itemPool.RemoveAt(itemPool.Count - 1);
        itemPool.Insert(0, item);
    }

    void UpdateItems()
    {
        for (int i = 0; i < itemPool.Count; i++)
        {
            int dataIndex = (firstItemIndex + i) % totalItemCount;
            itemPool[i].SetActive(true);
            // Update the item data here, e.g.:
            // itemPool[i].GetComponent<ItemController>().SetData(dataList[dataIndex]);
        }
    }
}