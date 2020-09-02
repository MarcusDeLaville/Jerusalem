using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] _charactersList;
    [SerializeField] private int _index;

    private void Start()
    {
        _index = PlayerPrefs.GetInt("Character");

        _charactersList = new GameObject[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            _charactersList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject gameObject in _charactersList)
        {
            gameObject.SetActive(false);
        }
        if (_charactersList[_index])
        {
            _charactersList[_index].SetActive(true);
        }

    }

    public void Switch(int i)
    {
        _charactersList[_index].SetActive(false);
        _index += i;

        if(_index < 0)
        {
            _index = _charactersList.Length - 1;
        }
        if(_index == _charactersList.Length)
        {
            _index = 0;
        }
        _charactersList[_index].SetActive(true);

    }
    
    public void SubmitChange()
    {
        PlayerPrefs.SetInt("Character", _index);
    }

}
