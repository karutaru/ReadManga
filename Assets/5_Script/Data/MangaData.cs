using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum MangaTag
{
    �Ȃ�,
    �ق̂ڂ�,
    �R���f�B�[,
    �o�g��,
    ����,
    �}�j�A�b�N,
    ����,
    �T,
    �O��,
    �G��,
    ����^�O
}

[System.Serializable]
public class MangaTagWithValue
{
    [ValueDropdown("GetAllMangaTags")]
    public MangaTag Tag;

    [ShowIf("IsCustomTagSelected")]
    public string customTagText;  // �J�X�^���^�O�̖��O

    [Range(0, 10)]
    public float Value;

    private IEnumerable<MangaTag> GetAllMangaTags()
    {
        return System.Enum.GetValues(typeof(MangaTag)) as IEnumerable<MangaTag>;
    }

    // "����^�O"���I������邩
    private bool IsCustomTagSelected()
    {
        return Tag == MangaTag.����^�O;
    }
}




[System.Serializable]
public class MangaData
{
    [LabelText("�����ID"), HideLabel]
    public int manga_ID;            // �����ID
    [LabelText("����̖��O"), HideLabel]
    public string manga_Name;       // ����̖��O
    [LabelText("����̃��r"), HideLabel]
    public string manga_ruby;       // ����̃��r

    [Range(0, 10)]
    [LabelText("����̂��C�ɓ���x"), HideLabel]
    public float manga_Fav;          // ����̂��C�ɓ���x

    [LabelText("�W�������Ƃ��̐��l"), HideLabel]
    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<MangaTagWithValue> manga_Tags;  // �W�������Ƃ��̐��l

    [LabelText("����̐���"), HideLabel]
    [SerializeField, Multiline(4)]  // ����̐���
    public string explanation;

    [LabelText("����̃A�C�R���p�^�C�g���摜"), HideLabel]
    public Sprite manga_Title;     // ����̃A�C�R���p�^�C�g���摜
    [LabelText("����̕\��"), HideLabel]
    public Sprite manga_Sprite;     // ����̕\��
    [LabelText("����̃I�X�X���̃R�}"), HideLabel]
    public Sprite manga_Page;       // ����̃I�X�X���̃R�}


    private void OnValidate()
    {
        // manga_Tags���X�g��3�ȏ�̗v�f�������Ă���ꍇ�A����𐧌�����
        if (manga_Tags.Count > 3)
        {
            manga_Tags.RemoveRange(3, manga_Tags.Count - 3);
            Debug.LogWarning("�^�O��3�ȉ��ɐݒ肵�Ă��������B");
        }
    }
}
