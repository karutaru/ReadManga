using DG.Tweening;
using UnityEngine;
using UnityEngine.UI; // 必要なネームスペースをインポート

public class RotateTag_3 : MonoBehaviour
{
    private Image image; // Image コンポーネントを参照する変数

    private void Awake()
    {
        image = GetComponent<Image>(); // Image コンポーネントを取得
        if (image == null)
        {
            Debug.LogWarning("Image component is not attached to this game object.");
        }
    }

    public void OnSpriteChanged_Tag_3()
    {
        if (image == null) return; // Image が存在しない場合、メソッドを終了

        GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -190f);

        // アニメーションの変更
        transform.DOShakePosition(duration: 0.5f,strength: 10f).SetEase(Ease.OutExpo);
    }
}
