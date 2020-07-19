using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    private GameObject gamePlayPanel;
    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private Text yourScoreText,scoretextField,highScoreField,gameOverTextField;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        UpdateScore();
    }

    private int currenrScore;
    public int CurrentScore
    {
        get
        {
            return currenrScore;
        }
        set
        {
            currenrScore = value;
            UpdateScore();
        }

    }

    public void YouWin()
    {
        gameOverTextField.text = "You Win!";
        yourScoreText.text = currenrScore.ToString();
        gameOverPanel.SetActive(true);
        GameManager.instance.gameState = GameState.GameOver;
    }
    public void GameOver()
    {
        gameOverTextField.text = "Game Over";
        yourScoreText.text = currenrScore.ToString();
        gameOverPanel.SetActive(true);
        GameManager.instance.gameState = GameState.GameOver;
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void UpdateScore()
    {
       
        if (SaveData.instance.GetValue(Helper.HighScore) < currenrScore)
        {
            SaveData.instance.SetValue(Helper.HighScore, currenrScore);
        }
       
        scoretextField.text = currenrScore.ToString();
        highScoreField.text = SaveData.instance.GetValue(Helper.HighScore).ToString();        
    }


}//class
