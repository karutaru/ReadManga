using UnityEngine;

public class ImageResizer2880 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // このオブジェクトのSpriteRendererを自動で取得します

    private const float TARGET_WIDTH = 1440f;
    private const float TARGET_HEIGHT = 2880f;

    [Tooltip("Additive offset to apply after resizing.")]
    public Vector2 additiveOffset = Vector2.zero; // インスペクターから編集可能な加算オフセット

    private bool resized = false; // ResizeImageが呼ばれたかどうかを追跡するフラグ

    private void Start()
    {
        // このオブジェクトのSpriteRendererを取得
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

        // スケールをリセット
        spriteRenderer.transform.localScale = Vector3.one;

        float originalWidth = spriteRenderer.sprite.rect.width;
        float originalHeight = spriteRenderer.sprite.rect.height;

        // アスペクト比を計算
        float aspectRatio = originalWidth / originalHeight;
        float targetAspectRatio = TARGET_WIDTH / TARGET_HEIGHT;

        // ターゲットのアスペクト比に基づいて、新しいサイズを計算
        float newWidth, newHeight;

        if (aspectRatio < targetAspectRatio)
        {
            // 元の画像の方が縦長なら、幅を基準にサイズを変更
            newWidth = TARGET_WIDTH;
            newHeight = newWidth / aspectRatio;
        }
        else
        {
            // 元の画像の方が横長か同じアスペクト比なら、高さを基準にサイズを変更
            newHeight = TARGET_HEIGHT;
            newWidth = newHeight * aspectRatio;
        }

        // スケールを計算して適用
        Vector3 baseScale = new Vector3((newWidth + additiveOffset.x) / originalWidth, (newHeight + additiveOffset.y) / originalHeight, 1f);
        spriteRenderer.transform.localScale = baseScale;
    }
}