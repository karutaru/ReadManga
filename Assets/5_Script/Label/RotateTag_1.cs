using DG.Tweening;
using UnityEngine;
using UnityEngine.UI; // �K�v�ȃl�[���X�y�[�X���C���|�[�g

public class RotateTag_1 : MonoBehaviour
{
    private Image image; // Image �R���|�[�l���g���Q�Ƃ���ϐ�

    private void Awake()
    {
        image = GetComponent<Image>(); // Image �R���|�[�l���g���擾
        if (image == null)
        {
            Debug.LogWarning("Image���Ȃ�");
        }
    }

    public void OnSpriteChanged_Tag_1()
    {
        if (image == null) return; // Image �����݂��Ȃ��ꍇ�A���\�b�h���I��

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // �A�j���[�V�����̕ύX
        transform.DOShakePosition(duration: 0.5f,strength: 10f).SetEase(Ease.OutExpo);
    }
}
