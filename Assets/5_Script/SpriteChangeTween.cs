using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChangeTween : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Sprite lastSprite;
    [SerializeField] private float fadeDuration = 0.5f; // �A�j���[�V�����̎�������

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
        // Sprite���ύX���ꂽ���ǂ������m�F
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
        // Sprite���ύX���ꂽ�Ƃ��̃A�j���[�V����
        spriteRenderer.DOFade(0, 0); // �����x�𑦍���0�ɐݒ�
        transform.DORotate(new Vector3(0, -100, 0),0f); // ��]�𑦍��ɕύX

        spriteRenderer.DOFade(1, fadeDuration); // �����x��fadeDuration�̎��Ԃ�1�ɕύX
        transform.DORotate(new Vector3(0, 0, 0), 0.8f).SetEase(Ease.OutBack); // ��]��0��
    }
}