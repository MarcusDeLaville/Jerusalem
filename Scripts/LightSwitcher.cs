using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LightSwitcher : InteractableItemsBase, IPunObservable
{
    [SerializeField] private bool _isEnabled;
    [SerializeField] private GameObject _groupLamps;
    private PhotonView _photon;

    private void Start()
    {
        _photon = GetComponent<PhotonView>();
        _isEnabled = _groupLamps.activeSelf;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_isEnabled);
        }
        else if (stream.IsReading)
        {
            this._isEnabled = (bool)stream.ReceiveNext();    
        }
    }

    public void Update()
    {
        _groupLamps.SetActive(_isEnabled);
    }

    public override void OnInteract()
    {
        InteractText = $"Нажмите <color=#0b979c>{InteractKey}</color> чтобы "; ; 
        InteractText += _isEnabled ? "включить" : "выключить";

        if (Input.GetKeyDown(InteractKey))
        {
        _isEnabled = !_isEnabled;
        _groupLamps.SetActive(_isEnabled);
        }
        
    }

    public void Trasfer(Player player)
    {
        if (!(_photon.Owner == player))
        {
            _photon.TransferOwnership(player);
        }

    }

}
