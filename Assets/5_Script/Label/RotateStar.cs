using DG.Tweening;
using UnityEngine;
using UnityEngine.UI; // 必要なネームスペースをインポート

public class RotateStar : MonoBehaviour
{
    private Image image; // Image コンポーネントを参照する変数

    private void Awake()
    {
        image = GetComponent<Image>(); // Image コンポーネントを取得
        if (image == null)
        {
            Debug.LogWarning("Imageコンポーネントが無い");
        }
    }

    public void OnSpriteChanged_Star()
    {
        if (image == null) return; // Image が存在しない場合、メソッドを終了

        GetComponent<RectTransform>().anchoredPosition = new Vector2(554.7f, -420f);

        transform.DORotate(new Vector3(0, 180f, 0f), 0f);

        // アニメーションの変更
        transform.DORotate(new Vector3(0, 0, 0f), 0.5f).SetEase(Ease.OutExpo);
    }
}
