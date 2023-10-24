using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChangeTween : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Sprite lastSprite;
    [SerializeField] private float fadeDuration = 0.5f; // アニメーションの持続時間

    public RotateStar rotateStar;
    public RotateTag_1 rotateTag_1;
    public RotateTag_2 rotateTag_2;
    public RotateTag_3 rotateTag_3;

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

            rotateStar.OnSpriteChanged_Star();
            rotateTag_1.OnSpriteChanged_Tag_1();
            rotateTag_2.OnSpriteChanged_Tag_2();
            rotateTag_3.OnSpriteChanged_Tag_3();
        }
    }

    private void OnSpriteChanged()
    {
        // Spriteが変更されたときのアニメーション
        spriteRenderer.DOFade(0, 0); // 透明度を即座に0に設定
        transform.DORotate(new Vector3(0, -100, 0),0f); // 回転を即座に変更

        spriteRenderer.DOFade(1, fadeDuration); // 透明度をfadeDurationの時間で1に変更
        transform.DORotate(new Vector3(0, 0, 0), 0.8f).SetEase(Ease.OutBack); // 回転を0に
    }
}