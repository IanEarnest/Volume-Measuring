using UnityEngine;
using System.Collections;

public class Level2_2Puzzle : MonoBehaviour {
	
	// 4 Liter jug
	int liter4Int;
	int liter4Max = 4;
	GameObject liter4Jug1;
	GameObject liter4Jug2;
	GameObject liter4Jug3;
	GameObject liter4Jug4;
	TextMesh liter4Value;
	TextMesh liter4Transfer;
	TextMesh liter4Transfer2;
	TextMesh liter4Transfer3;
	
	// 5 Liter jug
	int liter5Int;
	int liter5Max = 5;
	GameObject liter5Jug1;
	GameObject liter5Jug2;
	GameObject liter5Jug3;
	GameObject liter5Jug4;
	GameObject liter5Jug5;
	TextMesh liter5Value;
	TextMesh liter5Transfer;
	TextMesh liter5Transfer2;
	TextMesh liter5Transfer4;
	
	// 10 Liter jug
	int liter10Int;
	int liter10Max = 10;
	GameObject liter10Jug1;
	GameObject liter10Jug2;
	GameObject liter10Jug3;
	GameObject liter10Jug4;
	GameObject liter10Jug5;
	GameObject liter10Jug6;
	GameObject liter10Jug7;
	GameObject liter10Jug8;
	GameObject liter10Jug9;
	GameObject liter10Jug10;
	TextMesh liter10Value;
	TextMesh liter10Transfer;
	TextMesh liter10Transfer3;
	TextMesh liter10Transfer4;
	
	// other 10 Liter jug
	int liter210Int;
	int liter210Max = 10;
	GameObject liter210Jug1;
	GameObject liter210Jug2;
	GameObject liter210Jug3;
	GameObject liter210Jug4;
	GameObject liter210Jug5;
	GameObject liter210Jug6;
	GameObject liter210Jug7;
	GameObject liter210Jug8;
	GameObject liter210Jug9;
	GameObject liter210Jug10;
	TextMesh liter210Value;
	TextMesh liter210Transfer2;
	TextMesh liter210Transfer3;
	TextMesh liter210Transfer4;
	
	
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
		liter4Jug1 = GameObject.Find("4 Liter Jug Water 1");
		liter4Jug2 = GameObject.Find("4 Liter Jug Water 2");
		liter4Jug3 = GameObject.Find("4 Liter Jug Water 3");
		liter4Jug4 = GameObject.Find("4 Liter Jug Water 4");
		liter4Value = findMesh(liter4Value,"4 Liter Value");
		liter4Transfer = findMesh(liter4Transfer,"4 Liter Transfer");
		liter4Transfer2 = findMesh(liter4Transfer2,"4 Liter Transfer2");
		liter4Transfer3 = findMesh(liter4Transfer3,"4 Liter Transfer3");
		
		liter5Jug1 = GameObject.Find("5 Liter Jug Water 1");
		liter5Jug2 = GameObject.Find("5 Liter Jug Water 2");
		liter5Jug3 = GameObject.Find("5 Liter Jug Water 3");
		liter5Jug4 = GameObject.Find("5 Liter Jug Water 4");
		liter5Jug5 = GameObject.Find("5 Liter Jug Water 5");
		liter5Value = findMesh(liter5Value,"5 Liter Value");
		liter5Transfer = findMesh(liter5Transfer,"5 Liter Transfer");
		liter5Transfer2 = findMesh(liter5Transfer2,"5 Liter Transfer2");
		liter5Transfer4 = findMesh(liter5Transfer4,"5 Liter Transfer4");
		
		liter10Jug1 = GameObject.Find("10 Liter Jug Water 1");
		liter10Jug2 = GameObject.Find("10 Liter Jug Water 2");
		liter10Jug3 = GameObject.Find("10 Liter Jug Water 3");
		liter10Jug4 = GameObject.Find("10 Liter Jug Water 4");
		liter10Jug5 = GameObject.Find("10 Liter Jug Water 5");
		liter10Jug6 = GameObject.Find("10 Liter Jug Water 6");
		liter10Jug7 = GameObject.Find("10 Liter Jug Water 7");
		liter10Jug8 = GameObject.Find("10 Liter Jug Water 8");
		liter10Jug9 = GameObject.Find("10 Liter Jug Water 9");
		liter10Jug10 = GameObject.Find("10 Liter Jug Water 10");
		liter10Value = findMesh(liter10Value,"10 Liter Value");
		liter10Transfer = findMesh(liter10Transfer,"10 Liter Transfer");
		liter10Transfer3 = findMesh(liter10Transfer3,"10 Liter Transfer3");
		liter10Transfer4 = findMesh(liter10Transfer4,"10 Liter Transfer4");
		
