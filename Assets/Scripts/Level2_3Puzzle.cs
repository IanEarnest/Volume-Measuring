using UnityEngine;
using System.Collections;

public class Level2_3Puzzle : MonoBehaviour {
	
	// Jug 1
	int Jug1Int;
	int Jug1Max;
	TextMesh Jug1Value;
	TextMesh Jug1Fill;
	TextMesh Jug1Empty;
	TextMesh Jug1Transfer;
	TextMesh Jug1Transfer2;
	
	// Jug 2
	int Jug2Int;
	int Jug2Max;
	TextMesh Jug2Value;
	TextMesh Jug2Fill;
	TextMesh Jug2Empty;
	TextMesh Jug2Transfer;
	TextMesh Jug2Transfer2;

	// Jug 3
	int Jug3Int;
	int Jug3Max;
	TextMesh Jug3Value;
	TextMesh Jug3Fill;
	TextMesh Jug3Empty;
	TextMesh Jug3Transfer;
	TextMesh Jug3Transfer2;
	
	int winNum = 1;
	int puzzleNum;
	TextMesh taskText;
	
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
		Jug1Value = findMesh(Jug1Value,"Jug 1 Value");
		Jug1Fill = findMesh(Jug1Fill,"Jug 1 Fill");
		Jug1Empty = findMesh(Jug1Empty,"Jug 1 Empty");
		Jug1Transfer = findMesh(Jug1Transfer,"Jug 1 Transfer");
		Jug1Transfer2 = findMesh(Jug1Transfer2,"Jug 1 Transfer 2");

		Jug2Value = findMesh(Jug2Value,"Jug 2 Value");
		Jug2Fill = findMesh(Jug2Fill,"Jug 2 Fill");
		Jug2Empty = findMesh(Jug2Empty,"Jug 2 Empty");
		Jug2Transfer = findMesh(Jug2Transfer,"Jug 2 Transfer");
		Jug2Transfer2 = findMesh(Jug2Transfer2,"Jug 2 Transfer 2");

		Jug3Value = findMesh(Jug3Value,"Jug 3 Value");
		Jug3Fill = findMesh(Jug3Fill,"Jug 3 Fill");
		Jug3Empty = findMesh(Jug3Empty,"Jug 3 Empty");
		Jug3Transfer = findMesh(Jug3Transfer,"Jug 3 Transfer");
		Jug3Transfer2 = findMesh(Jug3Transfer2,"Jug 3 Transfer 2");
		
		moves = findMesh(moves,"Moves Count");
		next = findMesh (next,"Next");
		next.renderer.enabled = false;
		finish = findMesh(finish,"Finish");
		finish.text = "";
		taskText = findMesh(taskText,"Task Text");
		undo = findMesh (undo,"Undo");

