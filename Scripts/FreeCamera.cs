using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    private float _speed = 2.5f;
    [SerializeField] private float _sensivity = 3f;

    private float _horInput;
    private float _verInput;
    private float _horMouseInput;
    private float _verMouseInput;

    private CharacterController _controller;
    private Quaternion _quaternion;
    private Quaternion _quaternionX;
    private Quaternion _quaternionY;

    private Vector3 _direction;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _quaternion = transform.rotation;
    }

    private void Update()
    {
        _horInput = Input.GetAxis("Horizontal");
        _verInput = Input.GetAxis("Vertical");
        _horMouseInput += Input.GetAxis("Mouse X") * _sensivity;
        _verMouseInput += Input.GetAxis("Mouse Y") * _sensivity;
        _verMouseInput = Mathf.Clamp(_verMouseInput, -90f, 90f);

        _quaternionX = Quaternion.AngleAxis(_horMouseInput, Vector3.up);
        _quaternionY = Quaternion.AngleAxis(_verMouseInput, Vector3.left);
        transform.rotation = _quaternion * _quaternionX * _quaternionY;

        _direction = transform.forward * _verInput;
        _direction += transform.right * _horInput;
        transform.position += _direction * _speed * Time.deltaTime;

    }
}
