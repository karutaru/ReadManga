using UnityEngine;
using UnityEngine.EventSystems;

public class ImageScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor = 1.5f;  // �g�傷��{��
    private Vector3 originalScale;    // ���̃T�C�Y��ۑ�����ϐ�

    void Start()
    {
        originalScale = transform.localScale;  // ���̃T�C�Y��ۑ�
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * scaleFactor;  // �}�E�X��������Ƃ��Ɋg��
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;  // �}�E�X�����ꂽ�Ƃ��Ɍ��̃T�C�Y�ɖ߂�
    }
}