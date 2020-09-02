using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using System;
using Photon.Pun;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    private ChatClient _chatClient;
    protected internal ChatAppSettings _chatAppSetttings;

    [SerializeField] private InputField _chatInputField;
    [SerializeField] private Text _connectionState;
    [SerializeField] private Text _messagesArea;
    [SerializeField] private GameObject _chatPanel;
    private string _channel;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        _connectionState.text = "Connecting...";
        _channel = "general";
        Connect();
    }

    private void Connect()
    {
        this._chatClient = new ChatClient(this);
        this._chatClient.UseBackgroundWorkerForSending = true;
        this._chatClient.Connect("4c203ea7-f726-41d1-bcc3-5320b896c047", "1.0", new Photon.Chat.AuthenticationValues(PhotonNetwork.NickName));
        this._chatClient.Subscribe("general");
        _connectionState.text = "Connecting to chat";

    }

    public void OnDestroy()
    {
        if (this._chatClient == null)
        {
 
        }
    }

    public void OnApplicationQuit()
    {
        if(this._chatClient == null)
        {
            this._chatClient.Disconnect();
        }
    }

    public void Update()
    {
        if (this._chatClient != null)
        {
        this._chatClient.Service();
        }
        if (Input.GetKeyDown(KeyCode.T) && _chatPanel.activeSelf == false)
        {    
            _chatPanel.SetActive(true);
            PlayerUI._frezee = true;      
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && _chatPanel.activeSelf == true)
        {
            _chatPanel.SetActive(false);
            PlayerUI._frezee = false;
        }

        
    }

    public void OnEnterSend()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            this.SendChatMessage(this._chatInputField.text);
            this._chatInputField.text = "";
        }
    }
    
    private void SendChatMessage(string inputLine)
    {
        this._chatClient.PublishMessage(_channel, inputLine);
    }
    public void OnClickSend()
    {
       
    }   

    public void OnConnected()
    {
        _connectionState.text = "Connected";
        this._chatClient.Subscribe(new string[] { _channel });
        this._chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void OnDisconnected()
    {

    }

    public void OnChatStateChange(ChatState state)
    { 
      
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach (string channel in channels)
        {
            this._chatClient.PublishMessage(channel, "says 'hi'."); 
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    { 
        channelName = this._channel;
        ChatChannel _channel = null;
        bool found = this._chatClient.TryGetChannel(this._channel, out _channel);

        if (!found)
        {
            Debug.Log("ShowChannel failed to find channel: " + channelName);
            return;
        }
       
        this._messagesArea.text = _channel.ToStringMessages();
    }   

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
       
    }

    public void OnUserSubscribed(string channel, string user)
    {
       
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }

    public void DebugReturn(DebugLevel level, string message)
    {
 
    }

    
}
