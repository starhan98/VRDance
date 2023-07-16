using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInfo : MonoBehaviour
{
	private float time;
	private string image;
	private List<Vector3> jointList;
	private List<string> answerBones;

	public NoteInfo() {
	}

	public void Initiate(float _time, string _image, List<Vector3> _jointList, List<string> _answerBones) {
		time = _time;
		image = _image;
		jointList = _jointList;
		answerBones = _answerBones;
	}

	public List<Vector3> GetJoints() {
		return jointList;
	}

	public string GetImagePath() {
		return image;
	}

	public float GetTime() {
		return time;
	}

	public List<string> GetCheckBones() {
		return answerBones;
	}
}
