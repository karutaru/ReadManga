using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MangaButton : MonoBehaviour
{
    public int mangaID;
    public MangaBarsDisplay mangaBarsDisplay; // MangaBarsDisplay�ւ̎Q�Ƃ�ǉ�

    private void Awake()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        // MangaBarsDisplay�̃��\�b�h���Ăяo����manga_ID��n��
        mangaBarsDisplay.DisplayMangaBars(mangaID);
    }

    public void Setup(int id, MangaBarsDisplay display)
    {
        mangaID = id;
        mangaBarsDisplay = display; // MangaBarsDisplay�̃C���X�^���X���Z�b�g�A�b�v
    }
}