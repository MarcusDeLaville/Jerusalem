using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using Photon.Pun;
using UnityEngine.UI;

public class VoiceTransmiter : MonoBehaviour
{
    private PhotonVoiceNetwork _punVoiceNetwork;

    public Recorder Recorder;

    private void Awake()
    {
        _punVoiceNetwork = PhotonVoiceNetwork.Instance;
    }

    private void Update()
    {
        if(!PlayerUI._frezee)
        {
        if (Input.GetKey(KeyCode.K))
        {
            Recorder.TransmitEnabled = true;
        }
        else
        {
            Recorder.TransmitEnabled = false;
        }
        }
        
    }
}
