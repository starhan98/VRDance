using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
	public List<GameObject> boxNoteList = new List<GameObject>();

	int perfectAngle = 10;
	int greatAngle = 15;
	int goodAngle = 20;
	int badAngle = 30;

	// 0: Perfect 1: Great 2: good 3: Bad 4: Miss
	public int Judge(List<Vector3> userPos) {
		List<Vector3> answerBones = CalcBones(boxNoteList[0].GetComponent<NoteObjectProp>().joints);
		List<Vector3> userBones = CalcBones(userPos);

		int result = CalcJudge(answerBones, userBones,
			boxNoteList[0].GetComponent<NoteObjectProp>().answerBones);

		boxNoteList.RemoveAt(0);
		return result;
	}

	public List<Vector3> CalcBones(List<Vector3> joints) {
		List<Vector3> bones = new List<Vector3>();
		// SP
		bones.Add(joints[12] - joints[14]);
		// LUA
		bones.Add(joints[1] - joints[14]);
		// LLA
		bones.Add(joints[0] - joints[1]);
		// RUA
		bones.Add(joints[4] - joints[14]);
		// RLA
		bones.Add(joints[3] - joints[4]);
		// LUL
		bones.Add(joints[7] - joints[12]);
		// LLL
		bones.Add(joints[6] - joints[7]);
		// RUL
		bones.Add(joints[10] - joints[12]);
		// RLL
		bones.Add(joints[9] - joints[10]);
		return bones;
	}

	public int CalcJudge(List<Vector3> answer, List<Vector3> user, List<string> checkBones) {
		int totalMiss = 0;
		if (checkBones.Contains("SP"))
			totalMiss += MissScore(checkDifference(answer[0], user[0]));
		if (checkBones.Contains("LUA"))
			totalMiss += MissScore(checkDifference(answer[1], user[1]));
		if (checkBones.Contains("LLA"))
			totalMiss += MissScore(checkDifference(answer[2], user[2]));
		if (checkBones.Contains("RUA"))
			totalMiss += MissScore(checkDifference(answer[3], user[3]));
		if (checkBones.Contains("RLA"))
			totalMiss += MissScore(checkDifference(answer[3], user[3]));
		if (checkBones.Contains("LUL"))
			totalMiss += MissScore(checkDifference(answer[4], user[4]));
		if (checkBones.Contains("LLL"))
			totalMiss += MissScore(checkDifference(answer[5], user[5]));
		if (checkBones.Contains("RUL"))
			totalMiss += MissScore(checkDifference(answer[6], user[6]));
		if (checkBones.Contains("RLL")) {
			totalMiss += MissScore(checkDifference(answer[7], user[7]));
		}

		if (totalMiss <= 8) return 0;
		if (totalMiss <= 16) return 1;
		if (totalMiss <= 24) return 2;
		if (totalMiss <= 34) return 3;
		return 4;
	}

	public int checkDifference(Vector3 v1, Vector3 v2) {
		if (Vector3.Angle(v1, v2) < perfectAngle) return 0;
		if (Vector3.Angle(v1, v2) < greatAngle) return 1;
		if (Vector3.Angle(v1, v2) < goodAngle) return 2;
		if (Vector3.Angle(v1, v2) < badAngle) return 3;
		return 4;
	}

	// perfect: 0, great: 1, good: 2, bad: 5, miss: 15
	int MissScore(int judgement) {
		switch (judgement) {
			case 0:
				return 0;
			case 1:
				return 1;
			case 2:
				return 2;
			case 3:
				return 5;
		}
		return 15;
	}
}
