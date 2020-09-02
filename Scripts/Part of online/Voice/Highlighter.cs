using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.Unity;
using Photon.Voice.PUN;
using Photon.Pun;

[RequireComponent(typeof(Canvas))]
public class Highlighter : MonoBehaviourPunCallbacks, IPunObservable
{
    private Canvas _canvas;
    private Animator _animator;
    private PhotonVoiceView _photonVoiceView;
    [SerializeField] private Text _nicknameText;
    [SerializeField] private Image _speakerSpriteCanvas;
    private PhotonView _photonView;

    private string _nickname;

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
        _canvas = GetComponent<Canvas>();
        _photonVoiceView = GetComponentInParent<PhotonVoiceView>();
    }

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();    
        this._nickname = _photonView.Owner.NickName;
        this._nicknameText.text = this._nickname;
    }

    private void Update()
    {    
        _speakerSpriteCanvas.enabled = _photonVoiceView.IsRecording;      
        _animator.SetBool("Talking", _photonVoiceView.IsRecording);     
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_nickname);
        }
        else if(stream.IsReading)
        {
            _nickname = (string)stream.ReceiveNext();
            _nicknameText.text = _nickname;
        }
    }

}
