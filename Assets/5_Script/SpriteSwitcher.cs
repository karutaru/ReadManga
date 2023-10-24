using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpriteSwitcher : MonoBehaviour
{
    public ImageResizer2880 imageResizer2880;

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public float fadeDuration = 1.0f;

    private Color originalColor;

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            originalColor = spriteRenderer.color;
        }
    }

    public void SwitchSprite(Sprite newSprite)
    {
        if (spriteRenderer.sprite != null && newSprite != null)
        {
            // 現在のSpriteのColorを戻す
            spriteRenderer.color = originalColor;

            // Spriteをフェードアウト
            spriteRenderer.DOColor(new Color(originalColor.r, originalColor.g, originalColor.b, 0), fadeDuration / 2).OnComplete(() =>
            {
                imageResizer2880.ResizeImage();
                // Spriteを切り替え
                spriteRenderer.sprite = newSprite;

                // 新しいSpriteをフェードイン
                spriteRenderer.DOColor(originalColor, fadeDuration / 2);
            });
        }
    }
}