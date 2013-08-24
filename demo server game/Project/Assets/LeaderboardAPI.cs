using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class LeaderboardAPI 
{
	
	public static void RecordScore (string leaderboardId, string name, int score, Action<bool> callback)
	{
		LeaderboardGUI.RunRoutine(DoRequest(leaderboardId, name, score, callback));
	}
	
	/// <summary>
	/// Sends data to server.
	/// </summary>
	static IEnumerator DoRequest (string leaderboardId, string name, int score, Action<bool> callback)
	{
		var url = "http://localhost:9086/leaderboard/" + leaderboardId;
		var parameters = new Hashtable() {
			{ "name", name },
			{ "score", score }
		};
		
		var postData = SerializePostData(parameters);

		var headers = new Hashtable() {
			{ "Content-Type", "application/json" }
		};
		
		var www = new WWW(url, postData, headers);

		// Send info to server.
		yield return www;
		Debug.Log("Request: " + url + "\n" + Encoding.Default.GetString(postData));
		Debug.Log("Response: " + www.text);

		if (www.error == null) {
			callback(true);
		} else {
			callback(false);
		}
	}
	
	/// <summary>
	/// Converts parameters into a JSON string to put in POST request's body.
	/// </summary>
	/// <param name="parameters">Information to send in the request.</param>
	/// <returns>A byte array suitable for sending over the wire.</returns>
	public static byte[] SerializePostData (object parameters)
	{
		string json;

		if (parameters == null) {
			json = "{}";
		} else {
			json = Json.Serialize(parameters);
		}

		var postData = Encoding.ASCII.GetBytes(json);
		return postData;
	}

}
