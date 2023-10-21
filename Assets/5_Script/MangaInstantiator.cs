using System;
using UnityEngine;
using UnityEngine.UI;

public class MangaInstantiator : MonoBehaviour
{
    public MangaDataSO mangaDataSO;
    public GameObject mangaPrefab;
    public Transform content;
    public MangaBarsDisplay mangaBarsDisplay; // MangaBarsDisplay�ւ̎Q�Ƃ�ǉ�

    private void Start()
    {
        InstantiateMangaItems();
    }

    private void InstantiateMangaItems()
    {
        foreach (var manga in mangaDataSO.manga_DataList)
        {
            GameObject mangaItem = Instantiate(mangaPrefab, content);

            // MangaButton�X�N���v�g��manga_ID��MangaBarsDisplay�̎Q�Ƃ�ݒ�
            MangaButton mangaButton = mangaItem.GetComponent<MangaButton>();
            if (mangaButton != null)
            {
                mangaButton.Setup(manga.manga_ID, mangaBarsDisplay);
            }

            // ������Image�R���|�[�l���g�̃X�v���C�g��ݒ�
            Image mangaImage = mangaItem.GetComponent<Image>();
            if (mangaImage != null && manga.manga_ID == mangaDataSO.manga_DataList.IndexOf(manga) + 1)
            {
                mangaImage.sprite = manga.manga_Title;
            }

            // �q�v�f��Image�R���|�[�l���g���擾���āAMangaBarsDisplay�̃A�C�R���摜��ݒ�
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