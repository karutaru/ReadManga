using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagButton_Search : MonoBehaviour
{
    //public ReceiverScript receiver; // �h���b�O&�h���b�v��ReceiverScript���A�^�b�`����

    public bool search;


    // �{�^���������ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    public void OnButtonPressed()
    {
        //targetScript.ReceiveSignal(true); // ���̃X�N���v�g�̃��\�b�h���Ă�

        gameObject.SetActive(false); // ���g���\���ɂ���
    }
}
