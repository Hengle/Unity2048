using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DealMessage : MonoBehaviour {
    /// <summary>
    /// 开始面板 游戏面板  再次游戏按钮
    /// </summary>
    public Transform PanelStart, PaneleAgain,againBtn;
    /// <summary>
    /// 精灵预设
    /// </summary>
    public GameObject goPrefab;
    /// <summary>
    /// 计分 计时文本
    /// </summary>
    public Text textSocre, textTime;
    /// <summary>
    /// 时间变量
    /// </summary>
    private float fTime = 0.0f;
    private int iMin = 0, iSec = 0;

	// Use this for initialization
	void Start () {
        Function2048.getInstance().DrawBackGround(goPrefab, PaneleAgain);
	}
	
	// Update is called once per frame
	void Update () {
        if (Function2048.getInstance().isStopGame)
            againBtn.gameObject.SetActive(true);
        else
        {
            fTime += Time.fixedDeltaTime;
            iSec = (int)fTime;
            if (iSec == 60)
            {
                iMin++;
                iSec = 0;
                fTime -= 60;
            }
            textSocre.text = "Socre: "+Function2048.getInstance().iSocre;
            textTime.text = "Time: " + iMin + "m " + iSec + "s";
        }
        Function2048.getInstance().DealUpdateGame();
	}
    /// <summary>
    ///开始游戏
    /// </summary>
    public void BtnStartGame()
    {
        Function2048.getInstance().InitGame();
        PaneleAgain.gameObject.SetActive(true);
        Destroy(PanelStart.gameObject);
        againBtn.gameObject.SetActive(false);
    }
    /// <summary>
    /// 再次游戏
    /// </summary>
    public void BtnAgainGame()
    {
        fTime = 0.0f;
        iMin = 0;
        iSec = 0;
        againBtn.gameObject.SetActive(false);
        Function2048.getInstance().InitGame();
        againBtn.gameObject.SetActive(false);
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void BtnQuitGame()
    {
        Application.Quit();
    }

}
