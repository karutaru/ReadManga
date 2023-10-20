using UnityEngine;

public class ImageResizer_Back : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // ���̃I�u�W�F�N�g��SpriteRenderer�������Ŏ擾���܂�

    private const float TARGET_WIDTH = 500f;
    private const float TARGET_HEIGHT = 650f;

    [Tooltip("Additive offset to apply after resizing.")]
    public Vector2 additiveOffset = Vector2.zero; // �C���X�y�N�^�[����ҏW�\�ȉ��Z�I�t�Z�b�g

    private void Start()
    {
        // ���̃I�u�W�F�N�g��SpriteRenderer���擾
        spriteRenderer = GetComponent<SpriteRenderer>();

        ResizeImage();
    }

    private void ResizeImage()
    {
        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogWarning("SpriteRenderer or Sprite is null.");
            return;
        }

        float originalWidth = spriteRenderer.sprite.rect.width;
        float originalHeight = spriteRenderer.sprite.rect.height;

        // �X�P�[�����v�Z���ēK�p
        Vector3 baseScale = new Vector3((TARGET_WIDTH + additiveOffset.x) / originalWidth, (TARGET_HEIGHT + additiveOffset.y) / originalHeight, 1f);
        spriteRenderer.transform.localScale = baseScale;
    }
}
