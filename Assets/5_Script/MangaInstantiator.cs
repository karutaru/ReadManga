using UnityEngine;
using UnityEngine.UI;

public class MangaInstantiator : MonoBehaviour
{
    public MangaDataSO mangaDataSO;
    public GameObject mangaPrefab;
    public Transform content;
    public MangaBarsDisplay mangaBarsDisplay; // MangaBarsDisplayへの参照を追加

    private void Start()
    {
        InstantiateMangaItems();
    }

    private void InstantiateMangaItems()
    {
        foreach (var manga in mangaDataSO.manga_DataList)
        {
            GameObject mangaItem = Instantiate(mangaPrefab, content);

            // MangaButtonスクリプトにmanga_IDとMangaBarsDisplayの参照を設定
            MangaButton mangaButton = mangaItem.GetComponent<MangaButton>();
            if (mangaButton != null)
            {
                mangaButton.Setup(manga.manga_ID, mangaBarsDisplay);
            }

            // ここでImageコンポーネントのスプライトを設定
            Image mangaImage = mangaItem.GetComponent<Image>();
            if (mangaImage != null && manga.manga_ID == mangaDataSO.manga_DataList.IndexOf(manga) + 1)
            {
                mangaImage.sprite = manga.manga_Title;
            }
        }
    }
}