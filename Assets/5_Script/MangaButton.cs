using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MangaButton : MonoBehaviour
{
    public int mangaID;
    public MangaBarsDisplay mangaBarsDisplay; // MangaBarsDisplayへの参照を追加

    private void Awake()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        // MangaBarsDisplayのメソッドを呼び出してmanga_IDを渡す
        mangaBarsDisplay.DisplayMangaBars(mangaID);
    }

    public void Setup(int id, MangaBarsDisplay display)
    {
        mangaID = id;
        mangaBarsDisplay = display; // MangaBarsDisplayのインスタンスをセットアップ
    }
}