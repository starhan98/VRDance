using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class NoteManager : MonoBehaviour
{
	NoteInfos noteInfos;

	[SerializeField] Transform NoteAppearLocation;
	[SerializeField] GameObject NotePrefab;

	int bpm = 120;
	double currentTime = 0d;
	double startOffset = 3d;
    bool ended = false;
    bool started = false;
    string jsonFile = "idol";
    
    JudgeManager judgeManager;
    JudgeViewer judgeViewer;
    HpBarManager hpBarManager;

    void Start()
    {
        judgeManager = GetComponent<JudgeManager>();
        judgeViewer = GetComponent<JudgeViewer>();
        hpBarManager = GameObject.Find("HpBar").GetComponent<HpBarManager>();
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

        if (!ended && currentTime >= 60d / bpm * noteInfos.notes[0].time) {
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
            // TODO: userPos 입력해주세요
            int judgeResult = 0;
            // int judgeResult = judgeManager.Judge(userPos);
            Destroy(collision.gameObject);
            judgeViewer.DisplayImage(judgeResult);
            hpBarManager.ChangeHp(10);
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

