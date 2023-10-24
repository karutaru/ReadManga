using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum MangaTag
{
    なし,
    ほのぼの,
    コメディー,
    バトル,
    恋愛,
    マニアック,
    推理,
    鬱,
    グロ,
    エロ,
    自作タグ
}

[System.Serializable]
public class MangaTagWithValue
{
    [ValueDropdown("GetAllMangaTags")]
    public MangaTag Tag;

    [ShowIf("IsCustomTagSelected")]
    public string customTagText;  // カスタムタグの名前

    [Range(0, 10)]
    public float Value;

    private IEnumerable<MangaTag> GetAllMangaTags()
    {
        return System.Enum.GetValues(typeof(MangaTag)) as IEnumerable<MangaTag>;
    }

    // "自作タグ"が選択されるか
    private bool IsCustomTagSelected()
    {
        return Tag == MangaTag.自作タグ;
    }
}




[System.Serializable]
public class MangaData
{
    [LabelText("漫画のID"), HideLabel]
    public int manga_ID;            // 漫画のID
    [LabelText("漫画の名前"), HideLabel]
    public string manga_Name;       // 漫画の名前
    [LabelText("漫画のルビ"), HideLabel]
    public string manga_ruby;       // 漫画のルビ

    [Range(0, 10)]
    [LabelText("漫画のお気に入り度"), HideLabel]
    public float manga_Fav;          // 漫画のお気に入り度

    [LabelText("ジャンルとその数値"), HideLabel]
    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<MangaTagWithValue> manga_Tags;  // ジャンルとその数値

    [LabelText("漫画の説明"), HideLabel]
    [SerializeField, Multiline(4)]  // 漫画の説明
    public string explanation;

    [LabelText("漫画のアイコン用タイトル画像"), HideLabel]
    public Sprite manga_Title;     // 漫画のアイコン用タイトル画像
    [LabelText("漫画の表紙"), HideLabel]
    public Sprite manga_Sprite;     // 漫画の表紙
    [LabelText("漫画のオススメのコマ"), HideLabel]
    public Sprite manga_Page;       // 漫画のオススメのコマ


    private void OnValidate()
    {
        // manga_Tagsリストが3つ以上の要素を持っている場合、それを制限する
        if (manga_Tags.Count > 3)
        {
            manga_Tags.RemoveRange(3, manga_Tags.Count - 3);
            Debug.LogWarning("タグは3つ以下に設定してください。");
        }
    }
}
