using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
	public float noteSpeed = 400;
	private List<Vector3> vectors;

	Image noteImage;

	void Start()
	{
		noteImage = GetComponent<Image>();
	}

	public void HideNote()
	{
		noteImage.enabled = false;
	}

    void Update()
    {
        transform.localPosition += Vector3.left * noteSpeed * Time.deltaTime;
    }

    public List<Vector3> GetVectors() {
    	return vectors;
    }

    public void SetVectors(List<Vector3> v) {
    	vectors = v;
    }
}
