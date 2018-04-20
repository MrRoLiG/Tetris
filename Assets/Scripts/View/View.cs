using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class View : MonoBehaviour {

    private RectTransform gameTitle;
    private RectTransform menuUI;
    private RectTransform inGameUI;

    private GameObject restartBtn;
    private GameObject gameOverUI;
    private GameObject settingUI;
    private GameObject mute;
    private GameObject recordUI;

    private Text inGameScoreText;
    private Text inGameHighScoreText;
    private Text gameOverScoreText;
    private Text recordScoreText;
    private Text recordHighScoreText;
    private Text recordGameTimesText;

	// Use this for initialization
	void Awake () {

        gameTitle = transform.Find("Canvas/GameTitle") as RectTransform;
        menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
        inGameUI = transform.Find("Canvas/InGameUI") as RectTransform;

        restartBtn = transform.Find("Canvas/MenuUI/Btn_Restart").gameObject;
        gameOverUI = transform.Find("Canvas/GameOverUI").gameObject;
        settingUI = transform.Find("Canvas/SettingUI").gameObject;
        mute = transform.Find("Canvas/SettingUI/Btn_Setting_Sound/Mute").gameObject;
        recordUI = transform.Find("Canvas/RecordUI").gameObject;

        inGameScoreText = transform.Find("Canvas/InGameUI/CurrentScore/Score").GetComponent<Text>();
        inGameHighScoreText = transform.Find("Canvas/InGameUI/HighestScore/Score").GetComponent<Text>();
        gameOverScoreText = transform.Find("Canvas/GameOverUI/ScoreText").GetComponent<Text>();

        recordScoreText = transform.Find("Canvas/RecordUI/CurrentScore/Score").GetComponent<Text>();
        recordHighScoreText = transform.Find("Canvas/RecordUI/HighestScore/Score").GetComponent<Text>();
        recordGameTimesText = transform.Find("Canvas/RecordUI/GameTimes/Score").GetComponent<Text>();
	}

    public void ShowMenuUI()
    {
        gameTitle.gameObject.SetActive(true);
        gameTitle.DOAnchorPosY(-152.0f, 0.5f);
        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(64.0f, 0.5f);
    }

    public void HideMenuUI()
    {
        gameTitle.DOAnchorPosY(100.0f, 0.5f)
            .OnComplete(delegate { gameTitle.gameObject.SetActive(false); });
        menuUI.DOAnchorPosY(-95.0f,0.5f)
            .OnComplete(delegate{ menuUI.gameObject.SetActive(false); });
    }

    public void ShowInGameUI(int score=0, int highScore=0)
    {
        this.inGameScoreText.text = score.ToString();
        this.inGameHighScoreText.text = highScore.ToString();

        inGameUI.gameObject.SetActive(true);
        inGameUI.DOAnchorPosY(-88.0f, 0.5f);
    }

    public void UpdateInGameUI(int score, int highScore)
    {
        this.inGameScoreText.text = score.ToString();
        this.inGameHighScoreText.text = highScore.ToString();
    }

    public void HideInGameUI()
    {
        inGameUI.DOAnchorPosY(82.0f, 0.5f)
            .OnComplete(delegate { inGameUI.gameObject.SetActive(false); });
    }

    public void ShowRestartBtn()
    {
        restartBtn.gameObject.SetActive(true);
    }

    public void ShowGameOverUI(int score=0)
    {
        gameOverUI.gameObject.SetActive(true);
        gameOverScoreText.text = score.ToString();
    }

    public void HideGameOverUI()
    {
        gameOverUI.gameObject.SetActive(false);
    }

    public void ShowSettingUI()
    {
        settingUI.gameObject.SetActive(true);
    }
    public void HideSettingUI()
    {
        settingUI.gameObject.SetActive(false);
    }

    public void SetMuteBtn(bool isMute)
    {
        mute.SetActive(isMute);
    }

    public void ShowRecordUI(int score, int highScore, int gameTimes ,bool isShow)
    {
        if(isShow)
        {
            recordScoreText.text = score.ToString();
            recordHighScoreText.text = highScore.ToString();
            recordGameTimesText.text = gameTimes.ToString();
        }

        recordUI.SetActive(isShow);
    }
    
}
