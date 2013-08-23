using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public Texture2D healthIcon;
	public GUISkin guiSkin;
	
	
	void OnGUI ()
	{
		GUI.skin = guiSkin;
		
		GUILayout.Space(10f);
		
		// Health Bar and Secure Button
		GUILayout.BeginHorizontal(GUILayout.Width(Screen.width));
			GUILayout.Space(10f);
			
			// Health icons 
			for (var health = 0; health < GameManager.player.health.val; health++) {
				GUILayout.Label(healthIcon, GUILayout.Width(35), GUILayout.Height(35));
				GUILayout.Space(10f);
			}
		
			GUILayout.FlexibleSpace();
		
			if (GUILayout.Button("Secure: " + GameManager.player.useSecureScore)) {
				GameManager.player.useSecureScore = !GameManager.player.useSecureScore;
			}
		
			GUILayout.Space(10f);
		GUILayout.EndHorizontal();
		
		// Score
		GUILayout.BeginHorizontal();
			int score;
		
			if (GameManager.player.useSecureScore) {
				score = GameManager.player.score.val;
			} else {
				score = GameManager.player.insecureScore;
			}
			GUILayout.Label("Score: " + score);
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
}
