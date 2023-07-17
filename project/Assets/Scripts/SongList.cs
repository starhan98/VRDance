using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public AudioClip fadeout_sound;
    public float speed = 1.0f;
    public int mode = 0;
    public Image panel;

    List<Vector3> leftMotion;
    List<Vector3> rightMotion;
    List<Vector3> okMotion;
    List<Vector3> noMotion;
    List<string> bones;
    JudgeManager judgeManager;

    private int select_mode = 0; // 0: select song, 1: select mode, 2: select speed

    private void Start() { 
        panel.enabled = false;
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
        panel.enabled = false;

        leftMotion = ReadMotion("SelectMotion/left");
        rightMotion = ReadMotion("SelectMotion/right");
        okMotion = ReadMotion("SelectMotion/ok");
        noMotion = ReadMotion("SelectMotion/no");
        bones = GetAllBones();
        judgeManager = GetComponent<JudgeManager>();
    }


    bool move_delay = false;
    private void Update() {
        bool shifting = true;
        if(move_delay) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            Select();
            move_delay = true;
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
                move_delay = true;
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
                move_delay = true;
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
                move_delay = true;
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
                move_delay = true;
            } 
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            Escape();
            move_delay = true;
        }
        if (move_delay) {
            StartCoroutine(delayKey(0.25f));
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

    private IEnumerator delayKey(float t) {
        yield return new WaitForSeconds(t);
        move_delay = false;
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
        else if (select_mode == 2) {
            // goto next scene;
            select_mode++;
            mv_player.Stop();
            StartCoroutine(FadeOut());
            GameObject SelectedSong = Instantiate(songs[cur_song_index]);
            SelectedSong.name = "SelectedSong";
            SelectedSong.GetComponent<SongInfo>().speed = speed;
            SelectedSong.GetComponent<SongInfo>().mode = mode;
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

    
    private IEnumerator FadeOut() {
        panel.enabled = true;

        float t = 0f;
        float duration = 1.5f;
        panel.enabled = true;
        sfx_player.clip = fadeout_sound;
        sfx_player.Play();

        while (t < 1f) {
            t += Time.deltaTime / duration;
            panel.color = new Color(0, 0, 0, t);
            yield return null;
        }
        panel.color = new Color(0, 0, 0, 1);
        SceneManager.LoadScene("GameScene");
    }

    List<Vector3> ReadMotion(string filePath) {
        TextAsset jsonText = Resources.Load<TextAsset>(filePath);
        string jsonString = jsonText.ToString();

        NoteInfo noteInfo = JsonUtility.FromJson<NoteInfo>(jsonString);
        List<Vector3> vectorList = new List<Vector3>();
        foreach (Joint joint in noteInfo.joints) {
            vectorList.Add(joint.GetVector());
        }
        return vectorList;
    }

    List<string> GetAllBones() {
        List<string> boneList = new List<string>();
        boneList.Add("SP");
        boneList.Add("LUA");
        boneList.Add("LLA");
        boneList.Add("RUA");
        boneList.Add("RLA");
        boneList.Add("LUL");
        boneList.Add("LLL");
        boneList.Add("RUL");
        boneList.Add("RLL");
        return boneList;
    }
}
