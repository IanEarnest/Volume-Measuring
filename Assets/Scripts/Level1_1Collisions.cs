using UnityEngine;
using System.Collections;

public class Level1_1Collisions : MonoBehaviour {
	
	Level1_1Puzzle puzzle;
	
	public static bool playerHit3L;
	public static bool playerHit5L;
	
	// Use this for initialization
	void Start () {
		puzzle = GameObject.Find("Main Camera").GetComponent("Level1_1Puzzle") as Level1_1Puzzle;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision col){
		// Update all numbers for containers and dock.
		
		// Check for collision on containers.
		if(col.collider.name.Contains("Container")){
			//print(this.name + " - " + col.collider.name);
			
			if(this.name == "Fill"){
				if(col.collider.name == "Container 3L")
					puzzle.setL3Value(puzzle.getL3Max());
				else
					puzzle.setL5Value(puzzle.getL5Max());
			}
			
			if(this.name == "Empty"){
				if(col.collider.name == "Container 3L")
					puzzle.setL3Value(0);
				else
					puzzle.setL5Value(0);
			}
		
			if(this.name == "Dock"){
				if(col.collider.name == "Container 3L")
					puzzle.setDockValue(puzzle.getL3Value());
				if(col.collider.name == "Container 5L")
					puzzle.setDockValue(puzzle.getL5Value());
			}
		}
		
		
		// If player hits 3L container and 3L container hits 5L container
		// Transfer liters
		if(col.collider.name == "Player(Clone)" && this.name == "Container 3L"){
			//print("Player hit 3L");
			playerHit3L = true;
			playerHit5L = false;
		}
		if(col.collider.name == "Player(Clone)" && this.name == "Container 5L"){
			//print("Player hit 5L");
			playerHit3L = false;
			playerHit5L = true;
		}
		if(playerHit3L == true && col.collider.name == "Container 3L" && this.name == "Container 5L"){
			int L3V = puzzle.getL3Value();
			int L5V = puzzle.getL5Value();
			
			while(L5V < puzzle.getL5Max() && L3V > 0){
				L3V--;
				L5V++;
				
				puzzle.setL3Value(L3V);
				puzzle.setL5Value(L5V);
			}
			playerHit3L = false;
		}
		if(playerHit5L == true && col.collider.name == "Container 3L" && this.name == "Container 5L"){
			int L3V = puzzle.getL3Value();
			int L5V = puzzle.getL5Value();
			
			while(L3V < puzzle.getL3Max() && L5V > 0){
				L3V++;
				L5V--;
				
				puzzle.setL3Value(L3V);
				puzzle.setL5Value(L5V);
			}
			playerHit5L = false;
		}
		//print(this.name + ", " + col.collider.name);
	}
}