		// Check connection before spawning, fixes error.
		if(Network.peerType != NetworkPeerType.Disconnected){
			pointer = (GameObject)Network.Instantiate(pointer, Vector3.zero, Quaternion.identity, 0);
			// set pointer name
			//if(Network.isServer){
			pointer.GetComponentInChildren<TextMesh>().text = NetworkManager.playerName;
		}
		if(Network.isServer || MainMenu.singlePlayer == true){
			/*(Puzzle number) formular
			 * (1) a-3b
			 * (2-6) b-a-2c
			 * (7,9,11) a-c
			 * (8,10) a+c
			 * 
			 * Work out a, b and c
			 * answer = b - a - (c * 2)
			 * 
			 * answer = random number
			 * 
			 * Working out a, b and c
			 * a = b - answer - (c * 2)
			 * b = answer + a + (c * 2)
			 * c = b - answer - a \ 2
			 * 
			 * What i currently have
			 * a = Random.Range (1, winNum / 2)
			 * c = Random.Range (1, winNum / 2)
			 * b = winNum + a + (c * 2)
			 */
			int a = 0;
			int b = 0;
			int c = 0;
			puzzleNum = 0;
			
			winNum = Random.Range (1, 255);
			puzzleNum = Random.Range (1, 11);

			a = Random.Range (1, winNum / 2);
			c = Random.Range (1, winNum / 2);
			b = winNum + a + (c * 2);

			//(2-6) b-a-2c
			if(puzzleNum >= 2 && puzzleNum <= 6){
				b = winNum + a + (c * 2);
			}
			//(7,9,11) a-c
			if(puzzleNum == 7 || puzzleNum == 9 || puzzleNum == 11){
				a = winNum + c;
				b = winNum + a + (c * 2);
			}
			//(8,10) a+c
			if(puzzleNum == 8 || puzzleNum == 10){
				a = winNum - c;
				b = winNum + a + (c * 2);
			}

			Jug1Max = a;
			Jug2Max = b;
			Jug3Max = c;

			networkView.RPC("StartSync", RPCMode.All, winNum, puzzleNum, Jug1Max, Jug2Max, Jug3Max);
		}
	}

	void Update () {
		Jug1Value.text = Jug1Int.ToString() + "/" + Jug1Max;
		Jug2Value.text = Jug2Int.ToString() + "/" + Jug2Max;
		Jug3Value.text = Jug3Int.ToString() + "/" + Jug3Max;
		taskText.text = "Level " + puzzleNum + ": Get " + winNum + " litres";
		
		moves.text = movesCount.ToString();

		// Check pointer exists, fixes error. Set pointer at mouse position.
		if(pointer){
			pointer.transform.rigidbody.MovePosition(hit.point);
		}

		// Game Over conditions
		if(Jug1Int == winNum ||
		   Jug2Int == winNum ||
		   Jug3Int == winNum){
			// Display win text
			finish.text = "Finished in " + movesCount + " Moves.";
			next.renderer.enabled = true;
			
			// Hide colliders
			if(Jug1Fill){
				/*Destroy(liter3Fill.collider);
				Destroy(liter3Empty.collider);
				Destroy(liter3Transfer.collider);
				Destroy(liter5Fill.collider);
				Destroy(liter5Empty.collider);
				Destroy(liter5Transfer.collider);*/
				Jug1Fill.color = Color.black;
				Jug1Fill.fontStyle = FontStyle.Normal;
				Jug1Empty.color = Color.black;
				Jug1Empty.fontStyle = FontStyle.Normal;
				Jug1Transfer.color = Color.black;
				Jug1Transfer.fontStyle = FontStyle.Normal;
				Jug1Transfer2.color = Color.black;
				Jug1Transfer2.fontStyle = FontStyle.Normal;
				Jug2Fill.color = Color.black;
				Jug2Fill.fontStyle = FontStyle.Normal;
				Jug2Empty.color = Color.black;
				Jug2Empty.fontStyle = FontStyle.Normal;
				Jug2Transfer.color = Color.black;
				Jug2Transfer.fontStyle = FontStyle.Normal;
				Jug2Transfer2.color = Color.black;
				Jug2Transfer2.fontStyle = FontStyle.Normal;
				Jug3Fill.color = Color.black;
				Jug3Fill.fontStyle = FontStyle.Normal;
				Jug3Empty.color = Color.black;
				Jug3Empty.fontStyle = FontStyle.Normal;
				Jug3Transfer.color = Color.black;
				Jug3Transfer.fontStyle = FontStyle.Normal;
				Jug3Transfer2.color = Color.black;
				Jug3Transfer2.fontStyle = FontStyle.Normal;
			}
			// Hide controlls
			/*Destroy(liter3Fill);
			Destroy(liter3Empty);
			Destroy(liter3Transfer);
			Destroy(liter5Fill);
			Destroy(liter5Empty);
			Destroy(liter5Transfer);*/
		}
		
		if(Jug1Fill){
			if(Jug1Int == Jug1Max){
				Jug1Fill.color = Color.black;
				Jug1Fill.fontStyle = FontStyle.Normal;
			}
			else{
				Jug1Fill.fontStyle = FontStyle.Normal;
				Jug1Fill.color = Color.white;
			}
			if(Jug1Int == 0){
				Jug1Empty.color = Color.black;
				Jug1Empty.fontStyle = FontStyle.Normal;
				Jug1Transfer.color = Color.black;
				Jug1Transfer.fontStyle = FontStyle.Normal;
				Jug1Transfer2.color = Color.black;
				Jug1Transfer2.fontStyle = FontStyle.Normal;
			}
			else{
				Jug1Empty.fontStyle = FontStyle.Normal;
				//Jug1Transfer.fontStyle = FontStyle.Normal;
				Jug1Empty.color = Color.white;
				//Jug1Transfer.color = Color.white;
			}
			if(Jug1Int != 0 && Jug2Int != Jug2Max){
				Jug1Transfer.fontStyle = FontStyle.Normal;
				Jug1Transfer.color = Color.white;
			}
			if(Jug1Int != 0 && Jug3Int != Jug3Max){
				Jug1Transfer2.fontStyle = FontStyle.Normal;
				Jug1Transfer2.color = Color.white;
			}
			
			
			if(Jug2Int == Jug2Max){
				Jug2Fill.color = Color.black;
				Jug2Fill.fontStyle = FontStyle.Normal;
			}
			else{
				Jug2Fill.fontStyle = FontStyle.Normal;
				Jug2Fill.color = Color.white;
			}
			if(Jug2Int == 0){
				Jug2Empty.color = Color.black;
				Jug2Empty.fontStyle = FontStyle.Normal;
				Jug2Transfer.color = Color.black;
				Jug2Transfer.fontStyle = FontStyle.Normal;
				Jug2Transfer2.color = Color.black;
				Jug2Transfer2.fontStyle = FontStyle.Normal;
			}
			else{
				Jug2Empty.fontStyle = FontStyle.Normal;
				//Jug2Transfer.fontStyle = FontStyle.Normal;
				Jug2Empty.color = Color.white;
				//Jug2Transfer.color = Color.white;
			}
			if(Jug2Int != 0 && Jug1Int != Jug1Max){
				Jug2Transfer.fontStyle = FontStyle.Normal;
				Jug2Transfer.color = Color.white;
			}
			if(Jug2Int != 0 && Jug3Int != Jug3Max){
				Jug2Transfer2.fontStyle = FontStyle.Normal;
				Jug2Transfer2.color = Color.white;
			}


			if(Jug3Int == Jug2Max){
				Jug3Fill.color = Color.black;
				Jug3Fill.fontStyle = FontStyle.Normal;
			}
			else{
				Jug3Fill.fontStyle = FontStyle.Normal;
				Jug3Fill.color = Color.white;
			}
			if(Jug3Int == 0){
				Jug3Empty.color = Color.black;
				Jug3Empty.fontStyle = FontStyle.Normal;
				Jug3Transfer.color = Color.black;
				Jug3Transfer.fontStyle = FontStyle.Normal;
				Jug3Transfer2.color = Color.black;
				Jug3Transfer2.fontStyle = FontStyle.Normal;
			}
			else{
				Jug3Empty.fontStyle = FontStyle.Normal;
				//Jug3Transfer.fontStyle = FontStyle.Normal;
				Jug3Empty.color = Color.white;
				//Jug3Transfer.color = Color.white;
			}
			if(Jug3Int != 0 && Jug1Int != Jug1Max){
				Jug3Transfer.fontStyle = FontStyle.Normal;
				Jug3Transfer.color = Color.white;
			}
			if(Jug3Int != 0 && Jug2Int != Jug2Max){
				Jug3Transfer2.fontStyle = FontStyle.Normal;
				Jug3Transfer2.color = Color.white;
			}
		}
		next.fontStyle = FontStyle.Normal;
		undo.fontStyle = FontStyle.Normal;
		
		// raycast to highlight 3D font when hovered over
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			if(Jug1Fill){
				if(hit.collider.name == "Jug 1 Fill" && Jug1Fill.color == Color.white){
					Jug1Fill.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "Jug 1 Empty" && Jug1Empty.color == Color.white){
					Jug1Empty.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "Jug 1 Transfer" && Jug1Transfer.color == Color.white){
					Jug1Transfer.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "Jug 1 Transfer 2" && Jug1Transfer2.color == Color.white){
					Jug1Transfer2.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "Jug 2 Fill" && Jug2Fill.color == Color.white){
					Jug2Fill.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "Jug 2 Empty" && Jug2Empty.color == Color.white){
					Jug2Empty.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "Jug 2 Transfer" && Jug2Transfer.color == Color.white){
					Jug2Transfer.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "Jug 2 Transfer 2" && Jug2Transfer2.color == Color.white){
					Jug2Transfer2.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "Jug 3 Fill" && Jug3Fill.color == Color.white){
					Jug3Fill.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "Jug 3 Empty" && Jug3Empty.color == Color.white){
					Jug3Empty.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "Jug 3 Transfer" && Jug3Transfer.color == Color.white){
					Jug3Transfer.fontStyle = FontStyle.Bold;
				}
				if(hit.collider.name == "Jug 3 Transfer 2" && Jug3Transfer2.color == Color.white){
					Jug3Transfer2.fontStyle = FontStyle.Bold;
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
				if(hit.collider.name == "Jug 1 Fill"){
					Jug1Int = Jug1Max;
					
					if(Jug1Fill.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "Jug 1 Empty"){
					Jug1Int = 0;
					
					if(Jug1Empty.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "Jug 1 Transfer"){
					for(; Jug1Int>0 && Jug2Int<Jug2Max; Jug1Int--){
						Jug2Int++;
					}
					
					if(Jug1Transfer.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "Jug 1 Transfer 2"){
					for(; Jug1Int>0 && Jug3Int<Jug3Max; Jug1Int--){
						Jug3Int++;
					}
					
					if(Jug1Transfer2.color == Color.white)
						movesCount++;
				}
				
				if(hit.collider.name == "Jug 2 Fill"){
					Jug2Int = Jug2Max;
					
					if(Jug2Fill.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "Jug 2 Empty"){
					Jug2Int = 0;
					
					if(Jug2Empty.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "Jug 2 Transfer"){
					for(;Jug2Int>0 && Jug1Int<Jug1Max; Jug2Int--){
						Jug1Int++;
					}
					
					if(Jug2Transfer.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "Jug 2 Transfer 2"){
					for(;Jug2Int>0 && Jug3Int<Jug3Max; Jug2Int--){
						Jug3Int++;
					}
					
					if(Jug2Transfer2.color == Color.white)
						movesCount++;
				}

				if(hit.collider.name == "Jug 3 Fill"){
					Jug3Int = Jug3Max;
					
					if(Jug3Fill.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "Jug 3 Empty"){
					Jug3Int = 0;
					
					if(Jug3Empty.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "Jug 3 Transfer"){
					for(; Jug3Int>0 && Jug1Int<Jug1Max; Jug3Int--){
						Jug1Int++;
					}
					
					if(Jug1Transfer.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "Jug 3 Transfer 2"){
					for(; Jug3Int>0 && Jug2Int<Jug2Max; Jug3Int--){
						Jug2Int++;
					}
					
					if(Jug1Transfer2.color == Color.white)
						movesCount++;
				}
				if(Jug1Int == winNum ||
				   Jug2Int == winNum ||
				   Jug3Int == winNum){
					if(hit.collider.name == "Next"){
						if(MainMenu.singlePlayer == true){
							Application.LoadLevel("Level 2-3");
						}
						if(MainMenu.singlePlayer == false){
							networkView.RPC("LoadLevel", RPCMode.All, "Level 2-3");
						}
					}
				}
				if(hit.collider.name == "Undo"){
					// not set-up yet.
				}

				// After clicking a button, sync the jug and move int values.
				networkView.RPC("SyncValues", RPCMode.All, Jug1Int, Jug2Int, Jug3Int, movesCount);
			}
		}
	}

	// Sync the jug and move int values.
	[@RPC]
	void SyncValues(int Jug1, int Jug2, int Jug3, int moves){
		Jug1Int = Jug1;
		Jug2Int = Jug2;
		Jug3Int = Jug3;
		movesCount = moves;
	}
	[@RPC]
	void StartSync(int win, int puzzle, int Jug1, int Jug2, int Jug3){
		Jug1Max = Jug1;
		Jug2Max = Jug2;
		Jug3Max = Jug3;
		winNum = win;
		puzzleNum = puzzle;
	}


	// Load levels across the network.
	[@RPC]
	void LoadLevel(string level){
		Application.LoadLevel(level);
	}
}
