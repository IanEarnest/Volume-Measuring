using UnityEngine;
using System.Collections;

public class Level2_1Puzzle : MonoBehaviour {
	
	// 3 Liter jug
	int liter3Int;
	int liter3Max = 3;
	GameObject liter3Jug1;
	GameObject liter3Jug2;
	GameObject liter3Jug3;
	TextMesh liter3Value;
	TextMesh liter3Fill;
	TextMesh liter3Empty;
	TextMesh liter3Transfer;
	
	// 5 Liter jug
	int liter5Int;
	int liter5Max = 5;
	GameObject liter5Jug1;
	GameObject liter5Jug2;
	GameObject liter5Jug3;
	GameObject liter5Jug4;
	GameObject liter5Jug5;
	TextMesh liter5Value;
	TextMesh liter5Fill;
	TextMesh liter5Empty;
	TextMesh liter5Transfer;
	
	int winNum = 4;
	
	// Ending text
	TextMesh next;
	TextMesh finish;
	
	// Menu
	int movesCount;
	TextMesh moves;
	TextMesh undo;
	
	// Raycast
	public Ray ray;
	public RaycastHit hit;

	public GameObject pointer;
	
	/// <summary>
	/// Finds TextMeshes.
	/// </summary>
	/// <returns>
	/// TextMesh.
	/// </returns>
	/// <param name='textMesh'>
	/// The TextMesh variable you wish to link to the TextMesh.
	/// </param>
	/// <param name='name'>
	/// Name of the gameObject in scene.
	/// </param>
	TextMesh findMesh(TextMesh textMesh, string name){
		textMesh = GameObject.Find(name).GetComponent("TextMesh") as TextMesh;
		return textMesh;
	}


	void Start () {
		// Find all the text meshes
		liter3Jug1 = GameObject.Find("3 Liter Jug Water 1");
		liter3Jug2 = GameObject.Find("3 Liter Jug Water 2");
		liter3Jug3 = GameObject.Find("3 Liter Jug Water 3");
		liter3Value = findMesh(liter3Value,"3 Liter Value");
		liter3Fill = findMesh(liter3Fill,"3 Liter Fill");
		liter3Empty = findMesh(liter3Empty,"3 Liter Empty");
		liter3Transfer = findMesh(liter3Transfer,"3 Liter Transfer");
		
		liter5Jug1 = GameObject.Find("5 Liter Jug Water 1");
		liter5Jug2 = GameObject.Find("5 Liter Jug Water 2");
		liter5Jug3 = GameObject.Find("5 Liter Jug Water 3");
		liter5Jug4 = GameObject.Find("5 Liter Jug Water 4");
		liter5Jug5 = GameObject.Find("5 Liter Jug Water 5");
		liter5Value = findMesh(liter5Value,"5 Liter Value");
		liter5Fill = findMesh(liter5Fill,"5 Liter Fill");
		liter5Empty = findMesh(liter5Empty,"5 Liter Empty");
		liter5Transfer = findMesh(liter5Transfer,"5 Liter Transfer");
		
		moves = findMesh(moves,"Moves Count");
		next = findMesh (next,"Next");
		next.renderer.enabled = false;
		finish = findMesh(finish,"Finish");
		finish.text = "";
		undo = findMesh (undo,"Undo");

		// Check connection before spawning, fixes error.
		if(Network.peerType != NetworkPeerType.Disconnected){
			pointer = (GameObject)Network.Instantiate(pointer, Vector3.zero, Quaternion.identity, 0);
			// set pointer name
			//if(Network.isServer){
			pointer.GetComponentInChildren<TextMesh>().text = NetworkManager.playerName;
		}
	}

