using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SongInfo : MonoBehaviour
{
    public string name;
    public string difficulty; // EASY, NORMAL, HARD
    public string feature;
    public VideoClip mv;
    public VideoClip shorts;
    private bool on_move = false;
    public int mode; // 0: practice, 1: ranked
    public float speed; // 0.5x, 1.0x, 1.5x
    public Texture stop_pos;
    public Texture thumbnail;

    public bool shiftRight() {
        if (on_move) {
            return false;;
        }
        Vector3 direction = new Vector3(-200, 0, 0);
        StartCoroutine(moveToPosition(direction));
        return true;
    }

    public bool shiftLeft() {
        if (on_move) {
            return false;
        }
        Vector3 direction = new Vector3(200, 0, 0);
        StartCoroutine(moveToPosition(direction));
        return true;
    }

    private System.Collections.IEnumerator moveToPosition(Vector3 direction) {
        on_move = true;

        float t = 0f;
        float duration = 0.5f;

        Vector3 cur_pos = this.gameObject.transform.localPosition;
        Vector3 target_pos = cur_pos + direction;

        while (t < 1f) {
            t += Time.deltaTime / duration;
            this.gameObject.transform.localPosition = Vector3.Lerp(cur_pos, target_pos, t);
            yield return null;
        }

        this.gameObject.transform.localPosition = target_pos;
        on_move = false;
    }

}