		liter210Jug1 = GameObject.Find("10 Liter2 Jug Water 1");
		liter210Jug2 = GameObject.Find("10 Liter2 Jug Water 2");
		liter210Jug3 = GameObject.Find("10 Liter2 Jug Water 3");
		liter210Jug4 = GameObject.Find("10 Liter2 Jug Water 4");
		liter210Jug5 = GameObject.Find("10 Liter2 Jug Water 5");
		liter210Jug6 = GameObject.Find("10 Liter2 Jug Water 6");
		liter210Jug7 = GameObject.Find("10 Liter2 Jug Water 7");
		liter210Jug8 = GameObject.Find("10 Liter2 Jug Water 8");
		liter210Jug9 = GameObject.Find("10 Liter2 Jug Water 9");
		liter210Jug10 = GameObject.Find("10 Liter2 Jug Water 10");
		liter210Value = findMesh(liter210Value,"10 Liter2 Value");
		liter210Transfer2 = findMesh(liter210Transfer2,"10 Liter2 Transfer2");
		liter210Transfer3 = findMesh(liter210Transfer3,"10 Liter2 Transfer3");
		liter210Transfer4 = findMesh(liter210Transfer4,"10 Liter2 Transfer4");
		
		moves = findMesh(moves,"Moves Count");
		next = findMesh (next,"Next");
		next.renderer.enabled = false;
		finish = findMesh(finish,"Finish");
		finish.text = "";
		undo = findMesh (undo,"Undo");
		
		liter10Int = liter10Max;
		liter210Int = liter210Max;

