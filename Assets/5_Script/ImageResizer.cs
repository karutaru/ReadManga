using UnityEngine;

public class ImageResizer : MonoBehaviour
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

    public void ResizeImage()
    {
        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogWarning("SpriteRenderer or Sprite is null.");
            return;
        }

        float originalWidth = spriteRenderer.sprite.rect.width;
        float originalHeight = spriteRenderer.sprite.rect.height;

        // �A�X�y�N�g����v�Z
        float aspectRatio = originalWidth / originalHeight;
        float targetAspectRatio = TARGET_WIDTH / TARGET_HEIGHT;

        // �^�[�Q�b�g�̃A�X�y�N�g��Ɋ�Â��āA�V�����T�C�Y���v�Z
        float newWidth, newHeight;

        if (aspectRatio > targetAspectRatio)
        {
            // ���̉摜�̕��������Ȃ�A������ɃT�C�Y��ύX
            newWidth = TARGET_WIDTH;
            newHeight = newWidth / aspectRatio;
        }
        else
        {
            // ���̉摜�̕����c���Ȃ�A��������ɃT�C�Y��ύX
            newHeight = TARGET_HEIGHT;
            newWidth = newHeight * aspectRatio;
        }

        // �X�P�[�����v�Z���ēK�p
        Vector3 baseScale = new Vector3((newWidth + additiveOffset.x) / originalWidth, (newHeight + additiveOffset.y) / originalHeight, 1f);
        spriteRenderer.transform.localScale = baseScale;
    }
}
