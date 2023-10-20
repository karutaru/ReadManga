using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "MangaDataSO", menuName = "Create ScriptableObjects/MangaDataSO")]
public class MangaDataSO : ScriptableObject
{
    [OnValueChanged("OnMangaListChanged")]
    public List<MangaData> manga_DataList = new List<MangaData>();

    private int lastMangaID = 0;  // 初期値を0に変更

    // 新しいMangaDataをリストに追加するメソッド
    public void AddManga(MangaData newManga)
    {
        lastMangaID++;  // IDを増やす
        newManga.manga_ID = lastMangaID;  // 新しいMangaDataのIDを設定
        manga_DataList.Add(newManga);  // リストに追加
    }

    // manga_DataListが変更されたときに呼び出されるメソッド
    private void OnMangaListChanged()
    {
        for (int i = 0; i < manga_DataList.Count; i++)
        {
            manga_DataList[i].manga_ID = i + 1;  // インデックスに1を足してIDとして使用
            lastMangaID = i + 1;
        }
    }
}
