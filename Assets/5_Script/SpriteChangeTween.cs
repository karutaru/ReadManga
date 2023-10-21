using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChangeTween : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Sprite lastSprite;
    [SerializeField] private float fadeDuration = 0.5f; // アニメーションの持続時間

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastSprite = spriteRenderer.sprite;
    }

    private void Update()
    {
        // Spriteが変更されたかどうかを確認
        if (lastSprite != spriteRenderer.sprite)
        {
            OnSpriteChanged();
            lastSprite = spriteRenderer.sprite;
        }
    }

    private void OnSpriteChanged()
    {
        // Spriteが変更されたときのアニメーション
        spriteRenderer.DOFade(0, 0); // 透明度を即座に0に設定
        spriteRenderer.DOFade(1, fadeDuration); // 透明度をfadeDurationの時間で1に変更
    }
}