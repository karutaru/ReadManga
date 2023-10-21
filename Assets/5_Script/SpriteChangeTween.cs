using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChangeTween : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Sprite lastSprite;
    [SerializeField] private float fadeDuration = 0.5f; // �A�j���[�V�����̎�������

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastSprite = spriteRenderer.sprite;
    }

    private void Update()
    {
        // Sprite���ύX���ꂽ���ǂ������m�F
        if (lastSprite != spriteRenderer.sprite)
        {
            OnSpriteChanged();
            lastSprite = spriteRenderer.sprite;
        }
    }

    private void OnSpriteChanged()
    {
        // Sprite���ύX���ꂽ�Ƃ��̃A�j���[�V����
        spriteRenderer.DOFade(0, 0); // �����x�𑦍���0�ɐݒ�
        spriteRenderer.DOFade(1, fadeDuration); // �����x��fadeDuration�̎��Ԃ�1�ɕύX
    }
}