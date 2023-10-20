using UnityEngine;
using UnityEngine.UI;

public class MangaDisplay : MonoBehaviour
{
    public MangaDataSO mangaDataSO;  // ScriptableObjectを参照するための変数
    public GameObject mangaPanelPrefab;  // 漫画情報を表示するためのUIパネルのプレハブ

    private void Start()
    {
        DisplayMangaData();
    }

    private void DisplayMangaData()
    {
        foreach (var manga in mangaDataSO.manga_DataList)
        {
            // パネルのインスタンスを作成
            GameObject panel = Instantiate(mangaPanelPrefab, transform);

            // パネルのテキストや画像を更新
            panel.transform.Find("MangaName").GetComponent<Text>().text = manga.manga_Name;
            panel.transform.Find("MangaRuby").GetComponent<Text>().text = manga.manga_ruby;
            panel.transform.Find("MangaFav").GetComponent<Text>().text = "お気に入り度: " + manga.manga_Fav.ToString();

            Text tagText = panel.transform.Find("MangaTags").GetComponent<Text>();
            tagText.text = "";
            foreach (var tag in manga.manga_Tags)
            {
                tagText.text += tag.Tag.ToString() + ": " + tag.Value + "\n";
            }

            panel.transform.Find("MangaExplanation").GetComponent<Text>().text = manga.explanation;
            panel.transform.Find("MangaCover").GetComponent<Image>().sprite = manga.manga_Sprite;
            panel.transform.Find("MangaRecommendPage").GetComponent<Image>().sprite = manga.manga_Page;
        }
    }
}
