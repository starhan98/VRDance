using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarManager : MonoBehaviour
{
    [SerializeField] Image StartTip;
    [SerializeField] Image MainBar;
    [SerializeField] Image EndTip;

    public double tickTime = 0.05d;
    double currentTime = 0d;
    int maxBarLength = 800;
    int hp = 100;
    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        ChangeHp(0);
    }

    public void StartGame() {
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) return;
        
        currentTime += Time.deltaTime;

        while (currentTime >= tickTime) {
            ChangeHp(-1);
            currentTime -= tickTime;
        }
    }

    public void ChangeHp(int offset) {
        hp += offset;

        if (hp > 100) hp = 100;
        if (hp < 0) hp = 0;

        float pixelOffset = hp * maxBarLength / 100f;

        MainBar.rectTransform.sizeDelta
            = new Vector2(pixelOffset,
                MainBar.rectTransform.sizeDelta.y);

        MainBar.rectTransform.anchoredPosition
            = new Vector2(StartTip.rectTransform.anchoredPosition.x + pixelOffset / 2,
                StartTip.rectTransform.anchoredPosition.y);

        EndTip.rectTransform.anchoredPosition
            = new Vector2(StartTip.rectTransform.anchoredPosition.x + pixelOffset,
                StartTip.rectTransform.anchoredPosition.y);
    }
}
