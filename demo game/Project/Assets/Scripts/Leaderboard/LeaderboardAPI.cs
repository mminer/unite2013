using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

public class LeaderboardAPI 
{
	public static string url = "http://localhost:9086/leaderboard/";
	public static string hashedUrl = "http://localhost:9086/leaderboard-hash/";
	
	public static void RecordScore (string leaderboardId, string name, int score, Action<bool> callback)
	{
		HUD.RunRoutine(DoRequest(leaderboardId, name, score, callback));
	}
	
	/// <summary>
	/// Sends data to server.
	/// </summary>
	static IEnumerator DoRequest (string leaderboardId, string name, int score, Action<bool> callback)
	{
		var api = GameManager.player.useHashedScore ? hashedUrl + leaderboardId : url + leaderboardId;
		var parameters = new Hashtable() {
			{ "name", name },
			{ "score", score }
		};
		
		if (GameManager.player.useHashedScore) {
			parameters["hash"] = GetHash(score.ToString(), GameManager.instance.secretKey);
		}
		
		var postData = SerializePostData(parameters);

		var headers = new Hashtable() {
			{ "Content-Type", "application/json" }
		};
		
		var www = new WWW(api, postData, headers);

		// Send info to server.
		yield return www;
		Debug.Log("Request: " + api + "\n" + Encoding.Default.GetString(postData));
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
	
	static string GetHash (string score, string secretKey)
	{
		var sha1 = SHA1.Create();
		var hash = sha1.ComputeHash(Encoding.Default.GetBytes(score + secretKey));
		string hexHash = BitConverter.ToString(hash).Replace("-", "").ToLower();
		return hexHash;
	}

}
