using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Result : MonoBehaviour
{
    public Texture S_icon;
    public Texture A_icon;
    public Texture B_icon;
    public Texture C_icon;
    public Texture F_icon;
    public RawImage result_icon;

    public TextMeshProUGUI[] score_texts;

    private void Start() {

        int[] scores = new int[6] {1234, 120, 50, 40, 12, 95};

        score_texts[0].enabled = false;
        score_texts[1].enabled = false;
        score_texts[2].enabled = false;
        score_texts[3].enabled = false;
        score_texts[4].enabled = false;
        score_texts[5].enabled = false;
        result_icon.enabled = false;
        StartCoroutine(displayScore(scores, A_icon));

    }
    private System.Collections.IEnumerator displayScore(int[] scores, Texture rank) {

        float t = 0f;
        float duration = 1f;

        for(int i = 0; i < 6; i++) {
            t = 0f;
            score_texts[i].enabled = true;
            while (t < 1f) {
                t += Time.deltaTime / duration;
                score_texts[i].text = ((int)(scores[i] * t)).ToString();
                if (i == 5) {
                    score_texts[i].text += "%";
                }
                yield return null;
            }
            score_texts[i].text = scores[i].ToString();
            if (i == 5) {
                score_texts[i].text += "%";
            }
        }

        t = 0f;
        while (t < 1f) {
            t += Time.deltaTime / 1.5f;
            yield return null;
        }
        result_icon.texture = rank;
        result_icon.enabled = true;
    }
 


}
