using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace EmotionWheel.Scripts
{
    public class EmotionSelector : MonoBehaviour
    {
        [SerializeField] private KeyCode _selectorKey = KeyCode.M;
        [SerializeField] private GameObject _sectorPanel;
        [SerializeField] private string[] _animationsTrigers = new string[12];
        [SerializeField] private Animator _animator;

        private string _trigerName;


        private void Start()
        {
            DisableSelector();        
        }

        private void Update()
        {
            if (Input.GetKeyDown(_selectorKey))
            {
                EnableSelector();
                
            }
            if (Input.GetKeyUp(_selectorKey))
            {
                DisableSelector();
            }
        }
       
        private void EnableSelector()
        {
            if(_sectorPanel != null)
            _sectorPanel.SetActive(true);
            PlayerUI._frezee = true;
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }

        private void DisableSelector()
        {
            if (_sectorPanel != null)
           _sectorPanel.SetActive(false);
            PlayerUI._frezee = false;

            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }

        public void SwitchEmotion(int index)
        {
            _trigerName = _animationsTrigers[index];
            _animator.SetTrigger(_trigerName);
            DisableSelector();
        }
    }
}

