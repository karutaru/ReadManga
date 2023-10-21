using System;
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

            // 子要素のImageコンポーネントを取得して、MangaBarsDisplayのアイコン画像を設定
            Image[] childImages = mangaItem.GetComponentsInChildren<Image>(true);

            // Skip the parent's image component by starting from the first child
            for (int i = 0; i < manga.manga_Tags.Count && i + 1 < childImages.Length; i++)
            {
                MangaTag tag = manga.manga_Tags[i].Tag;

                // Check if the tag's value is within the bounds of the individualAdjacentImages list
                if ((int)tag < mangaBarsDisplay.tagImageMappingList.individualAdjacentImages.Count)
                {
                    childImages[i + 1].sprite = mangaBarsDisplay.tagImageMappingList.individualAdjacentImages[(int)tag];
                }
            }
        }
    }
}