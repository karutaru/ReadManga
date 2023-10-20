using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;  

public class MangaListManager : MonoBehaviour
{
    public MangaBarsDisplay mangaBarsDisplay;
    public MangaDataSO mangaDataSO;  // MangaDataSOへの参照
    public TypingText nameTypingTextComponent;  // manga_Nameを表示するためのTypingTextコンポーネント
    public TypingText explanationTypingTextComponent;  // explanationを表示するためのTypingTextコンポーネント

    // manga_IDに基づいてMangaDataSOからmanga_Nameとexplanationを取得し、それぞれのTypingTextに送るメソッド
    public void DisplayMangaInfoByID(int manga_ID)
    {
        // 指定された manga_ID に基づいて MangaData エントリを検索
        MangaData targetMangaData = mangaDataSO.manga_DataList.FirstOrDefault(manga => manga.manga_ID == manga_ID);

        if (targetMangaData != null)
        {
            // 各TypingTextに情報を送る
            nameTypingTextComponent.TypeText(targetMangaData.manga_Name);
            explanationTypingTextComponent.TypeText(targetMangaData.explanation);
        }
        else
        {
            Debug.LogWarning($"No MangaData entry found for manga_ID: {manga_ID}");
        }
    }
}
