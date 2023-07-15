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
    [SerializeField] GameObject HpBar;

	int bpm = 120;
	double currentTime = 0d;
	double startOffset = 0d;
    
    JudgeManager judgeManager;
    JudgeViewer judgeViewer;
    HpBarManager hpBarManager;

    void Start()
    {
        judgeManager = GetComponent<JudgeManager>();
        judgeViewer = GetComponent<JudgeViewer>();
        hpBarManager = HpBar.GetComponent<HpBarManager>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

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
            judgeManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            judgeViewer.DisplayImage();
            hpBarManager.ChangeHp(10);
        }
    }
}

