using UnityEngine;
using System.Collections;

public class Level1_1Puzzle : MonoBehaviour {
	
	int L3Value;
	int L3Max = 3;
	int L5Value;
	int L5Max = 5;
	int DockValue;
	int Target = 4;
	
	TextMesh L3Text;
	Transform Container3L;
	TextMesh L5Text;
	Transform Container5L;
	GameObject Dock;
	
	TextMesh ChallengeText;
	public static bool hasWon;
	
	// Use this for initialization
	void Start () {
		L3Text = GameObject.Find("3L Text").GetComponent("TextMesh") as TextMesh;
		L5Text = GameObject.Find("5L Text").GetComponent("TextMesh") as TextMesh;
		Dock = GameObject.Find("Dock");
		ChallengeText = GameObject.Find("Challenge Text").GetComponent("TextMesh") as TextMesh;
	}
	
	// Update is called once per frame
	void Update () {
		L3Text.text = L3Value.ToString();
		L5Text.text = L5Value.ToString();
		
		if(GameObject.Find("Container 3L")){
			Container3L = GameObject.Find("Container 3L").GetComponent("Transform") as Transform;
			L3Text.transform.position = Container3L.position - new Vector3(.4f,0,-.7f);
				
			Container5L = GameObject.Find("Container 5L").GetComponent("Transform") as Transform;
			L5Text.transform.position = Container5L.position - new Vector3(.4f,0,-.7f);
		}
		
		// Finish
		if(DockValue == Target){
			hasWon = true;
			ChallengeText.text = "Complete!";
			Dock.renderer.material.color = new Color(0,1,0,0);
			//Destroy(GameObject.Find("Player(Clone)"));
		}
	}

	// Sync the jug values.
	[@RPC]
	void SyncValues(int liter3, int liter5){
		// Seems to reset the value to 0 instead of the actual value.
		//L3Value = liter3;
		//L3Text.text = L3Value.ToString();
		//L3Value = liter5;
		//L5Text.text = L5Value.ToString();
		ChallengeText.text = "Put 4 on Dock" + 
							 "\n3L: " + liter3 + " - 5L: " + liter5;
	}

	void SetValues(){
		networkView.RPC("SyncValues", RPCMode.All, L3Value, L5Value);
	}
	
	// 3L get and set
	public int getL3Value(){
		return L3Value;
	}
	public void setL3Value(int newValue){
		L3Value = newValue;
		SetValues();
	}
	public int getL3Max(){
		return L3Max;
	}
	
	// 5L get and set
	public int getL5Value(){
		return L5Value;
	}
	public void setL5Value(int newValue){
		L5Value = newValue;
		SetValues();
	}
	public int getL5Max(){
		return L5Max;
	}
	
	// Dock get and set
	public int getDockValue(){
		return DockValue;
	}
	public void setDockValue(int newValue){
		DockValue = newValue;
	}
	
	public int getTargetValue(){
		return Target;
	}
}
