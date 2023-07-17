using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Result : MonoBehaviour
{
    public Texture S_icon;
    public Texture A_icon;
    public Texture B_icon;
    public Texture C_icon;
    public Texture F_icon;
    public RawImage result_icon;

    public TextMeshProUGUI[] score_texts;
    public AudioSource SFX;
    public AudioClip score_sound;
    public AudioClip rank_sound;
    public SongInfo selected_song;
    public TextMeshProUGUI song_name;
    public RawImage thumbnail;
    private int[] score_distribute = new int[5] {1000, 800, 600, 200, -200};
    private float[] acc_distribute = new float[5] {1, 0.8f, 0.5f, 0.2f, 0};

    private void Start() {

        selected_song = GameObject.Find("SelectedSong").GetComponent<SongInfo>();
        thumbnail.texture = selected_song.thumbnail;
        song_name.text = selected_song.name;

        score_texts[0].enabled = false;
        score_texts[1].enabled = false;
        score_texts[2].enabled = false;
        score_texts[3].enabled = false;
        score_texts[4].enabled = false;
        score_texts[5].enabled = false;
        score_texts[6].enabled = false;
        result_icon.enabled = false;
        StartCoroutine(displayScore(calAccuracy(selected_song.score_cnt)));

    }
    private System.Collections.IEnumerator displayScore(int[] scores) {

        float t = 0f;
        float duration = 0.7f;

        
        t = 0f;
        while (t < 1f) {
            t += Time.deltaTime / 1f;
            yield return null;
        }


        SFX.clip = score_sound;
        SFX.loop = true;
        SFX.Play();
        for(int i = 0; i < 7; i++) {
            t = 0f;
            score_texts[i].enabled = true;
            
            while (t < 1f) {
                t += Time.deltaTime / duration;
                score_texts[i].text = ((int)(scores[i] * t)).ToString();
                if (i == 6) {
                    score_texts[i].text += "%";
                }
                yield return null;
            }
            score_texts[i].text = scores[i].ToString();
            if (i == 6) {
                score_texts[i].text += "%";
            }
            t = 0f;
            while (t < 1f) {
                t += Time.deltaTime / 0.1f;
                yield return null;
            }
        }
        SFX.Stop();
        SFX.loop = false;

        t = 0f;
        while (t < 1f) {
            t += Time.deltaTime / 1.5f;
            yield return null;
        }
        SFX.clip = rank_sound;

        SFX.Play();
        int acc = scores[6];
        if (acc > 90) {
            result_icon.texture = S_icon;
        }
        else if (acc > 75) {
            result_icon.texture = A_icon;
        }
        else if (acc > 60) {
            result_icon.texture = B_icon;
        }
        else if (acc > 30) {
            result_icon.texture = C_icon;
        } else {
            result_icon.texture = F_icon;
        }
        result_icon.enabled = true;
    }
 
    private int[] calAccuracy(int[] score_cnt) {
        int[] scores = new int[7];
        float acc = 0;
        int total_score = 0;
        for (int i = 0; i < 5; i++) {
            scores[i] = score_cnt[i] * score_distribute[i];
            total_score += score_cnt[i] * score_distribute[i];
            acc += score_cnt[i] * acc_distribute[i];
        }
        scores[5] = total_score;
        scores[6] = (int)(acc * 100 / score_cnt.Sum());
        return scores;

    }

}
