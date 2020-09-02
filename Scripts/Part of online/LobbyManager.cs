using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _spawnPoint;
    private int _index;

    private void Start()
    {
        _index = PlayerPrefs.GetInt("Character") + 1;
        InstantiatePlayer("Player" + _index);
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Игрок {0} вошёл в комнату", newPlayer.NickName);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Игрок {0} покинул комнату", otherPlayer.NickName);
    }
    
    public void InstantiatePlayer(string name)
    {
        PhotonNetwork.Instantiate(Resources.Load(name).name, _spawnPoint.transform.position, Quaternion.identity);  
    }
   
    private void Update()
    {
        print("Player" + _index);
    }
}
