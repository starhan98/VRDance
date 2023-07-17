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

    void Start()
    {
    	judgeManager = GetComponent<JudgeManager>();
    	judgeResults = new List<string>();
    	judgeResults.Add("PERFECT");
    	judgeResults.Add("GREAT");
    	judgeResults.Add("GOOD");
    	judgeResults.Add("BAD");
    	judgeResults.Add("MISS");

        answer = new List<Vector3>();
        // 여기에 테스트하고싶은 자세 관절을 순서대로 입력
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
        answer.Add(new Vector3(0,0,0));
    }

    void Update()
    {
    	//TODO: 여기에 유저의 현재자세 입력
    	// List<Vector3> userPos = ??????;

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
}
