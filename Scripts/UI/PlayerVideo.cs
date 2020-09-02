using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;


    public void SwitchСondition()
    {
        if (_videoPlayer.isPlaying)
            _videoPlayer.Pause();
        else
            _videoPlayer.Play();
    }
}
