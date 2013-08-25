using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public Texture2D healthIcon;
	public GUISkin guiSkin;
	
	float width = 100f;
	float margin = 10f;
	string result = "";
	string leaderboardId = "demo-leaderboard";
	static HUD instance;
	HUD () {}
	
	
	void Awake ()
	{
		instance = this;
	}	
	
	void OnGUI ()
	{
		var defaultSkin = GUI.skin;
		GUI.skin = guiSkin;
		
		GUILayout.Space(margin);
		
		// Health Bar and Secure Button
		GUILayout.BeginHorizontal(GUILayout.Width(Screen.width));
			GUILayout.Space(margin);
			
			// Health icons 
			for (var health = 0; health < GameManager.player.health.val; health++) {
				GUILayout.Label(healthIcon, GUILayout.Width(35), GUILayout.Height(35));
				GUILayout.Space(margin);
			}
		
			GUILayout.FlexibleSpace();
		
			int score;
		
			if (GameManager.player.useSecureScore) {
				score = GameManager.player.score.val;
			} else {
				score = GameManager.player.insecureScore;
			}
			
			GUILayout.Label("Score: " + score);
		
			GUILayout.FlexibleSpace();
	
			GUILayout.BeginVertical();
				if (GUILayout.Button("Secure: " + GameManager.player.useSecureScore, GUILayout.Width(width))) {
					GameManager.player.useSecureScore = !GameManager.player.useSecureScore;
				}
		
				if (GUILayout.Button("Hashed: " + GameManager.player.useHashedScore, GUILayout.Width(width))) {
					GameManager.player.useHashedScore = !GameManager.player.useHashedScore;
				}
			
				// Submit
				if (GUILayout.Button("Submit Score", GUILayout.Width(width))) {
					SubmitScore();
				}
				
				// View Scores
				if (GUILayout.Button("View Scores", GUILayout.Width(width))) {
					ViewScores();
				}
				
				GUILayout.Space(margin);
				
				GUI.skin = defaultSkin;
				GUILayout.Label(result);
				GUI.skin = guiSkin;
		
			GUILayout.EndVertical();
		
			GUILayout.Space(margin);
			GUILayout.EndHorizontal();
		
		// Try again button
		if (GameManager.player.health.val <= 0) {
			GUILayout.BeginVertical(GUILayout.Height(Screen.height / 1.5f));
				GUILayout.FlexibleSpace();
			
				GUILayout.BeginHorizontal(GUILayout.Width(Screen.width));
					GUILayout.FlexibleSpace();
			
					var original = GUI.skin.label.normal.textColor;
					GUI.skin.label.normal.textColor = Color.red;
					GUILayout.Label("Dead!");
					GUI.skin.label.normal.textColor = original;
			
					GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal(GUILayout.Width(Screen.width));
					GUILayout.FlexibleSpace();
			
					if (GUILayout.Button("Try Again", GUILayout.Width(150), GUILayout.Height(35))) {
						Application.LoadLevel(0);
					}
			
					GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				
				GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
		}
	}
	
	void ViewScores ()
	{		
		Application.OpenURL(LeaderboardAPI.url + leaderboardId);
	}
	
	void SubmitScore ()
	{
		LeaderboardAPI.RecordScore(leaderboardId, "Brad Keys", System.Convert.ToInt32(GameManager.player.score.val), OnScoreRecorded);
	}
				
	void OnScoreRecorded (bool success)
	{
		if (success) {
			result = "Recorded";	
		} else {
			result = "Failed";
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
