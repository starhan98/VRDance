using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.SceneManagement;

public class SongList : MonoBehaviour
{
    public GameObject[] songs;
    public int cur_song_index = 2;
    public VideoPlayer mv_player; 
    public TextMeshProUGUI song_name;
    public TextMeshProUGUI song_difficulty;
    public TextMeshProUGUI song_feature;
    public TextMeshProUGUI song_mode;
    public TextMeshProUGUI song_speed;
    public AudioSource sfx_player;
    public AudioClip shift_sound;
    public AudioClip select_sound;
    public float speed = 1.0f;
    public int mode = 0;
    


    private int select_mode = 0; // 0: select song, 1: select mode, 2: select speed

    private void Start() { 
        mv_player.loopPointReached += OnVideoClipEnd;
        mv_player.clip = songs[cur_song_index].GetComponent<SongInfo>().mv;
        mv_player.Play();

        SongInfo cur_song = songs[cur_song_index].GetComponent<SongInfo>();
        song_name.text = cur_song.name;
        song_difficulty.text = cur_song.difficulty;
        if (song_difficulty.text == "HARD") {
            song_difficulty.color = Color.red;
        } else if (song_difficulty.text == "NORMAL") {
            song_difficulty.color = Color.yellow;
        } else if (song_difficulty.text == "EASY") {
            song_difficulty.color = Color.green;
        }
        song_feature.text = cur_song.feature;
    }


    private void Update() {
        bool shifting = true;
        if (Input.GetKeyDown(KeyCode.Return)) {
            Select();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (select_mode == 0) {    
                if (cur_song_index > 0) {
                    for(int i = 0; i < songs.Length; i++) {
                        shifting &= songs[i].GetComponent<SongInfo>().shiftLeft();
                    }
                    
                    if (shifting) {
                        cur_song_index--;
                        updateCurrentSong(cur_song_index);
                    }
                    shifting = true;
                }
            } else if (select_mode == 1) {
                if (song_mode.text == "<color=green>Practice</color>    |    Ranked") {
                    song_mode.text = "Practice    |    <color=yellow>Ranked</color>";
                    mode = 1;
                } else {
                    song_mode.text = "<color=green>Practice</color>    |    Ranked";
                    mode = 0;
                }
            } else if (select_mode == 2) {
                if (song_speed.text == "0.5x    |    <color=yellow>1.0x</color>    |    1.5x") {
                    song_speed.text = "<color=green>0.5x</color>    |    1.0x    |    1.5x";
                    speed = 0.5f;
                } else if (song_speed.text == "<color=green>0.5x</color>    |    1.0x    |    1.5x") {
                    song_speed.text = "0.5x    |    1.0x    |    <color=red>1.5x</color>";
                    speed = 1.5f;
                } else {
                    song_speed.text = "0.5x    |    <color=yellow>1.0x</color>    |    1.5x";
                    speed = 1.0f;
                }
            } 
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (select_mode == 0) {
                if (cur_song_index < 4) {
                    for(int i = 0; i < songs.Length; i++) {
                        shifting &= songs[i].GetComponent<SongInfo>().shiftRight();
                    }

                    if (shifting) {
                        cur_song_index++;
                        updateCurrentSong(cur_song_index);
                    }
                }
                shifting = true;
            } else if (select_mode == 1) {
                if (song_mode.text == "<color=green>Practice</color>    |    Ranked") {
                    song_mode.text = "Practice    |    <color=yellow>Ranked</color>";
                    mode = 1;
                } else {
                    song_mode.text = "<color=green>Practice</color>    |    Ranked";
                    mode = 0;
                }
            } else if (select_mode == 2) {
                if (song_speed.text == "0.5x    |    <color=yellow>1.0x</color>    |    1.5x") {
                    song_speed.text = "0.5x    |    1.0x    |    <color=red>1.5x</color>";
                    speed = 1.5f;
                } else if (song_speed.text == "<color=green>0.5x</color>    |    1.0x    |    1.5x") {
                    song_speed.text = "0.5x    |    <color=yellow>1.0x</color>    |    1.5x";
                    speed = 1.0f;
                } else {
                    song_speed.text = "<color=green>0.5x</color>    |    1.0x    |    1.5x";
                    speed = 0.5f;
                }
            } 
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            Escape();
        }
    }

    void updateCurrentSong(int song_index) {
        
        mv_player.Stop();
        sfx_player.clip = shift_sound;
        sfx_player.Play();
        mv_player.clip = songs[song_index].GetComponent<SongInfo>().mv;
        StartCoroutine(delayMVPlay(0.3f));

        SongInfo cur_song = songs[song_index].GetComponent<SongInfo>();
        song_name.text = cur_song.name;
        song_difficulty.text = cur_song.difficulty;
        if (song_difficulty.text == "HARD") {
            song_difficulty.color = Color.red;
        } else if (song_difficulty.text == "NORMAL") {
            song_difficulty.color = Color.yellow;
        } else if (song_difficulty.text == "EASY") {
            song_difficulty.color = Color.green;
        }
        song_feature.text = cur_song.feature;

    }

    private IEnumerator delayMVPlay(float t) {
        yield return new WaitForSeconds(t);
        mv_player.Play();
    }

    private IEnumerator delayMVSound(float t) {
        yield return new WaitForSeconds(t);
        mv_player.SetDirectAudioVolume(0, 1.0f);
    }

    private void OnVideoClipEnd(VideoPlayer vp) {
        vp.Stop();
        vp.Play();
    }

    void Select() {

        sfx_player.clip = select_sound;
        mv_player.SetDirectAudioVolume(0, 0.2f);
        sfx_player.Play();
        StartCoroutine(delayMVSound(0.5f));
        if (select_mode == 0) {
            select_mode++;
            song_mode.text = "<color=green>Practice</color>    |    Ranked";
        } else if (select_mode == 1) {
            select_mode++;
            song_speed.text = "0.5x    |    <color=yellow>1.0x</color>    |    1.5x";
        } 
        else {
            // goto next scene;
            GameObject SelectedSong = Instantiate(songs[cur_song_index]);
            SelectedSong.name = "SelectedSong";
            SelectedSong.GetComponent<SongInfo>().speed = speed;
            SelectedSong.GetComponent<SongInfo>().mode = mode;
            SceneManager.LoadScene("GameScene");
            DontDestroyOnLoad(SelectedSong);
        }
    }

    void Escape() {
        if (select_mode == 1) {
            select_mode--;
            song_mode.text = "Practice    |    Ranked";
        } else if (select_mode == 2) {
            select_mode--;
            song_speed.text = "0.5x    |    1.0x    |    1.5x";
        } else {
            return;
        }
    }

}
