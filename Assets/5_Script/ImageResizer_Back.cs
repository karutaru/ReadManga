using UnityEngine;

public class ImageResizer_Back : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // このオブジェクトのSpriteRendererを自動で取得します

    private const float TARGET_WIDTH = 500f;
    private const float TARGET_HEIGHT = 650f;

    [Tooltip("Additive offset to apply after resizing.")]
    public Vector2 additiveOffset = Vector2.zero; // インスペクターから編集可能な加算オフセット

    private void Start()
    {
        // このオブジェクトのSpriteRendererを取得
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

        // スケールを計算して適用
        Vector3 baseScale = new Vector3((TARGET_WIDTH + additiveOffset.x) / originalWidth, (TARGET_HEIGHT + additiveOffset.y) / originalHeight, 1f);
        spriteRenderer.transform.localScale = baseScale;
    }
}
