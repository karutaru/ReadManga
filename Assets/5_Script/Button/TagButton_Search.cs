using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagButton_Search : MonoBehaviour
{
    //public ReceiverScript receiver; // ドラッグ&ドロップでReceiverScriptをアタッチする

    public bool search;


    // ボタンが押されたときに呼ばれるメソッド
    public void OnButtonPressed()
    {
        //targetScript.ReceiveSignal(true); // 他のスクリプトのメソッドを呼ぶ

        gameObject.SetActive(false); // 自身を非表示にする
    }
}
