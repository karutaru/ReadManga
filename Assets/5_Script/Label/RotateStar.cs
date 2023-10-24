using DG.Tweening;
using UnityEngine;
using UnityEngine.UI; // �K�v�ȃl�[���X�y�[�X���C���|�[�g

public class RotateStar : MonoBehaviour
{
    private Image image; // Image �R���|�[�l���g���Q�Ƃ���ϐ�

    private void Awake()
    {
        image = GetComponent<Image>(); // Image �R���|�[�l���g���擾
        if (image == null)
        {
            Debug.LogWarning("Image�R���|�[�l���g������");
        }
    }

    public void OnSpriteChanged_Star()
    {
        if (image == null) return; // Image �����݂��Ȃ��ꍇ�A���\�b�h���I��

        GetComponent<RectTransform>().anchoredPosition = new Vector2(554.7f, -420f);

        transform.DORotate(new Vector3(0, 180f, 0f), 0f);

        // �A�j���[�V�����̕ύX
        transform.DORotate(new Vector3(0, 0, 0f), 0.5f).SetEase(Ease.OutExpo);
    }
}
