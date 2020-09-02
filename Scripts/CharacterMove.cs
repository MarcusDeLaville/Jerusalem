using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private bool _isGrounded;

    [SerializeField] private bool _jump;
    private CharacterController _controller;
    private Animator _animator;
    private Vector3 _moveDirection;

    [SerializeField] private float _gravity = 30f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _isGrounded = _controller.isGrounded;
        _animator.SetBool("isGrounded", _isGrounded);

        _moveDirection.y -= _gravity * Time.deltaTime;
        _controller.Move(_moveDirection * Time.deltaTime);
    }
}
