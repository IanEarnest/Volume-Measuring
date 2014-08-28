using UnityEngine;
using System; // system is only used on standalone.
using System.Collections;

public class MainMenu : MonoBehaviour {

	//Rect menuRect = new Rect(0, 0, 150, 0);
	Rect menuRect = new Rect(0, 0, 200, 0); // Keeps window big for now.
	Rect optionsRect = new Rect(0, 150, 150, 0);

	bool openOptions;
	public static bool singlePlayer = true;

	void OnGUI(){
		menuRect = GUILayout.Window(0, menuRect, menuFunction, "Main Menu");
		if(openOptions){
			optionsRect = GUILayout.Window(1, optionsRect, optionsFunction, "Options");
		}
	}



	// Main Menu window
	void menuFunction(int id){
		if(GUILayout.Button("SinglePlayer")){
			singlePlayer = true;
			Application.LoadLevel("SinglePlayer");
		}
		if(GUILayout.Button("Multi-Player")){
			singlePlayer = false;
			Application.LoadLevel("Lobby");
		}
		if(GUILayout.Button("Options")){
			openOptions = !openOptions;
		}
	}

	// Options window
	void optionsFunction(int id){

		GUILayout.Label("Nothing yet.");
		if(GUILayout.Button("Close")){
			openOptions = false;
		}
	}
}
