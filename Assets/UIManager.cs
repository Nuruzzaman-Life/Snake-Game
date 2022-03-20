using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public TextMeshProUGUI scoreText;

    private int currentScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        scoreText.text = currentScore.ToString();
    }
    public void DeductPoints(int points)
    {
        currentScore -= points;
        if (currentScore < 0)
        {
            GameOver();
        }
        else
        {
            scoreText.text = currentScore.ToString();
        }
        
    }
}