	void Update () {
		liter3Value.text = liter3Int.ToString() + "/" + liter3Max;
		liter5Value.text = liter5Int.ToString() + "/" + liter5Max;
		
		moves.text = movesCount.ToString();

		// Check pointer exists, fixes error. Set pointer at mouse position.
		if(pointer){
			pointer.transform.rigidbody.MovePosition(hit.point);
		}

		// Game Over conditions
		if(liter5Int == winNum){
			// Display win text
			finish.text = "Finished in " + movesCount + " Moves.";
			next.renderer.enabled = true;
			
			// Hide colliders
			if(liter3Fill){
				/*Destroy(liter3Fill.collider);
				Destroy(liter3Empty.collider);
				Destroy(liter3Transfer.collider);
				Destroy(liter5Fill.collider);
				Destroy(liter5Empty.collider);
				Destroy(liter5Transfer.collider);*/
				liter3Fill.color = Color.black;
				liter3Fill.fontStyle = FontStyle.Normal;
				liter3Empty.color = Color.black;
				liter3Empty.fontStyle = FontStyle.Normal;
				liter3Transfer.color = Color.black;
				liter3Transfer.fontStyle = FontStyle.Normal;
				liter5Fill.color = Color.black;
				liter5Fill.fontStyle = FontStyle.Normal;
				liter5Empty.color = Color.black;
				liter5Empty.fontStyle = FontStyle.Normal;
				liter5Transfer.color = Color.black;
				liter5Transfer.fontStyle = FontStyle.Normal;
			}
			// Hide controlls
			/*Destroy(liter3Fill);
			Destroy(liter3Empty);
			Destroy(liter3Transfer);
			Destroy(liter5Fill);
			Destroy(liter5Empty);
			Destroy(liter5Transfer);*/
			
		}
		
		// Graphics for jugs
		liter3Jug1.renderer.enabled = false;
		liter3Jug2.renderer.enabled = false;
		liter3Jug3.renderer.enabled = false;
		liter5Jug1.renderer.enabled = false;
		liter5Jug2.renderer.enabled = false;
		liter5Jug3.renderer.enabled = false;
		liter5Jug4.renderer.enabled = false;
		liter5Jug5.renderer.enabled = false;
		if(liter3Int == 1){
			liter3Jug1.renderer.enabled = true;
		}
		if(liter3Int == 2){
			liter3Jug2.renderer.enabled = true;
		}
		if(liter3Int == 3){
			liter3Jug3.renderer.enabled = true;
		}
		if(liter5Int == 1){
			liter5Jug1.renderer.enabled = true;
		}
		if(liter5Int == 2){
			liter5Jug2.renderer.enabled = true;
		}
		if(liter5Int == 3){
			liter5Jug3.renderer.enabled = true;
		}
		if(liter5Int == 4){
			liter5Jug4.renderer.enabled = true;
		}		
		if(liter5Int == 5){
			liter5Jug5.renderer.enabled = true;
		}
		
		if(liter3Fill){
			if(liter3Int == liter3Max){
				liter3Fill.color = Color.black;
				liter3Fill.fontStyle = FontStyle.Normal;
			}
			else{
				liter3Fill.fontStyle = FontStyle.Normal;
				liter3Fill.color = Color.white;
			}
			if(liter3Int == 0){
				liter3Empty.color = Color.black;
				liter3Empty.fontStyle = FontStyle.Normal;
				liter3Transfer.color = Color.black;
				liter3Transfer.fontStyle = FontStyle.Normal;
			}
			else{
				liter3Empty.fontStyle = FontStyle.Normal;
				liter3Transfer.fontStyle = FontStyle.Normal;
				liter3Empty.color = Color.white;
				liter3Transfer.color = Color.white;
			}
			
			
			if(liter5Int == liter5Max){
				liter5Fill.color = Color.black;
				liter5Fill.fontStyle = FontStyle.Normal;
			}
			else{
				liter5Fill.fontStyle = FontStyle.Normal;
				liter5Fill.color = Color.white;
			}
			if(liter5Int == 0){
				liter5Empty.color = Color.black;
				liter5Empty.fontStyle = FontStyle.Normal;
				liter5Transfer.color = Color.black;
				liter5Transfer.fontStyle = FontStyle.Normal;
			}
			else{
				liter5Empty.fontStyle = FontStyle.Normal;
				liter5Transfer.fontStyle = FontStyle.Normal;
				liter5Empty.color = Color.white;
				liter5Transfer.color = Color.white;
			}
		}
		next.fontStyle = FontStyle.Normal;
		undo.fontStyle = FontStyle.Normal;
		
		// raycast to highlight 3D font when hovered over
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			if(liter3Fill){
				if(hit.collider.name == "3 Liter Fill" && liter3Fill.color == Color.white){
					liter3Fill.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "3 Liter Empty" && liter3Empty.color == Color.white){
					liter3Empty.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "3 Liter Transfer" && liter3Transfer.color == Color.white){
					liter3Transfer.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "5 Liter Fill" && liter5Fill.color == Color.white){
					liter5Fill.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "5 Liter Empty" && liter5Empty.color == Color.white){
					liter5Empty.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "5 Liter Transfer" && liter5Transfer.color == Color.white){
					liter5Transfer.fontStyle = FontStyle.Bold;
				}
			}
			if(hit.collider.name == "Next"){
				next.fontStyle = FontStyle.Bold;
			}
			if(hit.collider.name == "Main Menu"){
				//mainMenu.fontStyle = FontStyle.Bold;
			}
			if(hit.collider.name == "Reset"){
				//reset.fontStyle = FontStyle.Bold;
			}
			if(hit.collider.name == "Undo"){
				undo.fontStyle = FontStyle.Bold;
			}
		}
		
		// Click to use 3D TextMesh as buttons.
		if (Input.GetButtonDown ("Fire1")) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		    if (Physics.Raycast (ray, out hit)) {
				if(hit.collider.name == "3 Liter Fill"){
					liter3Int = liter3Max;
					
					if(liter3Fill.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "3 Liter Empty"){
					liter3Int = 0;
					
					if(liter3Empty.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "3 Liter Transfer"){
					for(; liter3Int>0 && liter5Int<liter5Max; liter3Int--){
						liter5Int++;
					}
					
					if(liter3Transfer.color == Color.white)
						movesCount++;
				}
				
				if(hit.collider.name == "5 Liter Fill"){
					liter5Int = liter5Max;
					
					if(liter5Fill.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "5 Liter Empty"){
					liter5Int = 0;
					
					if(liter5Empty.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "5 Liter Transfer"){
					for(;liter5Int>0 && liter3Int<liter3Max; liter5Int--){
						liter3Int++;
					}
					
					if(liter5Transfer.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "Next" && liter5Int == winNum){
					if(MainMenu.singlePlayer == true){
						Application.LoadLevel("Level 2-2");
					}
					if(MainMenu.singlePlayer == false){
						networkView.RPC("LoadLevel", RPCMode.All, "Level 2-2");
					}
				}
				if(hit.collider.name == "Undo"){
					// not set-up yet.
				}

				// After clicking a button, sync the jug and move int values.
				networkView.RPC("SyncValues", RPCMode.All, liter3Int, liter5Int, movesCount);
			}
		}
	}

	// Sync the jug and move int values.
	[@RPC]
	void SyncValues(int liter3, int liter5, int moves){
		liter3Int = liter3;
		liter5Int = liter5;
		movesCount = moves;
	}

	// Load levels across the network.
	[@RPC]
	void LoadLevel(string level){
		Application.LoadLevel(level);
	}
}
