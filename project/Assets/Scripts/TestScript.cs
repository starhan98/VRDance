using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestScript : MonoBehaviour
{
	List<Vector3> answer;
	JudgeManager judgeManager;

	List<string> judgeResults;
	[SerializeField] TMP_Text text;

    BodySourceView bodyView;

    void Start()
    {
    	judgeManager = GetComponent<JudgeManager>();
    	judgeResults = new List<string>();
    	judgeResults.Add("PERFECT");
    	judgeResults.Add("GREAT");
    	judgeResults.Add("GOOD");
    	judgeResults.Add("BAD");
    	judgeResults.Add("MISS");

        bodyView = GameObject.Find("BodyView").GetComponent<BodySourceView>();

        answer = new List<Vector3>();
        // 여기에 테스트하고싶은 자세 관절을 순서대로 입력
        answer.Add(new Vector3(-7.08f,5.22f,18.44f));
        answer.Add(new Vector3(-3.87f,6.299f,18.7f));
        answer.Add(new Vector3(-1.59f,7.02f,18.56f));
        answer.Add(new Vector3(7.15f,4.43f,17.76f));
        answer.Add(new Vector3(3.9233148097991945f,6.00341796875f,18.405990600585939f));
        answer.Add(new Vector3(1.76f,6.89f,18.21f));
        answer.Add(new Vector3(-1.89f,-5.51f,17.57f));
        answer.Add(new Vector3(-1.24f,-1.16f,18.04f));
        answer.Add(new Vector3(-0.60f,1.83f,18.10f));
        answer.Add(new Vector3(1.40f,-4.91f,17.82f));
        answer.Add(new Vector3(1.40f,-0.72f,18.03f));
        answer.Add(new Vector3(0.88f,1.84f,18.00f));
        answer.Add(new Vector3(0.14f,1.87f,18.46f));
        answer.Add(new Vector3(0.17f,5.05f,18.46f));
        answer.Add(new Vector3(0.20f,7.34f,18.43f));
        answer.Add(new Vector3(-0.24f,9.17f,18.31f));

        
    }

    void Update()
    {
    	//TODO: 여기에 유저의 현재자세 입력
    	List<Vector3> userPos = GetUserPos();

        string result = "";

        List<Vector3> answerBones = judgeManager.CalcBones(answer);
        List<Vector3> userBones = judgeManager.CalcBones(userPos);

        for (int i = 0; i < answerBones.Count; i++) {
        	result += judgeResults[judgeManager.checkDifference(answerBones[i], userBones[i])];
        	if (i < answerBones.Count - 1)
        		result += "\n";
        }

        text.text = result;
    }

    List<Vector3> GetUserPos() {
        List<Vector3> res = new List<Vector3>();
        //for (int i = 0; i < 16; i++) {
        //    res.Add(new Vector3(i, i+1, i+2));
        //}
        res = bodyView.GetPosData();
        return res;
    }
}
