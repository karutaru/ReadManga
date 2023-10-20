using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System;

public class MangaBarsDisplay : MonoBehaviour
{
    public MangaDataSO mangaDataSO;
    public MangaListManager mangaListManager;  // MangaListManagerへの参照を追加


    public List<Image> barImages;
    public List<Image> adjacentImageDisplays;  // 最大3つのBarの隣接画像のリスト
    public List<Text> genreTextObjects;  // ジャンルの日本語名を表示するテキストオブジェクトのリスト
    //public List<Color> genreColors;  // ジャンルタグに対応する色のリスト

    [System.Serializable]
    public struct MangaTagImageMappingList
    {
        public List<MangaTag> mangaTags;
        public List<Sprite> individualBarImages;  // 各タグに対応するバーの画像のリスト
        public List<Sprite> individualAdjacentImages;  // 各タグに対応する隣接画像のリスト
        public List<Color> individualTagColors;  // 各タグに対応する色のリスト
    }
    public MangaTagImageMappingList tagImageMappingList;

    private void Start()
    {
        DisplayMangaBars(1);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            DisplayMangaBars(1);
            mangaListManager.DisplayMangaInfoByID(1);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            DisplayMangaBars(2);
            mangaListManager.DisplayMangaInfoByID(2);
        }
    }


    public void DisplayMangaBars(int? manga_ID = null)
    {
        MangaData targetMangaData;

        // manga_IDが指定されていれば、それに基づいてMangaDataエントリを検索
        if (manga_ID.HasValue)
        {
            targetMangaData = mangaDataSO.manga_DataList.FirstOrDefault(manga => manga.manga_ID == manga_ID.Value);

            if (targetMangaData == null)
            {
                Debug.LogWarning($"No MangaData entry found for manga_ID: {manga_ID.Value}");
                return;
            }
        }
        else
        {
            // manga_IDが指定されていない場合、デフォルトの動作を維持
            targetMangaData = mangaDataSO.manga_DataList[0];
        }

        for (int i = 0; i < targetMangaData.manga_Tags.Count; i++)
        {
            float value = targetMangaData.manga_Tags[i].Value / 10f;

            int index = tagImageMappingList.mangaTags.IndexOf(targetMangaData.manga_Tags[i].Tag);
            if (index != -1)
            {
                barImages[i].sprite = tagImageMappingList.individualBarImages[index];
                adjacentImageDisplays[i].sprite = tagImageMappingList.individualAdjacentImages[index];
            }

            // ジャンルの日本語の名前をテキストオブジェクトに設定
            if (i < genreTextObjects.Count)
            {
                genreTextObjects[i].text = targetMangaData.manga_Tags[i].Tag.ToString();

                // ジャンルタグに対応する色をテキストオブジェクトに設定
                int colorIndex = tagImageMappingList.mangaTags.IndexOf(targetMangaData.manga_Tags[i].Tag);
                if (colorIndex != -1 && colorIndex < tagImageMappingList.individualTagColors.Count)
                {
                    genreTextObjects[i].color = tagImageMappingList.individualTagColors[colorIndex];
                }
            }
            barImages[i].DOFillAmount(value, 0.5f);
        }
    }
}