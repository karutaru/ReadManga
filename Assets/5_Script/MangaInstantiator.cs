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
        }
    }
}