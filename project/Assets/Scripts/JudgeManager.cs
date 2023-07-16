using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
	public List<GameObject> boxNoteList = new List<GameObject>();

	int perfectAngle = 10;
	int greatAngle = 15;
	int goodAngle = 20;
	int badAngle = 40;

	// 0: Perfect 1: Great 2: good 3: Bad 4: Miss
	public int Judge(List<Vector3> userPos) {
		List<Vector3> answerBones = CalcBones(boxNoteList[0].GetComponent<NoteObjectProp>().joints);
		List<Vector3> userBones = CalcBones(userPos);

		int result = CalcJudge(answerBones, userBones,
			boxNoteList[0].GetComponent<NoteObjectProp>().answerBones);

		boxNoteList.RemoveAt(0);
		return result;
	}

	List<Vector3> CalcBones(List<Vector3> joints) {
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

	int CalcJudge(List<Vector3> answer, List<Vector3> user, List<string> checkBones) {
		int leastScore = 0, tempScore = 0;
		if (checkBones.Contains("SP")) {
			tempScore = checkDifference(answer[0], user[0]);
			leastScore = leastScore > tempScore ? leastScore : tempScore;
		}
		if (checkBones.Contains("LUA")) {
			tempScore = checkDifference(answer[1], user[1]);
			leastScore = leastScore > tempScore ? leastScore : tempScore;
		}
		if (checkBones.Contains("LLA")) {
			tempScore = checkDifference(answer[2], user[2]);
			leastScore = leastScore > tempScore ? leastScore : tempScore;
		}
		if (checkBones.Contains("RUA")) {
			tempScore = checkDifference(answer[3], user[3]);
			leastScore = leastScore > tempScore ? leastScore : tempScore;
		}
		if (checkBones.Contains("RLA")) {
			tempScore = checkDifference(answer[4], user[4]);
			leastScore = leastScore > tempScore ? leastScore : tempScore;
		}
		if (checkBones.Contains("LUL")) {
			tempScore = checkDifference(answer[5], user[5]);
			leastScore = leastScore > tempScore ? leastScore : tempScore;
		}
		if (checkBones.Contains("LLL")) {
			tempScore = checkDifference(answer[6], user[6]);
			leastScore = leastScore > tempScore ? leastScore : tempScore;
		}
		if (checkBones.Contains("RUL")) {
			tempScore = checkDifference(answer[7], user[7]);
			leastScore = leastScore > tempScore ? leastScore : tempScore;
		}
		if (checkBones.Contains("RLL")) {
			tempScore = checkDifference(answer[8], user[8]);
			leastScore = leastScore > tempScore ? leastScore : tempScore;
		}
		return leastScore;
	}

	int checkDifference(Vector3 v1, Vector3 v2) {
		if (Vector3.Angle(v1, v2) < perfectAngle) return 0;
		if (Vector3.Angle(v1, v2) < greatAngle) return 1;
		if (Vector3.Angle(v1, v2) < goodAngle) return 2;
		if (Vector3.Angle(v1, v2) < badAngle) return 3;
		return 4;
	}
}
