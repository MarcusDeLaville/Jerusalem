using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class Door : InteractableItemsBase, IPunObservable
{
    [SerializeField] private bool _isOpen = false; 
    [SerializeField] private Animator _animator;
    [SerializeField] private PhotonView _photon;

    public override void OnInteract()
    {
        this.InteractText = $"Нажмите <color=#0b979c>{InteractKey}</color> чтобы "; ;
        this.InteractText += _isOpen ? "закрыть" : "открыть";
        

        if (Input.GetKeyDown(InteractKey))
        {
            _isOpen = !_isOpen;
            _animator.SetBool("isOpen", _isOpen);
        }    
    }

    public void Trasfer(Player player)
    {
        if(!(_photon.Owner == player))
        {
        _photon.TransferOwnership(player);
        }
       
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_isOpen);
        }
        else if (stream.IsReading)
        {
            this._isOpen = (bool)stream.ReceiveNext();
        }
    }
}
