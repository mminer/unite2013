using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour 
{

	void Start () 
	{

	}
	
	void OnGUI ()
	{
		if (GUILayout.Button("Record Event")) {
			//LumosAnalytics.RecordEvent("MyFirstEvent");
		}
		
		if (GUILayout.Button("Record Logs")) {
			Debug.LogWarning("A warning!");
			Debug.LogError("Oh no! An Error!");
		}
	}
}
