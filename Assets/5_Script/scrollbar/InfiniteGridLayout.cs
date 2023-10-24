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
    public int totalCount = -1; // �\�����鍀�ڂ̑����B
    private ObjectPool<GameObject> _pool;

    private void Start()
    {
        var scrollRect = GetComponent<LoopScrollRect>();
        scrollRect.prefabSource = this;
        scrollRect.dataSource = this;
        scrollRect.totalCount = totalCount;
        scrollRect.RefillCells();

        // �I�u�W�F�N�g�v�[����������
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
        // �I�u�W�F�N�g�̃f�[�^���X�V
        trans.GetChild(0).GetComponent<TextMeshProUGUI>().text = index.ToString();
    }
}