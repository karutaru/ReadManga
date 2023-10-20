using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "MangaDataSO", menuName = "Create ScriptableObjects/MangaDataSO")]
public class MangaDataSO : ScriptableObject
{
    [OnValueChanged("OnMangaListChanged")]
    public List<MangaData> manga_DataList = new List<MangaData>();

    private int lastMangaID = 0;  // �����l��0�ɕύX

    // �V����MangaData�����X�g�ɒǉ����郁�\�b�h
    public void AddManga(MangaData newManga)
    {
        lastMangaID++;  // ID�𑝂₷
        newManga.manga_ID = lastMangaID;  // �V����MangaData��ID��ݒ�
        manga_DataList.Add(newManga);  // ���X�g�ɒǉ�
    }

    // manga_DataList���ύX���ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    private void OnMangaListChanged()
    {
        for (int i = 0; i < manga_DataList.Count; i++)
        {
            manga_DataList[i].manga_ID = i + 1;  // �C���f�b�N�X��1�𑫂���ID�Ƃ��Ďg�p
            lastMangaID = i + 1;
        }
    }
}
