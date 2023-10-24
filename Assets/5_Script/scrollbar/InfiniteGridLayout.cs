using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

[RequireComponent(typeof(LoopScrollRect))]
[DisallowMultipleComponent]
public sealed class InfiniteGridLayout : MonoBehaviour, LoopScrollPrefabSource, LoopScrollDataSource
{
    [SerializeField] private GameObject _prefab;
    public int totalCount = -1; // 表示する項目の総数。
    private ObjectPool<GameObject> _pool;

    private void Start()
    {
        var scrollRect = GetComponent<LoopScrollRect>();
        scrollRect.prefabSource = this;
        scrollRect.dataSource = this;
        scrollRect.totalCount = totalCount;
        scrollRect.RefillCells();

        // オブジェクトプールを初期化
        _pool = new ObjectPool<GameObject>(
            () => Instantiate(_prefab),
            o => o.SetActive(true),
            o =>
            {
                o.transform.SetParent(transform);
                o.SetActive(false);
            }
        );
    }


    GameObject LoopScrollPrefabSource.GetObject(int index)
    {
        var obj = _pool.Get();
        obj.transform.SetParent(transform);
        return obj;
    }

    void LoopScrollPrefabSource.ReturnObject(Transform trans)
    {
        _pool.Release(trans.gameObject);
    }

    void LoopScrollDataSource.ProvideData(Transform trans, int index)
    {
        // オブジェクトのデータを更新
        trans.GetChild(0).GetComponent<TextMeshProUGUI>().text = index.ToString();
    }
}