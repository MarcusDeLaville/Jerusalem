using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Vector3 _cameraOffset;

    [Header("Other Camera Settings")]
    public static float Sensitivity = 5f;

    private Camera _camera;
    private PhotonView _photonView;
    private Transform _cameraHelper;
    private Animator _animator;

    [SerializeField] private float _headMinY = -40f; // ограничение угла для головы
    [SerializeField] private float _headMaxY = 40f;
    private float _rotationY;
    private float _rotationX; 
    
    [SerializeField] private Transform _head;
    [SerializeField] private PlayerMove _playerMove;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _camera = GetComponent<Camera>();
        _animator = GetComponentInParent<Animator>();
        _head = GetComponent<Transform>();
        
        ADD_CameraPositionHelper();
        if (!_photonView.IsMine)
        {
            _camera.enabled = false;   
        }
    }

    private void ADD_CameraPositionHelper()
    {
        _cameraHelper = new GameObject().transform;
        _cameraHelper.name = "_cameraHelper";
        _cameraHelper.SetParent(_animator.GetBoneTransform(HumanBodyBones.Head));
        _cameraHelper.localPosition = Vector3.zero;
    }

    private void Update()
    {
        if (!PlayerUI._frezee)
        {
            SetCameraHelperPosition();
            _rotationY += Input.GetAxis("Mouse Y") * Sensitivity;
            _rotationX += Input.GetAxis("Mouse X") * Sensitivity;
            _rotationY = Mathf.Clamp(_rotationY, _headMinY, _headMaxY);
            if(_playerMove._frezeeMoving == true)
            {
            _head.localEulerAngles = new Vector3(-_rotationY, _rotationX, 0);
            }
            else
            {
             _head.localEulerAngles = new Vector3(-_rotationY, 0, 0);
            }
            
        }
    }

    private void SetCameraHelperPosition()
    {
        if (!_animator)
            return;

        _cameraHelper.localPosition = _cameraOffset;
        transform.position = _cameraHelper.position;
    }     

   
}
