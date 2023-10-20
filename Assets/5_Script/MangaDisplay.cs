using UnityEngine;
using UnityEngine.UI;

public class MangaDisplay : MonoBehaviour
{
    public MangaDataSO mangaDataSO;  // ScriptableObject���Q�Ƃ��邽�߂̕ϐ�
    public GameObject mangaPanelPrefab;  // �������\�����邽�߂�UI�p�l���̃v���n�u

    private void Start()
    {
        DisplayMangaData();
    }

    private void DisplayMangaData()
    {
        foreach (var manga in mangaDataSO.manga_DataList)
        {
            // �p�l���̃C���X�^���X���쐬
            GameObject panel = Instantiate(mangaPanelPrefab, transform);

            // �p�l���̃e�L�X�g��摜���X�V
            panel.transform.Find("MangaName").GetComponent<Text>().text = manga.manga_Name;
            panel.transform.Find("MangaRuby").GetComponent<Text>().text = manga.manga_ruby;
            panel.transform.Find("MangaFav").GetComponent<Text>().text = "���C�ɓ���x: " + manga.manga_Fav.ToString();

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
