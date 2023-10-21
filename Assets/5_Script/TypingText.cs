using System.Collections;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TypingText : MonoBehaviour
{
    public float typeSpeed = 0.05f;
    public Action OnTypeComplete;
    public float counterDuration = 0.2f;  // カウンターアニメーションの時間

    private Tween currentTween;  // 現在のTweenアニメーションを追跡する

    public void TypeText(string textToType)
    {
        StopAllCoroutines(); // 既存のタイピングを中止
        if (currentTween != null)
        {
            currentTween.Kill();  // 既存のアニメーションをキル
        }
        StartCoroutine(TypeCoroutine(textToType));
    }

    private IEnumerator TypeCoroutine(string textToType)
    {
        Text textComponent = GetComponent<Text>();
        textComponent.text = "";

        foreach (char letter in textToType.ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        OnTypeComplete?.Invoke(); // タイピングが完了したらコールバックを呼び出す
    }

    public void AnimateCounter(int targetValue)
    {
        int currentValue = 0;

        // 既存のアニメーションをキル
        if (currentTween != null)
        {
            currentTween.Kill();
        }

        // Textコンポーネントのテキストをリセット
        GetComponent<Text>().text = "0";

        // 新しいアニメーションをアサイン
        currentTween = DOTween.To(() => currentValue, x => currentValue = x, targetValue, counterDuration)
            .OnUpdate(() =>
            {
                GetComponent<Text>().text = currentValue.ToString();
            })
            .OnComplete(() => OnTypeComplete?.Invoke());
    }
}