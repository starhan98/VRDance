using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SongList : MonoBehaviour
{
    public GameObject[] songs;
    public int cur_song_index = 3;
    public VideoPlayer mv_player; 

    private void Start() {
        updateCurrentSong(cur_song_index);
    }


    private void Update() {
        bool shifting = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            for(int i = 0; i < songs.Length; i++) {
                shifting &= songs[i].GetComponent<SongInfo>().shiftLeft();
            }
            if (shifting) {
                if (cur_song_index == 0) {
                    cur_song_index = 6;
                } else {
                    cur_song_index--;
                }
                updateCurrentSong(cur_song_index);
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            for(int i = 0; i < songs.Length; i++) {
                shifting &= songs[i].GetComponent<SongInfo>().shiftRight();
            }
            if (shifting) {
                if (cur_song_index == 6) {
                    cur_song_index = 0;
                } else {
                    cur_song_index++;
                }
                updateCurrentSong(cur_song_index);
            }
        }
        shifting = true;
    }

    void updateCurrentSong(int song_index) {
        mv_player.Stop();
        mv_player.clip = songs[song_index].GetComponent<SongInfo>().mv;
        mv_player.Play();
    }

}
