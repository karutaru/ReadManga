using UnityEngine;

public class AddPaddingToTexture : MonoBehaviour
{
    public Texture2D originalTexture; // 元のテクスチャ
    public int padding; // 追加する余白の大きさ

    void Start()
    {
        // デバッグ情報の出力
        Debug.Log($"Original Texture Size: {originalTexture.width}x{originalTexture.height}");
        Debug.Log($"Padding Value: {padding}");

        Texture2D paddedTexture = AddPadding(originalTexture, padding);

        Debug.Log($"New Texture Size: {paddedTexture.width}x{paddedTexture.height}");

        GetComponent<SpriteRenderer>().sprite = Sprite.Create(paddedTexture, new Rect(0, 0, paddedTexture.width, paddedTexture.height), new Vector2(0.5f, 0.5f));
    }

    Texture2D AddPadding(Texture2D original, int padding)
    {
        int newWidth = original.width + padding * 2;
        int newHeight = original.height + padding * 2;

        Texture2D newTexture = new Texture2D(newWidth, newHeight, TextureFormat.ARGB32, false);
        Color32[] clearPixels = new Color32[newWidth * newHeight];
        for (int i = 0; i < clearPixels.Length; i++)
        {
            clearPixels[i] = new Color32(0, 0, 0, 0);
        }
        newTexture.SetPixels32(clearPixels);

        // 元のテクスチャを中心に配置
        for (int y = 0; y < original.height; y++)
        {
            for (int x = 0; x < original.width; x++)
            {
                newTexture.SetPixel(x + padding, y + padding, original.GetPixel(x, y));
            }
        }

        newTexture.Apply();

        return newTexture;
    }
}
