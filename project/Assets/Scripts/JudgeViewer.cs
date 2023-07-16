using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeViewer : MonoBehaviour
{
	[SerializeField]  Image imagePrefab;
    [SerializeField]  Sprite[] perfSprite = new Sprite[5];
    [SerializeField] Transform JudgeAppearLocation;
    private System.Random random;

    public void DisplayImage(int judgeResult)
    {
    	Image image = Instantiate(imagePrefab, JudgeAppearLocation.position, Quaternion.identity);
        image.transform.SetParent(this.transform);
        image.sprite = perfSprite[judgeResult];

        StartCoroutine(RemoveImage(image));
    }

    private IEnumerator RemoveImage(Image image)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(image.gameObject);
    }
}