		// Check connection before spawning, fixes error.
		if(Network.peerType != NetworkPeerType.Disconnected){
			pointer = (GameObject)Network.Instantiate(pointer, Vector3.zero, Quaternion.identity, 0);
			// set pointer name
			networkView.RPC("SetNames", RPCMode.AllBuffered);
		}
	}

	// Set pointer name.
	[@RPC]
	void SetNames(){
		//if(Network.isServer){
		pointer.GetComponentInChildren<TextMesh>().text = NetworkManager.playerName;
	}

	void Update () {
		liter4Value.text = liter4Int.ToString() + "/" + liter4Max;
		liter5Value.text = liter5Int.ToString() + "/" + liter5Max;
		liter10Value.text = liter10Int.ToString() + "/" + liter10Max;
		liter210Value.text = liter210Int.ToString() + "/" + liter210Max;
		
		moves.text = movesCount.ToString();

		// Check pointer exists, fixes error. Set pointer at mouse position.
		if(pointer){
			pointer.transform.rigidbody.MovePosition(hit.point);
		}


		// Game Over conditions
		if(liter4Int == 2 && liter5Int == 2){
			// Display win text
			finish.text = "Finished in " + movesCount + " Moves.";
			next.renderer.enabled = true;
			// Hide controlls
			if(liter4Transfer){
				Destroy(liter4Transfer.collider);
				Destroy(liter4Transfer2.collider);
				Destroy(liter4Transfer3.collider);
				Destroy(liter5Transfer.collider);
				Destroy(liter5Transfer2.collider);
				Destroy(liter5Transfer4.collider);
				Destroy(liter10Transfer.collider);
				Destroy(liter10Transfer3.collider);
				Destroy(liter10Transfer4.collider);
				Destroy(liter210Transfer2.collider);
				Destroy(liter210Transfer3.collider);
				Destroy(liter210Transfer4.collider);
				}
			Destroy(liter4Transfer);
			Destroy(liter4Transfer2);
			Destroy(liter4Transfer3);
			Destroy(liter5Transfer);
			Destroy(liter5Transfer2);
			Destroy(liter5Transfer4);
			Destroy(liter10Transfer);
			Destroy(liter10Transfer3);
			Destroy(liter10Transfer4);
			Destroy(liter210Transfer2);
			Destroy(liter210Transfer3);
			Destroy(liter210Transfer4);
		}
		
		
		// Graphics for jugs
		liter4Jug1.renderer.enabled = false;
		liter4Jug2.renderer.enabled = false;
		liter4Jug3.renderer.enabled = false;
		liter4Jug4.renderer.enabled = false;
		liter5Jug1.renderer.enabled = false;
		liter5Jug2.renderer.enabled = false;
		liter5Jug3.renderer.enabled = false;
		liter5Jug4.renderer.enabled = false;
		liter5Jug5.renderer.enabled = false;
		liter10Jug1.renderer.enabled = false;
		liter10Jug2.renderer.enabled = false;
		liter10Jug3.renderer.enabled = false;
		liter10Jug4.renderer.enabled = false;
		liter10Jug5.renderer.enabled = false;
		liter10Jug6.renderer.enabled = false;
		liter10Jug7.renderer.enabled = false;
		liter10Jug8.renderer.enabled = false;
		liter10Jug9.renderer.enabled = false;
		liter10Jug10.renderer.enabled = false;
		liter210Jug1.renderer.enabled = false;
		liter210Jug2.renderer.enabled = false;
		liter210Jug3.renderer.enabled = false;
		liter210Jug4.renderer.enabled = false;
		liter210Jug5.renderer.enabled = false;
		liter210Jug6.renderer.enabled = false;
		liter210Jug7.renderer.enabled = false;
		liter210Jug8.renderer.enabled = false;
		liter210Jug9.renderer.enabled = false;
		liter210Jug10.renderer.enabled = false;
		if(liter4Int == 1){
			liter4Jug1.renderer.enabled = true;
		}
		if(liter4Int == 2){
			liter4Jug2.renderer.enabled = true;
		}
		if(liter4Int == 3){
			liter4Jug3.renderer.enabled = true;
		}
		if(liter4Int == 4){
			liter4Jug4.renderer.enabled = true;
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
		if(liter10Int == 1){
			liter10Jug1.renderer.enabled = true;
		}
		if(liter10Int == 2){
			liter10Jug2.renderer.enabled = true;
		}
		if(liter10Int == 3){
			liter10Jug3.renderer.enabled = true;
		}
		if(liter10Int == 4){
			liter10Jug4.renderer.enabled = true;
		}		
		if(liter10Int == 5){
			liter10Jug5.renderer.enabled = true;
		}
		if(liter10Int == 6){
			liter10Jug6.renderer.enabled = true;
		}
		if(liter10Int == 7){
			liter10Jug7.renderer.enabled = true;
		}
		if(liter10Int == 8){
			liter10Jug8.renderer.enabled = true;
		}
		if(liter10Int == 9){
			liter10Jug9.renderer.enabled = true;
		}		
		if(liter10Int == 10){
			liter10Jug10.renderer.enabled = true;
		}
		if(liter210Int == 1){
			liter210Jug1.renderer.enabled = true;
		}
		if(liter210Int == 2){
			liter210Jug2.renderer.enabled = true;
		}
		if(liter210Int == 3){
			liter210Jug3.renderer.enabled = true;
		}
		if(liter210Int == 4){
			liter210Jug4.renderer.enabled = true;
		}		
		if(liter210Int == 5){
			liter210Jug5.renderer.enabled = true;
		}
		if(liter210Int == 6){
			liter210Jug6.renderer.enabled = true;
		}
		if(liter210Int == 7){
			liter210Jug7.renderer.enabled = true;
		}
		if(liter210Int == 8){
			liter210Jug8.renderer.enabled = true;
		}
		if(liter210Int == 9){
			liter210Jug9.renderer.enabled = true;
		}		
		if(liter210Int == 10){
			liter210Jug10.renderer.enabled = true;
		}
		
		// Black and white
		if(liter4Transfer){
			// First 10 liter
			if(liter210Int != liter210Max){
				liter10Transfer.color = Color.white;
				liter4Transfer.color = Color.white;
				liter5Transfer.color = Color.white;	
			}
			else{
				liter10Transfer.color = Color.black;
				liter4Transfer.color = Color.black;
				liter5Transfer.color = Color.black;	
			}
			// Second 10 liter
			if(liter10Int != liter10Max){
				liter210Transfer2.color = Color.white;
				liter4Transfer2.color = Color.white;
				liter5Transfer2.color = Color.white;
			}
			else{
				liter210Transfer2.color = Color.black;
				liter4Transfer2.color = Color.black;
				liter5Transfer2.color = Color.black;
			}
			// 4 liter
			if(liter4Int != liter4Max){
				liter210Transfer4.color = Color.white;
				liter10Transfer4.color = Color.white;
				liter5Transfer4.color = Color.white;
			}
			else{
				liter210Transfer4.color = Color.black;
				liter10Transfer4.color = Color.black;
				liter5Transfer4.color = Color.black;
			}
			// 5 liter
			if(liter5Int != liter5Max){
				liter210Transfer3.color = Color.white;
				liter10Transfer3.color = Color.white;
				liter4Transfer3.color = Color.white;
			}
			else{
				liter210Transfer3.color = Color.black;
				liter10Transfer3.color = Color.black;
				liter4Transfer3.color = Color.black;	
			}
			
			
			// Second 10 liter empty
			if(liter210Int == 0){
				liter210Transfer2.color = Color.black;
				liter210Transfer2.fontStyle = FontStyle.Normal;
				liter210Transfer3.color = Color.black;
				liter210Transfer3.fontStyle = FontStyle.Normal;
				liter210Transfer4.color = Color.black;
				liter210Transfer4.fontStyle = FontStyle.Normal;
			}
			// First 10 liter empty
			if(liter10Int == 0){
				liter10Transfer.color = Color.black;
				liter10Transfer.fontStyle = FontStyle.Normal;
				liter10Transfer3.color = Color.black;
				liter10Transfer3.fontStyle = FontStyle.Normal;
				liter10Transfer4.color = Color.black;
				liter10Transfer4.fontStyle = FontStyle.Normal;
			}
			// 5 liter empty
			if(liter5Int == 0){
				liter5Transfer.color = Color.black;
				liter5Transfer.fontStyle = FontStyle.Normal;
				liter5Transfer2.color = Color.black;
				liter5Transfer2.fontStyle = FontStyle.Normal;
				liter5Transfer4.color = Color.black;
				liter5Transfer4.fontStyle = FontStyle.Normal;
			}
			// 4 liter empty
			if(liter4Int == 0){
				liter4Transfer.color = Color.black;
				liter4Transfer.fontStyle = FontStyle.Normal;
				liter4Transfer2.color = Color.black;
				liter4Transfer2.fontStyle = FontStyle.Normal;
				liter4Transfer3.color = Color.black;
				liter4Transfer3.fontStyle = FontStyle.Normal;
			}
		}
		
		next.fontStyle = FontStyle.Normal;
		undo.fontStyle = FontStyle.Normal;
		
		
		// raycast to highlight 3D font when hovered over
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			if(liter4Transfer){
				// 4 Liter
				if(hit.collider.name == "4 Liter Transfer" && liter4Transfer.color == Color.white){
					liter4Transfer.fontStyle = FontStyle.Bold;
				}
				else{
					liter4Transfer.fontStyle = FontStyle.Normal;
				}
				if(hit.collider.name == "4 Liter Transfer2" && liter4Transfer2.color == Color.white){
					liter4Transfer2.fontStyle = FontStyle.Bold;
				}
				else{
					liter4Transfer2.fontStyle = FontStyle.Normal;
				}
				if(hit.collider.name == "4 Liter Transfer3" && liter4Transfer3.color == Color.white){
					liter4Transfer3.fontStyle = FontStyle.Bold;
				}
				else{
					liter4Transfer3.fontStyle = FontStyle.Normal;
				}
				// 5 Liter
				if(hit.collider.name == "5 Liter Transfer" && liter5Transfer.color == Color.white){
					liter5Transfer.fontStyle = FontStyle.Bold;
				}
				else{
					liter5Transfer.fontStyle = FontStyle.Normal;
				}
				if(hit.collider.name == "5 Liter Transfer2" && liter5Transfer2.color == Color.white){
					liter5Transfer2.fontStyle = FontStyle.Bold;
				}
				else{
					liter5Transfer2.fontStyle = FontStyle.Normal;
				}
				if(hit.collider.name == "5 Liter Transfer4" && liter5Transfer4.color == Color.white){
					liter5Transfer4.fontStyle = FontStyle.Bold;
				}
				else{
					liter5Transfer4.fontStyle = FontStyle.Normal;
				}
				// 10 Liter
				if(hit.collider.name == "10 Liter Transfer" && liter10Transfer.color == Color.white){
					liter10Transfer.fontStyle = FontStyle.Bold;
				}
				else{
					liter10Transfer.fontStyle = FontStyle.Normal;
				}
				if(hit.collider.name == "10 Liter Transfer3" && liter10Transfer3.color == Color.white){
					liter10Transfer3.fontStyle = FontStyle.Bold;
				}
				else{
					liter10Transfer3.fontStyle = FontStyle.Normal;
				}
				if(hit.collider.name == "10 Liter Transfer4" && liter10Transfer4.color == Color.white){
					liter10Transfer4.fontStyle = FontStyle.Bold;
				}
				else{
					liter10Transfer4.fontStyle = FontStyle.Normal;
				}
				// 10 Liter2
				if(hit.collider.name == "10 Liter2 Transfer2" && liter210Transfer2.color == Color.white){
					liter210Transfer2.fontStyle = FontStyle.Bold;
				}
				else{
					liter210Transfer2.fontStyle = FontStyle.Normal;
				}
				if(hit.collider.name == "10 Liter2 Transfer3" && liter210Transfer3.color == Color.white){
					liter210Transfer3.fontStyle = FontStyle.Bold;
				}
				else{
					liter210Transfer3.fontStyle = FontStyle.Normal;
				}
				if(hit.collider.name == "10 Liter2 Transfer4" && liter210Transfer4.color == Color.white){
					liter210Transfer4.fontStyle = FontStyle.Bold;
				}
				else{
					liter210Transfer4.fontStyle = FontStyle.Normal;
				}
			}
			if(hit.collider.name == "Next"){
				next.fontStyle = FontStyle.Bold;
			}
			if(hit.collider.name == "Undo"){
				undo.fontStyle = FontStyle.Bold;
			}
		}
		
		// Click to use 3D TextMesh as buttons.
		if (Input.GetButtonDown ("Fire1")) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		    if (Physics.Raycast (ray, out hit)) {
				// 4 Liter buttons
				if(hit.collider.name == "4 Liter Transfer"){
					for(;liter4Int>0 && liter210Int<liter210Max; liter4Int--){
						liter210Int++;
					}
					if(liter4Transfer.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "4 Liter Transfer2"){
					for(;liter4Int>0 && liter10Int<liter10Max; liter4Int--){
						liter10Int++;
					}
					if(liter4Transfer2.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "4 Liter Transfer3"){
					for(;liter4Int>0 && liter5Int<liter5Max; liter4Int--){
						liter5Int++;
					}
					if(liter4Transfer3.color == Color.white)
						movesCount++;
				}
				
				// 5 Liter buttons
				if(hit.collider.name == "5 Liter Transfer"){
					for(;liter5Int>0 && liter210Int<liter210Max; liter5Int--){
						liter210Int++;
					}
					if(liter5Transfer.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "5 Liter Transfer2"){
					for(;liter5Int>0 && liter10Int<liter10Max; liter5Int--){
						liter10Int++;
					}
					if(liter5Transfer2.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "5 Liter Transfer4"){
					for(;liter5Int>0 && liter4Int<liter4Max; liter5Int--){
						liter4Int++;
					}
					if(liter5Transfer4.color == Color.white)
						movesCount++;
				}
				
				// 10 Liter buttons
				if(hit.collider.name == "10 Liter Transfer"){
					for(;liter10Int>0 && liter210Int<liter210Max; liter10Int--){
						liter210Int++;
					}
					if(liter10Transfer.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "10 Liter Transfer3"){
					for(;liter10Int>0 && liter5Int<liter5Max; liter10Int--){
						liter5Int++;
					}
					if(liter10Transfer3.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "10 Liter Transfer4"){
					for(;liter10Int>0 && liter4Int<liter4Max; liter10Int--){
						liter4Int++;
					}
					if(liter10Transfer4.color == Color.white)
						movesCount++;
				}
				
				// 10 Liter2 buttons
				if(hit.collider.name == "10 Liter2 Transfer2"){
					for(;liter210Int>0 && liter10Int<liter10Max; liter210Int--){
						liter10Int++;
					}
					if(liter210Transfer2.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "10 Liter2 Transfer3"){
					for(;liter210Int>0 && liter5Int<liter5Max; liter210Int--){
						liter5Int++;
					}
					if(liter210Transfer3.color == Color.white)
						movesCount++;
				}
				if(hit.collider.name == "10 Liter2 Transfer4"){
					for(;liter210Int>0 && liter4Int<liter4Max; liter210Int--){
						liter4Int++;
					}
					if(liter210Transfer4.color == Color.white)
						movesCount++;
				}
				
				if(hit.collider.name == "Next" && liter4Int == 2 && liter5Int == 2){
					networkView.RPC("LoadLevel", RPCMode.All, "Level 2-1");
				}
				if(hit.collider.name == "Undo"){
					// not set-up yet.
				}

				// After clicking a button, sync the jug and move int values.
				networkView.RPC("SyncValues", RPCMode.All, liter4Int, liter5Int, liter10Int, liter210Int, movesCount);
			}
		}
	}

	// Sync the jug and move int values.
	[@RPC]
	void SyncValues(int liter4, int liter5, int liter10, int liter210, int moves){
		liter4Int = liter4;
		liter5Int = liter5;
		liter10Int = liter10;
		liter210Int = liter210;
		movesCount = moves;
	}

	// Load levels across the network.
	[@RPC]
	void LoadLevel(string level){
		Application.LoadLevel(level);
	}
}
