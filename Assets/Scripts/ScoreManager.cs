using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private Text scoreLabel;
    public int a = 1;

    void Start()
    {
        scoreLabel = GameObject.Find("ScoreLabel").GetComponent<Text>();
        scoreLabel.text = "Score：" + score;
    }


    public void AddScore(int amount)
    {
        score += amount;
        scoreLabel.text = "Score:" + score;
        if(score > 300){
            SceneManager.LoadScene("complete");
        }
    }
}