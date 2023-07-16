using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public VideoPlayer mv_player;
    public SongInfo selected_song;
    public float song_speed;
    public int song_mode;
    void Start() {
        selected_song = GameObject.Find("SelectedSong").GetComponent<SongInfo>();
        mv_player.clip = selected_song.shorts;
        song_speed = selected_song.speed;
        song_mode = selected_song.mode;
        mv_player.Play();
    }


}
