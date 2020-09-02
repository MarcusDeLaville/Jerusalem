using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCloser : MonoBehaviour
{

    public void OpenPanel(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void ClosePanel(GameObject gameObject)
    {
        gameObject.SetActive(false);
        PlayerUI._frezee = false;
    }

}
