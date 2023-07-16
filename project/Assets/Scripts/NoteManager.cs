using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class NoteManager : MonoBehaviour
{
	List<NoteInfo> noteInfos;

	[SerializeField] Transform NoteAppearLocation;
	[SerializeField] GameObject NotePrefab;

	int bpm = 120;
	double currentTime = 0d;
	double startOffset = 3d;
    bool started = false;
    
    JudgeManager judgeManager;
    JudgeViewer judgeViewer;
    HpBarManager hpBarManager;

    public GameObject bodyView;

    void Start()
    {
        judgeManager = GetComponent<JudgeManager>();
        judgeViewer = GetComponent<JudgeViewer>();
        hpBarManager = GameObject.Find("HpBar").GetComponent<HpBarManager>();
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

        if (currentTime >= 60d / bpm) {
        	GameObject t_note = Instantiate(NotePrefab, NoteAppearLocation.position, Quaternion.identity);
        	t_note.transform.SetParent(this.transform);
        	t_note.tag = "note";
        	judgeManager.boxNoteList.Add(t_note);
        	currentTime -= 60d / bpm;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("note")) {
            List<Vector3> userPos = new List<Vector3>();
            // TODO: userPos 입력해주세요
            userPos = bodyView.GetComponent<BodySourceView>().GetPosData();
            Debug.Log(userPos[0]);

            int judgeResult = 0;
            // int judgeResult = judgeManager.Judge(userPos);
            Destroy(collision.gameObject);
            judgeViewer.DisplayImage(judgeResult);
            hpBarManager.ChangeHp(10);
        }
    }
}

