using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
    {
    [SerializeField] private Button _buttonConncted;

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            _buttonConncted.interactable = true;
        }
        ConnectToRussia();
        Debug.Log("Ник игрока установлен:" + PhotonNetwork.NickName);   
    }

    public override void OnConnectedToMaster()
    {
        _buttonConncted.interactable = true;
    }

    public void CreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("general", new Photon.Realtime.RoomOptions { MaxPlayers = 20 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom() 
    {        
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _buttonConncted.interactable = false;
    }

    private void ConnectToRussia()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "ru";
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1.0";
    }

    public void Quit()
    {
        Application.Quit();
    }
}

