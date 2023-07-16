using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public VideoPlayer mv_player;
    public SongInfo selected_song;
    public float song_speed;
    public int song_mode;
    public Texture[] numbers;
    public RawImage countdown;
    public RawImage stop_pos;
    public Image panel;

    public HpBarManager hpBarManager;


    void Start() {

        selected_song = GameObject.Find("SelectedSong").GetComponent<SongInfo>();
        hpBarManager = GameObject.Find("HpBar").GetComponent<HpBarManager>();

        stop_pos.texture = selected_song.stop_pos;
        mv_player.clip = selected_song.shorts;
        song_speed = selected_song.speed;
        song_mode = selected_song.mode;
        
        mv_player.Prepare();
        countdown.enabled = false;
        countdown.texture = numbers[0];

        StartCoroutine(FadeIn());

    }

    private IEnumerator Countdown() {
        float t = 0f;
        countdown.enabled = true;
        while (t < 1) {
            t += Time.deltaTime / 1.0f;
            yield return null;
        }
        countdown.texture = numbers[1];
        t = 0f;

        while (t < 1) {
            t += Time.deltaTime / 1.0f;
            yield return null;
        }
        countdown.texture = numbers[2];
        t = 0f;

        while (t < 1) {
            t += Time.deltaTime / 1.0f;
            yield return null;
        }
        countdown.enabled = false;
        
        mv_player.Play();
        StartCoroutine(FadeOut(mv_player.length));
        hpBarManager.StartGame();


    }


    private IEnumerator FadeIn() {

        float t = 0f;
        float duration = 1.5f;

        while (t < 1f) {
            t += Time.deltaTime / duration;
            panel.color = new Color(0, 0, 0, 1 - t);
            yield return null;
        }
        panel.color = new Color(0, 0, 0, 0);
        panel.enabled = false;
        StartCoroutine(Countdown());
    }

    private IEnumerator FadeOut(double video_len) {
        float t = 0f;
        float duration = 1.5f;
        Debug.Log(Time.deltaTime / ((float)video_len - 1));
        while (t < 1f) {
            t += Time.deltaTime / ((float)video_len - 1);
            yield return null;
        }

        Debug.Log(Time.deltaTime / ((float)video_len - 1));
        panel.enabled = true;
        t = 0f;

        while (t < 1f) {
            t += Time.deltaTime / duration;
            panel.color = new Color(0, 0, 0, t);
            mv_player.SetDirectAudioVolume(0, 1-t);
            yield return null;
        }
        panel.color = new Color(0, 0, 0, 1);
        SceneManager.LoadScene("ResultScene");
    }
}
