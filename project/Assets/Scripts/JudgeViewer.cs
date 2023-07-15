using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeViewer : MonoBehaviour
{
	[SerializeField]  Image imagePrefab;
    [SerializeField]  Sprite[] perfSprite = new Sprite[4];
    [SerializeField] Transform JudgeAppearLocation;
    private System.Random random;

    public void DisplayImage()
    {
    	random = new System.Random();
        int randomNumber = random.Next(0, 100);
    	Image image = Instantiate(imagePrefab, JudgeAppearLocation.position, Quaternion.identity);
        image.transform.SetParent(this.transform);
        if (randomNumber < 10)
        	image.sprite = perfSprite[0];
        else if (randomNumber < 30)
        	image.sprite = perfSprite[1];
        else if (randomNumber < 50)
        	image.sprite = perfSprite[2];
        else
        	image.sprite = perfSprite[3];

        StartCoroutine(RemoveImage(image));
    }

    private IEnumerator RemoveImage(Image image)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(image.gameObject);
    }
}
