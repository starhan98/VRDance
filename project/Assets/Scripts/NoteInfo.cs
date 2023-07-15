using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInfo
{
	private float appearTime;
	private string imagePath;
	private List<Vector3> vectorList;

	public NoteInfo() {
	}

	public List<Vector3> GetVectors() {
		return vectorList;
	}

	public string GetImagePath() {
		return imagePath;
	}

	public float GetAppearTime() {
		return appearTime;
	}
}
