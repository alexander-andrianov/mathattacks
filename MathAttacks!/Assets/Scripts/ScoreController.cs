using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private GameSettings gameSettings;

    private Text score;
    private Animator scoreAnimator;

    private int zero = 0;

    void Start()
    {
        TargetController.OnScoreUpdated += ChangeScore;

        GetComponent<Text>().text = zero.ToString();
        score = GetComponent<Text>();
        scoreAnimator = GetComponent<Animator>();
    }

    void ChangeScore()
    {
        if (score != null)
        {
            score.text = gameSettings.CurrentScore.ToString();
            scoreAnimator.Play("Base Layer.score-change-animation", 0, 0.25f);

            if(gameSettings.CurrentScore >= gameSettings.WinLimit)
            {
                GameObject.FindWithTag("UI").transform.GetChild(1).gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
