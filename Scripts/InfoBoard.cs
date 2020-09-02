using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class InfoBoard : InteractableItemsBase, IPunObservable
{

    [SerializeField] private GameObject _controllPanel;
    [SerializeField] private RawImage _boardImage;
    [SerializeField] private RawImage _lectorImage;
    [SerializeField] private Texture2D[] _slides = new Texture2D[2];
    [SerializeField] private PhotonView _photon;
    private int _index;

    private void Start()
    {
        _controllPanel.SetActive(false);
        SetImage();
    }

    public void SetImage()
    {
        for(int i = 0; i < 2; i++)
        {
            _slides[i] = Resources.Load<Texture2D>($"Images/{i + 1}");
        }
        _boardImage.texture = _slides[0];
        _lectorImage.texture = _slides[0];
    }

    public override void OnInteract()
    {
        this.InteractText = $"Нажмите <color=#0b979c>{InteractKey}</color> чтобы открыть панель доски"; ;

        if (Input.GetKeyDown(InteractKey))
        {
            _controllPanel.SetActive(true);
            PlayerUI._frezee = true;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_index);
        }
        else if (stream.IsReading)
        {
            _index = (int)stream.ReceiveNext();
            _boardImage.texture = _slides[_index];
            _lectorImage.texture = _slides[_index];
        }
    }

    public void Switch(int i)
    {      
        _index += i;

        if (_index < 0)
        {
            _index = _slides.Length - 1;
        }
        if (_index == _slides.Length)
        {
            _index = 0;
        }

        _boardImage.texture = _slides[_index];
        _lectorImage.texture = _slides[_index];
    }

    public void Trasfer(Player player)
    {
        if (!(_photon.Owner == player))
        {
            _photon.TransferOwnership(player);
        }

    }
}
