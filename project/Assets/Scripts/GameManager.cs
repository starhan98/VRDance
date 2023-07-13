using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public TMP_Text counter;
    public TMP_Text Score;
    private int currentCountdownValue = 3;
    private int score_count = 7;
    private string[] score_list = new string[] {"Good", "Bad", "Miss", "Bad", "Miss", "Good", "Miss", "Bad"};
    public VideoPlayer mv;

    private void Start()
    {
        mv.Pause();
        StartCoroutine(StartCountdown());
        StartCoroutine(StartScore());
    }

    private System.Collections.IEnumerator StartCountdown()
    {
        while (currentCountdownValue > 0)
        {
            counter.text = currentCountdownValue.ToString();
            if (currentCountdownValue == 3) {
                counter.color = Color.blue;
            } else if (currentCountdownValue == 2) {
                counter.color = Color.green;
            } else if (currentCountdownValue == 1) {
                counter.color = Color.red;
            }
            yield return new WaitForSeconds(1);
            currentCountdownValue--;
        }

        counter.text = "";
        mv.Play();
    }

    private System.Collections.IEnumerator StartScore() {
        yield return new WaitForSeconds(4);
        
        while (score_count > 0)
        {
            Score.text = score_list[score_count];
            if (Score.text == "Perfect") {
                Score.color = Color.blue;
            } else if (Score.text == "Good") {
                Score.color = Color.green;
            } else if (Score.text == "Bad") {
                Score.color = Color.red;
            } else {
                Score.color = Color.gray;
            }
            yield return new WaitForSeconds(2);
            score_count--;
        }

        counter.text = "";
    }
    
}
