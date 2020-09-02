using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{

    [Header("Inputs")]
    private float _hor;
    private float _ver;
    [SerializeField]private KeyCode _jumpBtn = KeyCode.Space;
    [SerializeField]private KeyCode _runBtn = KeyCode.LeftShift;

    [Header("Player property")]
    [Range(0.1f, 2.0f)]
    [SerializeField] private float _walkSpeed = 1.5f;
    [Range(2.1f, 4.0f)]
    [SerializeField] private float _runSpeed = 2.5f;

    [SerializeField] private float _jumpForse = 8f;
    [SerializeField] private float _gravity = 20.0F;
    [SerializeField] private float _smooth = 0.1f;
    private float _smoothVelocity;

    private float _globalSpeed;
    private float _horMouseInput;
    private Animator _animator;
    private Transform _body;
    private PhotonView _photonView;
    private CharacterController _controller;
    private Vector3 _moveDirection = Vector3.zero;
    [SerializeField] private Transform _cameraPos;
    public bool _frezeeMoving;
    private float _horAnim;
    private float _vecAnim;
    private bool _isRotation;

    public PlayerMove instance;

    public void Start()
    {
        instance = this;

        _photonView = GetComponent<PhotonView>();  //инициализация Photon
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _body = GetComponent<Transform>();
    }

    public void Update()
    {
        if (!_photonView.IsMine) return;
        
    
        if (!PlayerUI._frezee && _controller.isGrounded) 
        {
            if (!_frezeeMoving)
            {

            _hor = Input.GetAxisRaw("Horizontal");
            _ver = Input.GetAxisRaw("Vertical");
            _horMouseInput = Input.GetAxis("Mouse X");

            _globalSpeed = Input.GetKey(_runBtn) ? _runSpeed : _walkSpeed;
            _moveDirection = new Vector3(_hor, 0, _ver);
            _moveDirection *= _globalSpeed;
            if (ChangePOV.isThirdCamera)
            {

            if (_moveDirection.magnitude >= 0.1f)
                {
                float _targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg + _cameraPos.eulerAngles.y;
                float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _smoothVelocity, _smooth);
                transform.rotation = Quaternion.Euler(0f, _angle, 0f);
                _moveDirection = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
                _controller.Move(_moveDirection.normalized * _globalSpeed * Time.deltaTime);
                }            
            }
            else
                {
                _moveDirection = transform.TransformDirection(_moveDirection);
                _body.localEulerAngles += new Vector3(0, _horMouseInput, 0) * PlayerCamera.Sensitivity;
                }

            if(Input.GetKeyUp(_jumpBtn))
                {
                Jump();
                }
            
            }
                 
        }
        _moveDirection.y -= _gravity * Time.deltaTime;
        _controller.Move(_moveDirection * Time.deltaTime);

        SetCharacterAnimations();
    }

    private void Jump()
    {    
        _animator.SetTrigger("Jump");
        _moveDirection.y = _jumpForse;   
    }

    private void SetCharacterAnimations()
    {
        if (!_animator)
            return;

        if (_horMouseInput != 0 && _hor == 0 && _ver == 0)
        {
            _isRotation = true;
        }
        else
        {
            _isRotation = false;
        }

        if (_controller.isGrounded)
        {
            if (_hor == 0 && _ver == 0)
            {
                _horAnim = Mathf.Lerp(_horAnim, 0, 5 * Time.deltaTime);
                _vecAnim = Mathf.Lerp(_vecAnim, 0, 5 * Time.deltaTime);
            }
            else
            {
                if (_globalSpeed == _walkSpeed)
                {
                 _horAnim = Mathf.Lerp(_horAnim, 1 * _hor, 5 * Time.deltaTime);
                _vecAnim = Mathf.Lerp(_vecAnim, 1 * _ver, 5 * Time.deltaTime);
                }
                else
                {
                 _horAnim = Mathf.Lerp(_horAnim, 2 * _hor, 5 * Time.deltaTime);
                _vecAnim = Mathf.Lerp(_vecAnim, 2 * _ver, 5 * Time.deltaTime);
                }
                   
            }
        }           
        _animator.SetFloat("Horizontal", _horAnim);
        _animator.SetFloat("Vertical", _vecAnim);
        _animator.SetBool("isGrounded", _controller.isGrounded);
        _animator.SetBool("isRotation", _isRotation);
    }

}
