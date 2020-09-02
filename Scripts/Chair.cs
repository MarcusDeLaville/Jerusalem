using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : InteractableItemsBase
{

    public bool _isSitting = false;

    private Transform _transform;
    private GameObject _character;
    private Animator _animator;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    public void Test(GameObject character, Animator animator)
    {
        _character = character;
        _animator = animator;
    }

    public override void OnInteract()
    {
     InteractText = $"Нажмите <color=#0b979c>{InteractKey}</color> чтобы "; 
     InteractText += _isSitting ? "встать" : "присесть";

    if (Input.GetKeyDown(InteractKey))
    {
        _isSitting = !_isSitting;
        _animator.SetBool("isSitting", _isSitting);

        if (_isSitting)
        {    
        _character.transform.position = new Vector3(_transform.transform.position.x, _transform.transform.position.y, (_transform.transform.position.z + -0.1f));
        _character.transform.localEulerAngles = _transform.transform.localEulerAngles + new Vector3(0, 90, 0);

        if(_character.transform.position == new Vector3(_transform.transform.position.x, _transform.transform.position.y, (_transform.transform.position.z + -0.1f)))
        {
        _character.GetComponent<PlayerMove>().instance._frezeeMoving = true;
        }
        
        }
        else
        {
        _character.GetComponent<PlayerMove>().instance._frezeeMoving = false;
        }
    }

    }
}
