using UnityEngine;

public class ImageResizer2880 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // ���̃I�u�W�F�N�g��SpriteRenderer�������Ŏ擾���܂�

    private const float TARGET_WIDTH = 1440f;
    private const float TARGET_HEIGHT = 2880f;

    [Tooltip("Additive offset to apply after resizing.")]
    public Vector2 additiveOffset = Vector2.zero; // �C���X�y�N�^�[����ҏW�\�ȉ��Z�I�t�Z�b�g

    private bool resized = false; // ResizeImage���Ă΂ꂽ���ǂ�����ǐՂ���t���O

    private void Start()
    {
        // ���̃I�u�W�F�N�g��SpriteRenderer���擾
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!resized)
        {
            ResizeImage();
            resized = true;
        }
    }

    public void ResizeImage()
    {
        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            return;
        }

        // �X�P�[�������Z�b�g
        spriteRenderer.transform.localScale = Vector3.one;

        float originalWidth = spriteRenderer.sprite.rect.width;
        float originalHeight = spriteRenderer.sprite.rect.height;

        // �A�X�y�N�g����v�Z
        float aspectRatio = originalWidth / originalHeight;
        float targetAspectRatio = TARGET_WIDTH / TARGET_HEIGHT;

        // �^�[�Q�b�g�̃A�X�y�N�g��Ɋ�Â��āA�V�����T�C�Y���v�Z
        float newWidth, newHeight;

        if (aspectRatio < targetAspectRatio)
        {
            // ���̉摜�̕����c���Ȃ�A������ɃT�C�Y��ύX
            newWidth = TARGET_WIDTH;
            newHeight = newWidth / aspectRatio;
        }
        else
        {
            // ���̉摜�̕��������������A�X�y�N�g��Ȃ�A��������ɃT�C�Y��ύX
            newHeight = TARGET_HEIGHT;
            newWidth = newHeight * aspectRatio;
        }

        // �X�P�[�����v�Z���ēK�p
        Vector3 baseScale = new Vector3((newWidth + additiveOffset.x) / originalWidth, (newHeight + additiveOffset.y) / originalHeight, 1f);
        spriteRenderer.transform.localScale = baseScale;
    }
}