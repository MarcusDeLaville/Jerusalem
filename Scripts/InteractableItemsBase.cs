using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemsBase : MonoBehaviour
{
    public string InteractText = $"Нажмите E для взаимодествия";
    public KeyCode InteractKey = KeyCode.E;

    public virtual void OnInteract()
    {

    }
}
