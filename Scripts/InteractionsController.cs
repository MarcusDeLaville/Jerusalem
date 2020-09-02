using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class InteractionsController : MonoBehaviour
{
    [SerializeField] private Text _screenStatus;

    private float _distance = 5; // в приделах этой дистанции объект будет доступна
    private const string _doorTag = "Door"; 
    private const string _chairTag = "Chair"; 
    private const string _switcherTag = "Switch";
    private const string _boardTag = "Board";

    [SerializeField] private GameObject _character;
    private Animator _animator;

    private PhotonView _photonView;
    private Player player;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _animator = _character.GetComponent<Animator>();

        player = _photonView.Owner;
        if (!_photonView.IsMine)
        {
            _screenStatus.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, _distance))
        {
            string tag = hit.collider.tag;

            switch (tag)
            {
                case _doorTag:
                    hit.transform.GetComponent<Door>().Trasfer(player);
                    hit.transform.GetComponent<Door>().OnInteract();
                    _screenStatus.text = hit.transform.GetComponent<Door>().InteractText;
                    break;
                case _chairTag:
                    _screenStatus.text = hit.transform.GetComponent<Chair>().InteractText;
                    hit.transform.GetComponent<Chair>().Test(_character, _animator);
                    hit.transform.GetComponent<Chair>().OnInteract();
                    break;
                case _switcherTag:
                    hit.transform.GetComponent<LightSwitcher>().Trasfer(player);
                    _screenStatus.text = hit.transform.GetComponent<LightSwitcher>().InteractText;
                    hit.transform.GetComponent<LightSwitcher>().OnInteract();
                     break;
                case _boardTag:
                    hit.transform.GetComponent<InfoBoard>().Trasfer(player);
                    _screenStatus.text = hit.transform.GetComponent<InfoBoard>().InteractText;
                    hit.transform.GetComponent<InfoBoard>().OnInteract();
                    break;
                default:
                    _screenStatus.text = "";
                    break;

            }

        }


        }


    
}