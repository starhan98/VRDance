using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapMaker : MonoBehaviour
{
	int order;
	NoteInfos result;
	string fileName;

    public GameObject bodyView;

    // Start is called before the first frame update
    void Start()
    {
        order = 1;
        result = new NoteInfos();
        result.notes = new List<NoteInfo>();
        fileName = "Assets/Resources/test.json";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
        	addNote();
        	Debug.Log("mouse input");
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
        	makeJson();
        	Debug.Log("enter input");
        }
    }

    void addNote() {
    	List<Vector3> userPos = GetUserPos();

    	NoteInfo info = new NoteInfo();
    	info.time = 0;
    	info.image = order.ToString("D4");
    	info.joints = toJoints(userPos);
    	info.answerBones = new List<string>();

    	result.notes.Add(info);
    	order++;
    }

    List<Vector3> GetUserPos() {
    	List<Vector3> res = new List<Vector3>();
        //for (int i = 0; i < 16; i++) {
        //TODO
        //	res.Add(new Vector3(i, i+1, i+2));
        //}
        res = bodyView.GetComponent<BodySourceView>().GetPosData();
    	return res;
    }

    List<Joint> toJoints(List<Vector3> vlist) {
    	List<Joint> res = new List<Joint>();
    	foreach (Vector3 v in vlist) {
    		res.Add(new Joint(v.x, v.y, v.z));
    	}
    	return res;
    }

    void makeJson() {
    	string jsonData = JsonUtility.ToJson(result);
    	File.WriteAllText(fileName, jsonData);
    	#if UNITY_EDITOR
    	UnityEditor.AssetDatabase.Refresh();
    	#endif
    }
}
