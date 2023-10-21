using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System;

public class MangaBarsDisplay : MonoBehaviour
{
    public SpriteRenderer manga_hyoshi; // 漫画の表紙

    public MangaDataSO mangaDataSO;
    public MangaListManager mangaListManager;   // MangaListManagerへの参照を追加
    public ImageResizer imageResizer;

    public List<TypingText> typingTexts = new List<TypingText>();
    public List<Image> barImages;
    public List<Image> adjacentImageDisplays;   // 最大3つのBarの隣接画像のリスト
    public List<Text> genreTextObjects;         // ジャンルの日本語名を表示するテキストオブジェクトのリスト
    public List<Text> valueTextObjects = new List<Text>();  // テキストオブジェクトのリスト


    [System.Serializable]
    public struct MangaTagImageMappingList
    {
        public List<MangaTag> mangaTags;
        public List<Sprite> individualBarImages;        // 各タグに対応するバーの画像のリスト
        public List<Sprite> individualAdjacentImages;   // 各タグに対応する隣接画像のリスト
        public List<Color> individualTagColors;         // 各タグに対応する色のリスト
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
            mangaListManager.DisplayMangaInfoByID(1); // 漫画の名前と説明をアニメーション
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            DisplayMangaBars(2);
            mangaListManager.DisplayMangaInfoByID(2); // 漫画の名前と説明をアニメーション
        }
    }


    public void DisplayMangaBars(int? manga_ID = null)
    {
        MangaData targetMangaData;

        // manga_IDが指定されていれば、それに基づいてMangaDataエントリを検索
        if (manga_ID.HasValue)
        {
            targetMangaData = mangaDataSO.manga_DataList.FirstOrDefault(manga => manga.manga_ID == manga_ID.Value);

            mangaListManager.DisplayMangaInfoByID(targetMangaData.manga_ID);

            manga_hyoshi.sprite = targetMangaData.manga_Sprite;
            imageResizer.ResizeImage();

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

        for (int i = 0; i < targetMangaData.manga_Tags.Count; i++) // 漫画のジャンルをテキストに設定
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

                MangaTagWithValue tagWithValue = targetMangaData.manga_Tags[i];
                if (tagWithValue.Tag == MangaTag.自作タグ && !string.IsNullOrEmpty(tagWithValue.customTagText))
                {
                    genreTextObjects[i].text = tagWithValue.customTagText;  // Set the text to the content of customTagText
                }

                // ジャンルタグに対応する色をテキストオブジェクトに設定
                int colorIndex = tagImageMappingList.mangaTags.IndexOf(targetMangaData.manga_Tags[i].Tag);
                if (colorIndex != -1 && colorIndex < tagImageMappingList.individualTagColors.Count)
                {
                    genreTextObjects[i].color = tagImageMappingList.individualTagColors[colorIndex];
                }
            }
            barImages[i].DOFillAmount(value, 1f).SetEase(Ease.OutExpo); // 漫画のジャンルバーをアニメーション
        }

        if (manga_ID.HasValue)
        {
            var manga = mangaDataSO.manga_DataList.Find(m => m.manga_ID == manga_ID.Value);
            if (manga != null)
            {
                List<int?> splitValues = SplitFloatValue(manga.manga_Fav);
                DisplayValuesInTextObjects(splitValues);
            }
        }
    }

    // splitValuesを使用して、テキストオブジェクトリストに各数値を表示するメソッド
    private void DisplayValuesInTextObjects(List<int?> splitValues)
    {
        for (int i = 0; i < valueTextObjects.Count; i++)
        {
            if (i < splitValues.Count && splitValues[i].HasValue)
            {
                valueTextObjects[i].text = null;
                valueTextObjects[i].text = splitValues[i].Value.ToString();
            }
            else
            {
                valueTextObjects[i].text = "";
            }
        }
        AnimateCounterSequentially(0, splitValues);
    }

    private void AnimateCounterSequentially(int index, List<int?> splitValues)
    {
        if (index >= splitValues.Count)
        {
            return;
        }

        if (splitValues[index].HasValue)
        {
            // カウンターアニメーションを開始する
            AnimateCounterSequentially(index + 1, splitValues);

            typingTexts[index].AnimateCounter(splitValues[index].Value);
        }
        else
        {
            valueTextObjects[index].text = "";
            AnimateCounterSequentially(index + 1, splitValues);  // 次のテキストのカウンターアニメーションを開始
        }
    }


    public static List<int?> SplitFloatValue(float value)
    {
        // 整数部と小数部を取得
        int integerPart = (int)value;
        int decimalPart = Mathf.RoundToInt((value - integerPart) * 10);  // 小数第一位まで考慮

        // 整数部を文字列に変換して各桁をリストに分解
        List<int?> integerDigits = new List<int?>();
        string integerString = integerPart.ToString();

        // 1桁の整数部の場合
        if (integerString.Length == 1)
        {
            integerDigits.Add(null);
            integerDigits.Add(int.Parse(integerString[0].ToString()));
        }
        // 2桁以上の整数部の場合
        else
        {
            foreach (char digit in integerString)
            {
                integerDigits.Add(int.Parse(digit.ToString()));
            }
        }

        integerDigits.Add(decimalPart);  // 最後の要素を小数部で置き換える

        return integerDigits;
    }
}