using DG.Tweening;
using UnityEngine;
using UnityEngine.UI; // 必要なネームスペースをインポート

public class RotateTag_1 : MonoBehaviour
{
    private Image image; // Image コンポーネントを参照する変数

    private void Awake()
    {
        image = GetComponent<Image>(); // Image コンポーネントを取得
        if (image == null)
        {
            Debug.LogWarning("Imageがない");
        }
    }

    public void OnSpriteChanged_Tag_1()
    {
        if (image == null) return; // Image が存在しない場合、メソッドを終了

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // アニメーションの変更
        transform.DOShakePosition(duration: 0.5f,strength: 10f).SetEase(Ease.OutExpo);
    }
}
