using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCamera : MonoBehaviour
{
    private Camera _camera;
    private PhotonView _photonView;



    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _camera = GetComponent<Camera>();

        if (!_photonView.IsMine)
        {
            _camera.enabled = false;
        }
    }
}
