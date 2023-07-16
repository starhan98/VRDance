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
    public AudioSource SFX;
    public AudioClip score_sound;
    public AudioClip rank_sound;
    public SongInfo selected_song;
    public TextMeshProUGUI song_name;
    public RawImage thumbnail;

    private void Start() {

        selected_song = GameObject.Find("SelectedSong").GetComponent<SongInfo>();
        thumbnail.texture = selected_song.thumbnail;
        song_name.text = selected_song.name;

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
        float duration = 0.7f;

        
        t = 0f;
        while (t < 1f) {
            t += Time.deltaTime / 1f;
            yield return null;
        }


        SFX.clip = score_sound;
        SFX.loop = true;
        SFX.Play();
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
        result_icon.texture = rank;
        result_icon.enabled = true;
    }
 


}
