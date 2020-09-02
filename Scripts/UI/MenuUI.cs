using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void ShitchPanel(GameObject gameObject)
    {  
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf == true)
            _audioSource.Pause();
        else
            _audioSource.Play();
    }
}
