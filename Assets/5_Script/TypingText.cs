using System.Collections;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TypingText : MonoBehaviour
{
    public float typeSpeed = 0.05f;
    public Action OnTypeComplete;
    public float counterDuration = 0.2f;  // �J�E���^�[�A�j���[�V�����̎���

    private Tween currentTween;  // ���݂�Tween�A�j���[�V������ǐՂ���

    public void TypeText(string textToType)
    {
        StopAllCoroutines(); // �����̃^�C�s���O�𒆎~
        if (currentTween != null)
        {
            currentTween.Kill();  // �����̃A�j���[�V�������L��
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

        OnTypeComplete?.Invoke(); // �^�C�s���O������������R�[���o�b�N���Ăяo��
    }

    public void AnimateCounter(int targetValue)
    {
        int currentValue = 0;

        // �����̃A�j���[�V�������L��
        if (currentTween != null)
        {
            currentTween.Kill();
        }

        // Text�R���|�[�l���g�̃e�L�X�g�����Z�b�g
        GetComponent<Text>().text = "0";

        // �V�����A�j���[�V�������A�T�C��
        currentTween = DOTween.To(() => currentValue, x => currentValue = x, targetValue, counterDuration)
            .OnUpdate(() =>
            {
                GetComponent<Text>().text = currentValue.ToString();
            })
            .OnComplete(() => OnTypeComplete?.Invoke());
    }
}