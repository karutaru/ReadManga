using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;  

public class MangaListManager : MonoBehaviour
{
    public MangaBarsDisplay mangaBarsDisplay;
    public MangaDataSO mangaDataSO;  // MangaDataSO�ւ̎Q��
    public TypingText nameTypingTextComponent;  // manga_Name��\�����邽�߂�TypingText�R���|�[�l���g
    public TypingText explanationTypingTextComponent;  // explanation��\�����邽�߂�TypingText�R���|�[�l���g

    // manga_ID�Ɋ�Â���MangaDataSO����manga_Name��explanation���擾���A���ꂼ���TypingText�ɑ��郁�\�b�h
    public void DisplayMangaInfoByID(int manga_ID)
    {
        // �w�肳�ꂽ manga_ID �Ɋ�Â��� MangaData �G���g��������
        MangaData targetMangaData = mangaDataSO.manga_DataList.FirstOrDefault(manga => manga.manga_ID == manga_ID);

        if (targetMangaData != null)
        {
            // �eTypingText�ɏ��𑗂�
            nameTypingTextComponent.TypeText(targetMangaData.manga_Name);
            explanationTypingTextComponent.TypeText(targetMangaData.explanation);
        }
        else
        {
            Debug.LogWarning($"No MangaData entry found for manga_ID: {manga_ID}");
        }
    }
}
