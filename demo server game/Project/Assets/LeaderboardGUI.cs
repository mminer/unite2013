using UnityEngine;
using System.Collections;

public class LeaderboardGUI : MonoBehaviour {
	
	string leaderboardId = "demo";
	string userName = "Matthew";
	string score = "1";
	string result = "";
	float width = 200f;
	float margin = 10f;
	
	static LeaderboardGUI instance;
	LeaderboardGUI () {}
	
	
	void Awake ()
	{
		instance = this;
	}
	
	void OnGUI ()
	{
		// Leaderboard ID
		GUILayout.BeginHorizontal();
			GUILayout.Label("Leaderboard ID", GUILayout.Width(width));
			GUILayout.Space(margin);
			leaderboardId = GUILayout.TextField(leaderboardId, GUILayout.Width(width));
		GUILayout.EndHorizontal();
		
		// Name
		GUILayout.BeginHorizontal();
			GUILayout.Label("Name", GUILayout.Width(width));
			GUILayout.Space(margin);
			userName = GUILayout.TextField(userName, GUILayout.Width(width));
		GUILayout.EndHorizontal();
		
		// Score
		GUILayout.BeginHorizontal();
			GUILayout.Label("Score", GUILayout.Width(width));
			GUILayout.Space(margin);
			score = GUILayout.TextField(score, GUILayout.Width(width));
		GUILayout.EndHorizontal();
		
		// Submit
		if (GUILayout.Button("Submit Score", GUILayout.Width(width))) {
			SubmitScore();
		}
		
		// View Scores
		if (GUILayout.Button("View Scores", GUILayout.Width(width))) {
			ViewScores();
		}
		
		GUILayout.Space(margin);
		
		GUILayout.Label(result);
	}
	
	void ViewScores ()
	{
		if (leaderboardId.Length == 0) {
			return;
		}
		
		Application.OpenURL(LeaderboardAPI.url + leaderboardId);
	}
	
	void SubmitScore ()
	{
		LeaderboardAPI.RecordScore(leaderboardId, userName, System.Convert.ToInt32(score), OnScoreRecorded);
	}
				
	void OnScoreRecorded (bool success)
	{
		if (success) {
			result = "Score recorded!";	
		} else {
			result = "Unable to record score";
		}
	}
	
	/// <summary>
	/// Executes a coroutine.
	/// </summary>
	/// <param name="routine">The coroutine to execute.</param>
	public static Coroutine RunRoutine (IEnumerator routine)
	{
		return instance.StartCoroutine(routine);
	}
}
