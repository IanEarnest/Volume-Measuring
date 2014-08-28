using UnityEngine;
using System.Collections;

public class Help : MonoBehaviour {

	bool showHelp;
	public static bool adjustSize;
	string instructions = "No instructions";
	Rect helpRect = new Rect(Screen.width - 80, 0, 80, 25);
	Rect adjustRect = new Rect(Screen.width - 80, 25, 80, 25);

	void OnGUI(){
		GUI.depth = 0;

		if(showHelp){
			if(GUI.Button(helpRect, "Hide Help")){
				showHelp = !showHelp;
			}

			if(Application.loadedLevelName == "Main Menu"){
				instructions = "Choose singleplayer or multi-player.";
			}
			if(Application.loadedLevelName == "SinglePlayer"){
				instructions = "Choose a level to play.";
			}
			if(Application.loadedLevelName == "Lobby"){
				instructions = "Press \"Host a game\" to start your own game, " +
								"\npress \"Refresh Hsots\" to list any other people hosting a game." +
								"\nWhen connected to a server press level buttons to load a level.";
			}
			if(Application.loadedLevelName == "Level 1-1"){
				instructions = "WASD to move, push the blocks into areas to fill or " +
								"\nempty and push into each other to transfer values." +
								"\n\nWhen the container is filled with 4, push onto Dock.";
			}
			if(Application.loadedLevelName == "Level 2-1" || Application.loadedLevelName == "Level 2-2"){
				instructions = "Click on text to fill, empty and move the volumes " +
								"\nbetween the containers." +
								"\n\nGet 4 liters in the 5 liter jug.";
			}
			if(Application.loadedLevelName == "Level 2-2" || Application.loadedLevelName == "Level 2-2"){
				instructions = "Click on text to fill, empty and move the volumes " +
								"\nbetween the containers."  +
								"\n\nGet 2 liters in two of the jugs.";
			}
			if(Application.loadedLevelName == "Level 2-3" || Application.loadedLevelName == "Level 2-2"){
				instructions = "Click on text to fill, empty and move the volumes " +
								"\nbetween the containers.";
			}
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), instructions);
		}
		else{
			if(GUI.Button(helpRect, "Show Help")){
				showHelp = !showHelp;
			}
		}
	}
}
