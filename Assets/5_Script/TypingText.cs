using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypingText : MonoBehaviour
{
    public float delay = 0.05f;  // 文字間の遅延時間
    private Text uiText;
    private Coroutine typingCoroutine;  // タイピングコルーチンの参照

    private void Awake()
    {
        uiText = GetComponent<Text>();
    }

    public void TypeText(string message)
    {
        // 既存のタイピングコルーチンが実行中の場合、それを停止
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        // 新しいタイピングコルーチンを開始
        typingCoroutine = StartCoroutine(TypeTextCoroutine(message));
    }

    private IEnumerator TypeTextCoroutine(string message)
    {
        uiText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            uiText.text += letter;
            yield return new WaitForSeconds(delay);
        }
        typingCoroutine = null;  // タイピングが完了したらコルーチンの参照をリセット
    }
}