using Photon.Pun;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;


    public GameObject obstaclesManager;
    private List<string> obstacles;
    private Vector3 obstacles1;
    private Vector3 obstacles2;
    private Vector3 obstacles3;
    private Vector3 obstacles4;

    /*
     * -2 0.5 z  Obstacles1
     * 2 0.5 z
     * 
     * 2.5 2.5 z  Obstacles2
     * -2.5 2.5 z 
     *
     * -2.5 2.5 z Obstacles3
     *
     * 
     * 2.5 2.5 z Obstacles4
     */

    #region Private Serializable Fields


    #endregion


    #region Private Fields


    /// <summary>
    /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
    /// </summary>
    string gameVersion = "1";


    #endregion


    #region MonoBehaviour CallBacks


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    void Start()
    {

        obstacles = new List<string>();
        obstacles.Add("Obstacle1");
        obstacles.Add("Obstacle2");
        obstacles.Add("Obstacle3");
        obstacles.Add("Obstacle4");
        obstacles.Add("Obstacle5");

        Connect();

        
    }

    #endregion
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null);
    }


    #region Public Methods


    /// <summary>
    /// Start the connection process.
    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network
    /// </summary>
    public void Connect()
    {
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinOrCreateRoom("Room 1", new Photon.Realtime.RoomOptions() { MaxPlayers = 4 }, default);
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            // PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Room 1", new Photon.Realtime.RoomOptions() { MaxPlayers = 4 }, default);

    }

    public override void OnJoinedRoom()
    {
        
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        if (PhotonNetwork.IsMasterClient)
        {
            int index = 4;
            string name = "Obstacles";
            Vector3 spawnLocation = new Vector3(2.5f, 2.5f, 0f);

            for (int z = 10; z < 200; z += 15)
            {
                index = Random.Range(1, 5);
                if ( index == 3)
                {
                    var obstacle = PhotonNetwork.Instantiate("Obstacles3", new Vector3(-2.5f, 2.5f, z), Quaternion.identity, 0);
                    obstacle.transform.parent = obstaclesManager.transform;
                }
            
                else if ( index == 1)
                {
                    int leftOrRight = Random.Range(-5, 5);
                    if (leftOrRight >= 0)
                    {
                        var obstacle = PhotonNetwork.Instantiate("Obstacles1", new Vector3(-2f, 0.5f, z), Quaternion.Euler(0.0f, 0.0f, 90.0f), 0);
                        obstacle.transform.parent = obstaclesManager.transform;
                    }
                    else
                    {
                        var obstacle = PhotonNetwork.Instantiate("Obstacles1", new Vector3(2f, 0.5f, z), Quaternion.Euler(0.0f, 0.0f, 90.0f), 0);
                        obstacle.transform.parent = obstaclesManager.transform;
                    }
                }

                else if (index == 2)
                {
                    int leftOrRight = Random.Range(-5, 5);
                    if (leftOrRight >= 0)
                    {
                        var obstacle = PhotonNetwork.Instantiate("Obstacles2", new Vector3(2.5f, 2.5f, z), Quaternion.identity, 0);
                        obstacle.transform.parent = obstaclesManager.transform;
                    }
                    else
                    {
                        var obstacle = PhotonNetwork.Instantiate("Obstacles2", new Vector3(-2.5f, 2.5f, z), Quaternion.identity, 0);
                        obstacle.transform.parent = obstaclesManager.transform;
                    }
                }

                else if (index == 4)
                {
                    var obstacle = PhotonNetwork.Instantiate("Obstacles4", new Vector3(2.5f, 2.5f, z), Quaternion.identity, 0);
                    obstacle.transform.parent = obstaclesManager.transform;
                }
            }
        }

        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        }

    }

    #endregion
    private void Update()
    {
    }

}


