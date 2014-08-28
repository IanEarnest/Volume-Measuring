using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	const string typeName = "MyUniqueGameName";
	public static string gameName = "MyRoomName";
	HostData[] hostList;
	public GameObject playerPrefab; // Set playerPrefab in inspector.

	public static string playerName = "Name";
	public static string globalChat = "Chat";
	public static string chatText = "";
	public static string debugText = "";
	string netPass;

	Vector2 chatScroll;
	Vector2 serverListScroll;
	Vector2 debugScroll;

	public static bool minimiseChat;

	Rect menuRect = new Rect(0, 0, 150, 0);

	// Resize debug window.
	public static bool debugExpanded;
	public static bool debugHidden;
	Rect debugWindowRect = new Rect(Screen.width - 65, 30, 0, 0);
	Rect debugHiddenRect = new Rect(Screen.width - 65, 30, 0, 0);
	Rect debugNormalRect = new Rect(Screen.width - 175, 30, 175, 150);
	Rect debugExpandRect = new Rect(Screen.width - 350, 30, 350, 250);

	// Window rectangles.
	Rect serverListWindowRect = new Rect(150, 0, 500, 100);
	Rect serverWindowRect = new Rect(Screen.width-150, 80, 150, 50);
	Rect chatWindowRect = new Rect(150, 0, 200, 250);
	Rect oChatWindowRect;


	void Start(){
		if(MainMenu.singlePlayer == false){
			// Set player random name.
			if(playerName == "Name"){
				playerName = "Steve" + Random.Range(1000,9999);
			}

			// Set debug size at start.
			if(debugExpanded == true){
				debugWindowRect = debugExpandRect;
			}
			else if(debugHidden == true){
				debugWindowRect = debugHiddenRect;
			}
			else{
				debugWindowRect = debugNormalRect;
			}


			oChatWindowRect = chatWindowRect;
		}
	}

	// When script loads
	void Awake(){
		if(MainMenu.singlePlayer == false){
			if (!Network.isClient && !Network.isServer){
				MasterServer.ClearHostList();
				MasterServer.RequestHostList(typeName);
			}
		}
	}

	void Update(){
		if(MainMenu.singlePlayer == false){
			// When disconnected and not on lobby, load lobby.
			if(Network.peerType == NetworkPeerType.Disconnected && Application.loadedLevelName != "Lobby"){
				Application.LoadLevel("Lobby");
			}
		}
		if(Application.loadedLevelName == "Level 1-1"){
			// Turn off player 2 camera on player 1 scene and other way around.
			if(Camera.allCameras.Length > 2){
				//Camera.main.enabled = false; //Turns off wrong player camera.
				for(int i=2; i<Camera.allCameras.Length; i++){
					//Camera.main.enabled = false;
					if(Camera.allCameras[i].enabled){
						Camera.allCameras[i-1].enabled = false;
					}
				}
			}
		}
	}

    void OnGUI(){
		menuRect = GUILayout.Window(0, menuRect, menuFunction, "Menu");
		if(MainMenu.singlePlayer == false){
			// Set a new GUIText as debug/ information for player.
			
			// If game is not a client and is not a server show Welcome and Server List.
			if(!Network.isClient && !Network.isServer){
				serverListWindowRect = GUILayout.Window(1, serverListWindowRect, ServerListWindow, "Server List");
			}
			
			// If game is server show ServerWindow.
			if (Network.isServer){
				serverWindowRect = GUILayout.Window(2, serverWindowRect, ServerWindow, "Server");
			}
			
			// If game is client or server show ChatWindow.
			if(Network.isClient || Network.isServer){
				chatWindowRect = GUILayout.Window(3, chatWindowRect, ChatWindow, "Chat");
			}
			// Debug window.
			debugWindowRect = GUILayout.Window(4, debugWindowRect, DebugWindow, "Debug");
		}
    }

	// Menu window
	void menuFunction(int id){
		// Display players.
		//GUILayout.Label("Players: " + Network.connections.Length);

		if(MainMenu.singlePlayer == true){
			if(GUILayout.Button("Back to SinglePlayer Menu")){
				Application.LoadLevel("SinglePlayer");
			}
		}

		// Buttons for levels
		// Load level buttons to load levels across the network.
		// Only Server can change levels.
		if(MainMenu.singlePlayer == false){
			if (Network.isServer || Network.isClient){			
				// Do not show lobby button on lobby.
				if(Application.loadedLevelName != "Lobby"){
					if(GUILayout.Button("Back to Lobby")){
						networkView.RPC("LoadLevel", RPCMode.All, "Lobby");
					}
				}
				// Show level buttons when on lobby.
				if(Application.loadedLevelName == "Lobby"){
					if(GUILayout.Button("Level 1-1")){
						networkView.RPC("LoadLevel", RPCMode.All, "Level 1-1");
					}
					if(GUILayout.Button("Level 2-1")){
						networkView.RPC("LoadLevel", RPCMode.All, "Level 2-1");
					}
					if(GUILayout.Button("Level 2-2")){
						networkView.RPC("LoadLevel", RPCMode.All, "Level 2-2");
					}
					if(GUILayout.Button("Level 2-3")){
						networkView.RPC("LoadLevel", RPCMode.All, "Level 2-3");
					}
				}
			}

			// Network buttons
			if (!Network.isClient && !Network.isServer){
				if(GUILayout.Button("Back to Main Menu")){
					Application.LoadLevel("Main Menu");
				}
				if (GUILayout.Button("Host a game")) {
					debugText += "\nGame Type Name: " + typeName;
					debugText += "\nGame Name: " + gameName;
					Network.InitializeServer(5, 25000, !Network.HavePublicAddress());
					MasterServer.RegisterHost(typeName, gameName);
				}
				// Player name
				//GUILayout.BeginHorizontal();
					GUILayout.Label("Your player name :", GUILayout.Width(120));
					playerName = GUILayout.TextField(playerName, 10, GUILayout.Width(120));
				//GUILayout.EndHorizontal();

				// Game name
				//GUILayout.BeginHorizontal();
					GUILayout.Label("Game Name: ", GUILayout.Width(120));
					gameName = GUILayout.TextField(gameName, 20, GUILayout.Width(120)); // Too many letters.
				//GUILayout.EndHorizontal();

				if (GUILayout.Button("Refresh Hosts")) {
					// Refresh the host list data.
					MasterServer.ClearHostList();
					MasterServer.RequestHostList(typeName);
					if(MasterServer.PollHostList().Length > 0){
						print("Poll list > 0?");
					}
				}
			}
		}
		
		// Server disconnect button
		// Show button when on lobby.
		if(Network.isServer && Application.loadedLevelName == "Lobby"){
			if (GUILayout.Button("Disconnect Server")) {
				Network.Disconnect();
			}
		}
		// Client disconnect button
		if (Network.isClient){
			if (GUILayout.Button("Disconnect from server")) {
				Network.Disconnect();
			}
		}
	}
	// Load levels across the network.
	[@RPC]
	void LoadLevel(string level){
		Application.LoadLevel(level);
	}

	// When a level loads, spawn players.
	void OnLevelWasLoaded(int level) {
		// Spawn players when they go back to lobby. Spawn player on level 1-1.
		if(Network.peerType != NetworkPeerType.Disconnected){
			if(level == 1 || level == 2){
				SpawnPlayer();
			}
		}
		if(MainMenu.singlePlayer == true){
			if(level == 2){
				Instantiate(playerPrefab, new Vector3(0, Random.Range(1,5),0), Quaternion.identity);
			}
		}
	}



	// ServerWindow
	void ServerWindow(int windowID) {
		GUILayout.Box("Kick players", GUILayout.Width(100));
		// At least one player connected to server.
		if (Network.connections.Length > 0){
			if (GUILayout.Button("Kick all players")){
				for(int i = 0; i < Network.connections.Length; i++) {
					Debug.Log("Disconnecting: " + Network.connections[i].ipAddress + ":" + Network.connections[i].port);
					Network.CloseConnection(Network.connections[i], true);
				}
			}
		}
		
		// Buttons to kick each player.
		for(int i = 0; i < Network.connections.Length; i++) {
			if(GUILayout.Button("Kick player \n" + Network.connections[i].port)){
				Debug.Log("Disconnecting: " + Network.connections[i].ipAddress + ":" + Network.connections[i].port);
				Network.CloseConnection(Network.connections[i], true);
			}
		}
	}







	// ServerListWindow
	void ServerListWindow(int windowID) {
		// No hosts.
		if (MasterServer.PollHostList().Length == 0) {
			GUILayout.Label("No Hosts");
		}
		// Display hosts.
		else if (MasterServer.PollHostList().Length != 0) {
			hostList = MasterServer.PollHostList();
			GUILayout.BeginHorizontal();
			GUILayout.Label("       Name          " + "Players/max" + "                     IP");
			GUILayout.EndHorizontal();
			
			serverListScroll = GUILayout.BeginScrollView (serverListScroll, false, true);
			for(int i = 0; i < hostList.Length; i++) {
				HostData h = hostList[i]; // Less to write
				// Display Server List Hosts longways.
				GUILayout.BeginHorizontal();
				GUILayout.Label(h.gameName + "          " + h.connectedPlayers + "/" + h.playerLimit + "             " + "[" + h.ip[i] + ":" + h.port + "]");
					if(GUILayout.Button("Connect", GUILayout.Width(120))){
						Network.Connect(h);
						debugText += "\nConnecting to " + h.gameName;
					}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndScrollView();
		}
	}
	
	// ChatWindow
	void ChatWindow(int windowID) {
		if(minimiseChat == false){

			GUILayout.BeginHorizontal();
				GUILayout.Label("Players: " + (Network.connections.Length + 1) + "/" + Network.maxConnections, GUILayout.Width(90));
				GUILayout.Label("Welcome " + playerName, GUILayout.Width(70 + (playerName.Length * 7)));
				if(GUILayout.Button("Min")){//, GUILayout.Width(60) // Set smaller so that it doesnt expand for client
					minimiseChat = true;
				}	
				if(Network.isServer){
					if(GUILayout.Button("Clear")){
						globalChat = "";
						networkView.RPC ("SyncChat", RPCMode.All, globalChat);
					}
				}


			GUILayout.EndHorizontal();


			// Chat box, contains all chat.
			chatScroll = GUILayout.BeginScrollView (chatScroll, false, true);
				// Need to display player name left of input message box and on every message sent.
				GUILayout.Box(globalChat);
			GUILayout.EndScrollView();

			// Player input.
			GUILayout.BeginHorizontal();
				GUILayout.Label(playerName + ":", GUILayout.Width(10 + (playerName.Length * 7)));
				chatText = GUILayout.TextField(chatText, GUILayout.Width(200));
					
				// Press button or enter to send message.
				if(GUILayout.Button("Send", GUILayout.Width(50)) && chatText != "" || 
			   						Event.current.type == EventType.KeyDown && chatText != ""){
					// Need to change what is sent, 
					// currently the whole chat is sent but 
					// when the chat is too big it can not be sent.
					PrintText(chatText, new NetworkMessageInfo());
					chatText = "";
					chatScroll.y = 100000; // Set scroll to bottom.
					// All text is wiped when a new person joins.
					networkView.RPC ("SyncChat", RPCMode.All, globalChat);
				}
				
				// Button sends a message to everyone connected.
				if(GUILayout.Button("HI!!", GUILayout.Width(40))){
					networkView.RPC ("PrintText", RPCMode.All, "Hello everyone.");
				}

			GUILayout.EndHorizontal();
		}
		else{
			if(chatWindowRect.width > 600 ){
				chatWindowRect.width = 0;
			}
			if(chatWindowRect.height > 100 ){
				chatWindowRect.height = 0;
			}
			GUILayout.BeginHorizontal();
				GUILayout.Label((Network.connections.Length + 1) + "/" + Network.maxConnections, GUILayout.Width(20));
			GUILayout.BeginScrollView (new Vector2(100, 100000), new GUIStyle(), new GUIStyle(), GUILayout.Width(320), GUILayout.Height(22));
					GUILayout.Box(globalChat);
				GUILayout.EndScrollView();
				if(GUILayout.Button("Max", GUILayout.Width (40))){
					minimiseChat = false;
					chatWindowRect = oChatWindowRect;
				}
			GUILayout.EndHorizontal();
		}
	}
	
	
	// DebugWindow
	void DebugWindow(int windowID) {
		// Scroll view with debug information inside.
		if(debugHidden == false){
			debugScroll = GUILayout.BeginScrollView (debugScroll, false, true);
				GUILayout.Box(debugText);
			GUILayout.EndScrollView();
		}
		
		// Two buttons, Shrink/Expand and Hide/Show.
		GUILayout.BeginHorizontal();
		if(debugHidden == false){
			if(debugExpanded == false){
				if(GUILayout.Button("Expand")){
					debugExpanded = true;
					debugWindowRect = debugExpandRect;
				}
			}
			if(debugExpanded == true){
				if(GUILayout.Button("Shrink")){
					debugExpanded = false;
					debugWindowRect = debugNormalRect;
				}
			}
		}
		if(debugHidden == true){
			if(GUILayout.Button("Show")){
				debugHidden = false;
				if(debugExpanded == true){
					debugWindowRect = debugExpandRect;
				}
				else{
					debugWindowRect = debugNormalRect;
				}
			}
		}
		if(debugHidden == false){
			if(GUILayout.Button("Hide")){
				debugHidden = true;
				debugWindowRect = debugHiddenRect;
			}
		}
		GUILayout.EndHorizontal();
		GUI.DragWindow();
	}

	// RPC examples
	[@RPC]
	void PrintText(string text, NetworkMessageInfo info){
		globalChat += "\n" + playerName + " Says: " + text;
		// RPC example 2 //networkView.RPC ("PrintText", RPCMode.All, "Hello everyone.");
	}
	[@RPC]
	void PrintText(string text){
		globalChat += "\n" + text;
		// RPC example 1 //PrintText("Hello 1");
	}
	[@RPC]
	void SyncChat(string globalChat2){
		globalChat = globalChat2;
	}







	// When player joins or network starts, spawn player.
	void OnServerInitialized(){
		SpawnPlayer();
	}
	void OnConnectedToServer(){
		SpawnPlayer();
	}

	// Spawn playerPrefab over the network.
    void SpawnPlayer(){
		Network.Instantiate(playerPrefab, new Vector3(0, Random.Range(1,5),0), Quaternion.identity, 0);
    }

	// Clean up player objects
	void OnPlayerDisconnected(NetworkPlayer player){
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
	// Destroy game objects.
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		foreach(GameObject playerObject in GameObject.FindGameObjectsWithTag("Players")){
			Destroy(playerObject);
		}
		MasterServer.ClearHostList();
		MasterServer.RequestHostList(typeName);
	}
}
