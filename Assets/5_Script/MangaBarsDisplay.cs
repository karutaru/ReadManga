using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System;

public class MangaBarsDisplay : MonoBehaviour
{
    public SpriteRenderer manga_hyoshi; // ����̕\��

    public MangaDataSO mangaDataSO;
    public MangaListManager mangaListManager;   // MangaListManager�ւ̎Q�Ƃ�ǉ�
    public ImageResizer imageResizer;

    public List<TypingText> typingTexts = new List<TypingText>();
    public List<Image> barImages;
    public List<Image> adjacentImageDisplays;   // �ő�3��Bar�̗אډ摜�̃��X�g
    public List<Text> genreTextObjects;         // �W�������̓��{�ꖼ��\������e�L�X�g�I�u�W�F�N�g�̃��X�g
    public List<Text> valueTextObjects = new List<Text>();  // �e�L�X�g�I�u�W�F�N�g�̃��X�g


    [System.Serializable]
    public struct MangaTagImageMappingList
    {
        public List<MangaTag> mangaTags;
        public List<Sprite> individualBarImages;        // �e�^�O�ɑΉ�����o�[�̉摜�̃��X�g
        public List<Sprite> individualAdjacentImages;   // �e�^�O�ɑΉ�����אډ摜�̃��X�g
        public List<Color> individualTagColors;         // �e�^�O�ɑΉ�����F�̃��X�g
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
            mangaListManager.DisplayMangaInfoByID(1); // ����̖��O�Ɛ������A�j���[�V����
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            DisplayMangaBars(2);
            mangaListManager.DisplayMangaInfoByID(2); // ����̖��O�Ɛ������A�j���[�V����
        }
    }


    public void DisplayMangaBars(int? manga_ID = null)
    {
        MangaData targetMangaData;

        // manga_ID���w�肳��Ă���΁A����Ɋ�Â���MangaData�G���g��������
        if (manga_ID.HasValue)
        {
            targetMangaData = mangaDataSO.manga_DataList.FirstOrDefault(manga => manga.manga_ID == manga_ID.Value);

            mangaListManager.DisplayMangaInfoByID(targetMangaData.manga_ID);

            manga_hyoshi.sprite = targetMangaData.manga_Sprite;
            imageResizer.ResizeImage();

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

        for (int i = 0; i < targetMangaData.manga_Tags.Count; i++) // ����̃W���������e�L�X�g�ɐݒ�
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

                MangaTagWithValue tagWithValue = targetMangaData.manga_Tags[i];
                if (tagWithValue.Tag == MangaTag.����^�O && !string.IsNullOrEmpty(tagWithValue.customTagText))
                {
                    genreTextObjects[i].text = tagWithValue.customTagText;  // Set the text to the content of customTagText
                }

                // �W�������^�O�ɑΉ�����F���e�L�X�g�I�u�W�F�N�g�ɐݒ�
                int colorIndex = tagImageMappingList.mangaTags.IndexOf(targetMangaData.manga_Tags[i].Tag);
                if (colorIndex != -1 && colorIndex < tagImageMappingList.individualTagColors.Count)
                {
                    genreTextObjects[i].color = tagImageMappingList.individualTagColors[colorIndex];
                }
            }
            barImages[i].DOFillAmount(value, 1f).SetEase(Ease.OutExpo); // ����̃W�������o�[���A�j���[�V����
        }

        if (manga_ID.HasValue)
        {
            var manga = mangaDataSO.manga_DataList.Find(m => m.manga_ID == manga_ID.Value);
            if (manga != null)
            {
                List<int?> splitValues = SplitFloatValue(manga.manga_Fav);
                DisplayValuesInTextObjects(splitValues);
            }
        }
    }

    // splitValues���g�p���āA�e�L�X�g�I�u�W�F�N�g���X�g�Ɋe���l��\�����郁�\�b�h
    private void DisplayValuesInTextObjects(List<int?> splitValues)
    {
        for (int i = 0; i < valueTextObjects.Count; i++)
        {
            if (i < splitValues.Count && splitValues[i].HasValue)
            {
                valueTextObjects[i].text = null;
                valueTextObjects[i].text = splitValues[i].Value.ToString();
            }
            else
            {
                valueTextObjects[i].text = "";
            }
        }
        AnimateCounterSequentially(0, splitValues);
    }

    private void AnimateCounterSequentially(int index, List<int?> splitValues)
    {
        if (index >= splitValues.Count)
        {
            return;
        }

        if (splitValues[index].HasValue)
        {
            // �J�E���^�[�A�j���[�V�������J�n����
            AnimateCounterSequentially(index + 1, splitValues);

            typingTexts[index].AnimateCounter(splitValues[index].Value);
        }
        else
        {
            valueTextObjects[index].text = "";
            AnimateCounterSequentially(index + 1, splitValues);  // ���̃e�L�X�g�̃J�E���^�[�A�j���[�V�������J�n
        }
    }


    public static List<int?> SplitFloatValue(float value)
    {
        // �������Ə��������擾
        int integerPart = (int)value;
        int decimalPart = Mathf.RoundToInt((value - integerPart) * 10);  // �������ʂ܂ōl��

        // �������𕶎���ɕϊ����Ċe�������X�g�ɕ���
        List<int?> integerDigits = new List<int?>();
        string integerString = integerPart.ToString();

        // 1���̐������̏ꍇ
        if (integerString.Length == 1)
        {
            integerDigits.Add(null);
            integerDigits.Add(int.Parse(integerString[0].ToString()));
        }
        // 2���ȏ�̐������̏ꍇ
        else
        {
            foreach (char digit in integerString)
            {
                integerDigits.Add(int.Parse(digit.ToString()));
            }
        }

        integerDigits.Add(decimalPart);  // �Ō�̗v�f���������Œu��������

        return integerDigits;
    }
}