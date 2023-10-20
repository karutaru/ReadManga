using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System;

public class MangaBarsDisplay : MonoBehaviour
{
    public MangaDataSO mangaDataSO;
    public MangaListManager mangaListManager;  // MangaListManager�ւ̎Q�Ƃ�ǉ�


    public List<Image> barImages;
    public List<Image> adjacentImageDisplays;  // �ő�3��Bar�̗אډ摜�̃��X�g
    public List<Text> genreTextObjects;  // �W�������̓��{�ꖼ��\������e�L�X�g�I�u�W�F�N�g�̃��X�g
    //public List<Color> genreColors;  // �W�������^�O�ɑΉ�����F�̃��X�g

    [System.Serializable]
    public struct MangaTagImageMappingList
    {
        public List<MangaTag> mangaTags;
        public List<Sprite> individualBarImages;  // �e�^�O�ɑΉ�����o�[�̉摜�̃��X�g
        public List<Sprite> individualAdjacentImages;  // �e�^�O�ɑΉ�����אډ摜�̃��X�g
        public List<Color> individualTagColors;  // �e�^�O�ɑΉ�����F�̃��X�g
    }
    public MangaTagImageMappingList tagImageMappingList;

    private void Start()
    {
        DisplayMangaBars(1);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            DisplayMangaBars(1);
            mangaListManager.DisplayMangaInfoByID(1);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            DisplayMangaBars(2);
            mangaListManager.DisplayMangaInfoByID(2);
        }
    }


    public void DisplayMangaBars(int? manga_ID = null)
    {
        MangaData targetMangaData;

        // manga_ID���w�肳��Ă���΁A����Ɋ�Â���MangaData�G���g��������
        if (manga_ID.HasValue)
        {
            targetMangaData = mangaDataSO.manga_DataList.FirstOrDefault(manga => manga.manga_ID == manga_ID.Value);

            if (targetMangaData == null)
            {
                Debug.LogWarning($"No MangaData entry found for manga_ID: {manga_ID.Value}");
                return;
            }
        }
        else
        {
            // manga_ID���w�肳��Ă��Ȃ��ꍇ�A�f�t�H���g�̓�����ێ�
            targetMangaData = mangaDataSO.manga_DataList[0];
        }

        for (int i = 0; i < targetMangaData.manga_Tags.Count; i++)
        {
            float value = targetMangaData.manga_Tags[i].Value / 10f;

            int index = tagImageMappingList.mangaTags.IndexOf(targetMangaData.manga_Tags[i].Tag);
            if (index != -1)
            {
                barImages[i].sprite = tagImageMappingList.individualBarImages[index];
                adjacentImageDisplays[i].sprite = tagImageMappingList.individualAdjacentImages[index];
            }

            // �W�������̓��{��̖��O���e�L�X�g�I�u�W�F�N�g�ɐݒ�
            if (i < genreTextObjects.Count)
            {
                genreTextObjects[i].text = targetMangaData.manga_Tags[i].Tag.ToString();

                // �W�������^�O�ɑΉ�����F���e�L�X�g�I�u�W�F�N�g�ɐݒ�
                int colorIndex = tagImageMappingList.mangaTags.IndexOf(targetMangaData.manga_Tags[i].Tag);
                if (colorIndex != -1 && colorIndex < tagImageMappingList.individualTagColors.Count)
                {
                    genreTextObjects[i].color = tagImageMappingList.individualTagColors[colorIndex];
                }
            }
            barImages[i].DOFillAmount(value, 0.5f);
        }
    }
}