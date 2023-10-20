using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypingText : MonoBehaviour
{
    public float delay = 0.05f;  // �����Ԃ̒x������
    private Text uiText;
    private Coroutine typingCoroutine;  // �^�C�s���O�R���[�`���̎Q��

    private void Awake()
    {
        uiText = GetComponent<Text>();
    }

    public void TypeText(string message)
    {
        // �����̃^�C�s���O�R���[�`�������s���̏ꍇ�A������~
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        // �V�����^�C�s���O�R���[�`�����J�n
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
        typingCoroutine = null;  // �^�C�s���O������������R���[�`���̎Q�Ƃ����Z�b�g
    }
}