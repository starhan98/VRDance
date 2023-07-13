using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public struct Sing {
    public RawImage thumbnail;
    public AudioSource music;
}

public class SelectSong : MonoBehaviour
{
    public Sing[] singList = new Sing[3];

    public RawImage cur_thumbnail;
    public RawImage next_thumbnail;
    public RawImage prev_thumbnail;
    public AudioSource cur_music;
    public AudioSource next_music;
    public AudioSource prev_music;

    private void Start()
    {
    }

    private void Update()
    {
        // 왼쪽 화살표 입력 처리
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePrevious();
        }
        // 오른쪽 화살표 입력 처리
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveNext();
        } else if (Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene("MainScene");
        }
    }


    public void MoveNext()
    {
        Texture tmp_img = cur_thumbnail.texture;
        AudioClip tmp_audio = cur_music.clip;

        cur_thumbnail.texture = next_thumbnail.texture;
        next_thumbnail.texture = prev_thumbnail.texture;
        prev_thumbnail.texture = tmp_img;

        cur_music.Stop();

        cur_music.clip = next_music.clip;
        next_music.clip = prev_music.clip;
        prev_music.clip = tmp_audio;

        cur_music.Play();
        
    }

    public void MovePrevious()
    {
        Texture tmp_img = cur_thumbnail.texture;
        AudioClip tmp_audio = cur_music.clip;

        cur_thumbnail.texture = prev_thumbnail.texture;
        prev_thumbnail.texture = next_thumbnail.texture;
        next_thumbnail.texture = tmp_img;
        
        cur_music.Stop();

        cur_music.clip = prev_music.clip;
        prev_music.clip = next_music.clip;
        next_music.clip = tmp_audio;

        cur_music.Play();
    }

}
