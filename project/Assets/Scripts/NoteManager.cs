using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.Video;

public class NoteManager : MonoBehaviour
{
	NoteInfos noteInfos;

	[SerializeField] Transform NoteAppearLocation;
	[SerializeField] GameObject NotePrefab;
    [SerializeField] GameObject MVPlayer;

	int bpm;
    string jsonFile;
    double startOffset;
    public float speed;
    
	double currentTime = 0d;
    bool started = false;
    bool ended = false;
    
    JudgeManager judgeManager;
    JudgeViewer judgeViewer;
    HpBarManager hpBarManager;

    public GameObject bodyView;
    private SongInfo selected_song;

    void Start()
    {
        judgeManager = GetComponent<JudgeManager>();
        judgeViewer = GetComponent<JudgeViewer>();
        hpBarManager = GameObject.Find("HpBar").GetComponent<HpBarManager>();
        selected_song = GameObject.Find("SelectedSong").GetComponent<SongInfo>();
        jsonFile = selected_song.jsonfile_name;
        bpm = selected_song.bpm;
        speed = selected_song.speed;
        switch (speed) {
        case 0.5f:
            startOffset = selected_song.start_offset_slow;
            break;
        case 1.5f:
            startOffset = selected_song.start_offset_fast;
            break;
        default:
            startOffset = selected_song.start_offset;
            break;
        }
        MVPlayer.GetComponent<VideoPlayer>().playbackSpeed = speed;

        hpBarManager.tickTime = 0.1d / speed;
        
        ReadJson();

    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (!started) {
            if (currentTime >= startOffset) {
                started = true;
                currentTime -= startOffset;
            }
            return;
        }

        if (!ended && currentTime >= 60d / bpm * noteInfos.notes[0].time / speed) {
        	GameObject t_note = Instantiate(NotePrefab, NoteAppearLocation.position, Quaternion.identity);
            t_note.GetComponent<NoteObjectProp>().Initiate(noteInfos.notes[0]);
            Sprite sprite = Resources.Load<Sprite>("Notes/" + jsonFile + "/" + noteInfos.notes[0].image);
            t_note.GetComponent<Image>().sprite = sprite;
            noteInfos.notes.RemoveAt(0);
        	t_note.transform.SetParent(this.transform);
        	t_note.tag = "note";
        	judgeManager.boxNoteList.Add(t_note);

            if (noteInfos.notes.Count <= 0) {
                ended = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("note")) {
            List<Vector3> userPos = new List<Vector3>();
            userPos = bodyView.GetComponent<BodySourceView>().GetPosData();

            // int judgeResult = 2;
            int judgeResult = judgeManager.Judge(userPos);
            Destroy(collision.gameObject);
            judgeViewer.DisplayImage(judgeResult);
            if (judgeResult == 0) {
                hpBarManager.ChangeHp(15);
                selected_song.score_cnt[0]++;
            }
            else if (judgeResult == 1) {
                hpBarManager.ChangeHp(10);
                selected_song.score_cnt[1]++;
            }
            else if (judgeResult == 2) {
                hpBarManager.ChangeHp(5);
                selected_song.score_cnt[2]++;
                }
            else if (judgeResult == 3) {
                hpBarManager.ChangeHp(0);
                selected_song.score_cnt[3]++;
            }
            else {
                hpBarManager.ChangeHp(-5);
                selected_song.score_cnt[4]++;
            }
        }
    }

    void ReadJson() {
        TextAsset jsonText = Resources.Load<TextAsset>(jsonFile);
        string jsonString = jsonText.ToString();

        noteInfos = JsonUtility.FromJson<NoteInfos>(jsonString);
        if (noteInfos.notes.Count <= 0) {
            ended = true;
        }
    }
}

