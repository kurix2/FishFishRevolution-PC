using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public Button startServerButton;
    public InputField portInput;

    private bool gameStarted;

    public bool useWifi = false;

    public GameObject countDownTimer;

   
    // Move me out off here
    public Texture2D TitleScreen;

    void Start()
    {
        portInput.text = "8888";
        StartServer();

        //countDownTimer = GameObject.Find("Timer Object");
     
    }
 
    void Update()
    {

    }

	public void StartServer()
    {
        int port = int.Parse(portInput.text);

        Debug.Log("Trying to start server");
        Network.InitializeServer(2, port, false);

        // For testing only, MasterServer is not reliable
        if (!useWifi)
        MasterServer.RegisterHost("JustAnotherFishingGame5468", "Game 1", "Debugging");

        startServerButton.gameObject.SetActive(false);
        portInput.gameObject.SetActive(false);
	}


	public void OnGUI() {

        if (!gameStarted)
        {
            GUI.Label(new Rect(Screen.width / 2 - TitleScreen.width / 2, Screen.height / 2 - TitleScreen.height / 2, TitleScreen.width, TitleScreen.height), TitleScreen);
            
        }
        else
        {
            return;
        }






        /*
         
        if (Network.isClient) {
            if (GUI.Button(new Rect(25f, 95f, 150f, 30f), "SpawnPlayer"))
            {
                SpawnPlayer();
            } 
        }
			

		if (GUI.Button (new Rect (25f, 25f, 150f, 30f), "Start Server")) {
			StartServer ();
		}

		if (GUI.Button (new Rect (25f, 65f, 150f, 30f), "Refresh List")) {
			StartCoroutine("RefresHostList");
		}
	*/

	}

    void OnServerInitialized()
    {
        Debug.Log("Server Started");
    }


    public GameObject theCamera;
    void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player connected");
        gameStarted = true;
        countDownTimer.GetComponent<CountDownTimer>().Init(3.0f);
        theCamera.GetComponent<Animator>().enabled = true;
        //theCamera.GetComponent<cameraAnimScript>().startAnim();

    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Player disconnected");
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
        Application.LoadLevel(Application.loadedLevel);
    }

    void OnApplicationQuit()
    {
        if (Network.isServer)
            Network.Disconnect(500);
    }

}
