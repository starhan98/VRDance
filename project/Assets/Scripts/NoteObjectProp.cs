using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObjectProp : MonoBehaviour
{
	public float time;
	public string image;
	public List<Vector3> joints;
	public List<string> answerBones;

	public void Initiate(NoteInfo noteInfo) {
		time = noteInfo.time;
		image = noteInfo.image;
		joints = new List<Vector3>();
		foreach (Joint joint in noteInfo.joints)
			joints.Add(joint.GetVector());
		answerBones = noteInfo.answerBones;
	}
}
