using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NickName : MonoBehaviour
{
    public string nickname { get; private set; }

    [SerializeField] private InputField _inputerName;
    [SerializeField] private GameObject _inputPanel;
    

    private void Start()
    {
        var name = new InputField.SubmitEvent();
        name.AddListener(SetName);
        _inputerName.onEndEdit = name;
    }

    private void SetName(string name)
    {
        this.nickname = name;
    }

    public void EnterButton()
    {
        if(_inputerName.text != "")
        {
        PhotonNetwork.NickName = _inputerName.text;
        _inputPanel.SetActive(false);
        }
        
    }

}
