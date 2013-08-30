using UnityEngine;
using System;
using System.Collections;
using System.Security;
using System.Runtime.InteropServices;

public class Score : MonoBehaviour
{
	int score = 9000;
	SecureScore secScore = new SecureScore(8000);

	void OnGUI ()
	{
		GUILayout.Label("Score: " + score);
		if (GUILayout.Button("Increment Score")) {
			score++;
		}

		GUILayout.Label("Secure score display: " + secScore.val);
		GUILayout.Label("Secure score final: " + secScore.finalVal);

		if (GUILayout.Button("Increment Score")) {
			secScore++;
		}
	}
}
