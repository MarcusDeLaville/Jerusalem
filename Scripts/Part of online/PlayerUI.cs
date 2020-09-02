using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviourPunCallbacks
{

    [SerializeField]private GameObject _EscMenu; 
    public static bool _frezee { get; set; }

    public void Start()
    {
       
        if(_EscMenu.activeSelf == true)
        {
            _EscMenu.SetActive(false);
        }

    }
    public void FixedUpdate()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
        {          
            if (_EscMenu.activeSelf == false)
            {
                _EscMenu.SetActive(true);
                _frezee = true;
            }
            else if (_EscMenu.activeSelf == true)
            {
                _EscMenu.SetActive(false);
                _frezee = false;
            }
        }  

    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    
}
