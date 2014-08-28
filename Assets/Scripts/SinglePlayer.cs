using UnityEngine;
using System.Collections;

public class SinglePlayer : MonoBehaviour {

	Rect menuRect = new Rect(0, 0, 200, 0);

	void OnGUI(){
		menuRect = GUILayout.Window(0, menuRect, menuFunction, "Menu");
	}

	// Menu window
	void menuFunction(int id){
		if(GUILayout.Button("Back to Main Menu")){
			Application.LoadLevel("Main Menu");
		}
		if(GUILayout.Button("Level 1-1")){
			Application.LoadLevel("Level 1-1");
		}
		if(GUILayout.Button("Level 2-1")){
			Application.LoadLevel("Level 2-1");
		}
		if(GUILayout.Button("Level 2-2")){
			Application.LoadLevel("Level 2-2");
		}
		if(GUILayout.Button("Level 2-3")){
			Application.LoadLevel("Level 2-3");
		}
	}
}
