using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    float lastSynchronizationTime = 0f;
    float syncDelay = 0f;
    float syncTime = 0f;
    Vector3 syncStartPosition = Vector3.zero;
    Vector3 syncEndPosition = Vector3.zero;
	
	void Awake(){
		lastSynchronizationTime = Time.time;
	}
	
	void Update(){
		// Seperate controlls
		if(networkView.isMine || MainMenu.singlePlayer == true){
			InputMovement();
		}
		else{
			SyncedMovement();	
		}
	}
	
	// Player movement
	void InputMovement(){
		// WASD for Forward, left, back, right.
		if(Input.GetKey(KeyCode.W)){ rigidbody.MovePosition(rigidbody.position + Vector3.forward * speed * Time.deltaTime);}
		if(Input.GetKey(KeyCode.A)){ rigidbody.MovePosition(rigidbody.position + Vector3.left * speed * Time.deltaTime);}
		if(Input.GetKey(KeyCode.S)){ rigidbody.MovePosition(rigidbody.position + Vector3.back * speed * Time.deltaTime);}
		if(Input.GetKey(KeyCode.D)){ rigidbody.MovePosition(rigidbody.position + Vector3.right * speed * Time.deltaTime);}
		// R for change colour.
		if(Input.GetKeyDown(KeyCode.R)){ChangeColor();}
		
		// Reset player position when too far away.
		Vector3 resetPosition = new Vector3(0, Random.Range(1,5),0);
		int distance = 50;
		if(transform.position.x > distance || transform.position.x < -distance ||
		   transform.position.y > distance || transform.position.y < -distance ||
		   transform.position.z > distance || transform.position.z < -distance){

			transform.position = resetPosition;
		}
	}
	
	// Other players on network movement
	private void SyncedMovement(){
		syncTime += Time.deltaTime;
		// Sync movement up smoothly.
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}
	
	// Change your own player colour
	void ChangeColor(){
		ChangeColorTo(new Vector3(Random.Range (0f,1f),Random.Range(0f,1f),Random.Range (0f,1f)));
	}
	// Change your own player colour over the network
	[RPC] 
	void ChangeColorTo(Vector3 color){
		renderer.material.color = new Color(color.x,color.y,color.z, 1f);
		if(networkView.isMine){
			networkView.RPC ("ChangeColorTo", RPCMode.OthersBuffered, color);	
		}
	}
	
	// Keep object positions the same for all players
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if(stream.isWriting){
			syncPosition = rigidbody.position;
			stream.Serialize (ref syncPosition);
			
			syncPosition = rigidbody.velocity;
			stream.Serialize (ref syncVelocity);
		}
		else{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
			
		}
	}

	void OnNetworkInstantiate(NetworkMessageInfo info){
		ChangeColor();
	}
}
