using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePOV : MonoBehaviour
{
    [SerializeField] private GameObject camFPS;
    [SerializeField] private GameObject camTPS;

    public static bool isThirdCamera = false;

    private void Start()
    {
            SetFPS();
    }

    public void SetFPS()
    {
        isThirdCamera = false;
        camTPS.SetActive(false);
        camFPS.SetActive(true);
    }

    public void SetTPS()
    {
        isThirdCamera = true;
        camTPS.SetActive(true);
        camFPS.SetActive(false);
    }
}

