using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoginSystem : MonoBehaviour
{
 
    public string nickname = "gopyt";
    public string password;
    private bool isLogined;
    private bool isNotLogin;

    [SerializeField]private GameObject launcher;
    public static LoginSystem instanse;

    [SerializeField]private Text screenStatus;
    [SerializeField]private bool dissableOnStart;

    public InputField InputerLogin;
    public InputField InputerPassword;
     
    public Text StatusText = null;
    public GameObject LoginPanel;

    void Start()
    {
        instanse = this;

        isLogined = false;
        var login = new InputField.SubmitEvent();
        var inputedPassword = new InputField.SubmitEvent();
		

        login.AddListener(SubmitName);
        inputedPassword.AddListener(SubmitPassword);
        InputerLogin.onEndEdit = login;
        InputerPassword.onEndEdit = inputedPassword;
        if(!dissableOnStart)
        {
            if(isLogined == false && LoginPanel.activeSelf == false)
            {
            LoginPanel.SetActive(true);
            }
        }
        

    }
    private void SubmitName(string login)
    {
        switch (login)
        {
            case "admin":
                nickname = "Lector";
                password = "12300";
                
                break;
            case "profile1":
                nickname = "Penitractor";
                password = "12389";
               
                break;
            case "profile2":
                nickname = "Torpeda";
                password = "12312";
               
                break;
            case "profile3":
                nickname = "Monkey3000";
                password = "12367";
                
                break;
            case "profile4": 
                nickname = "YourFather";
                password = "12345";
               
                break;
            default:
                nickname = "Guest";
                break;
        }
        
    }
    private void SubmitPassword(string pass)
    {
        if (pass == password) 
        {   
            launcher.SetActive(true);
            isLogined = true;      
        }
        if (!(pass == password))
        {           
            isLogined = false;
            isNotLogin = true;
        }

    }
    public void EnterButton()
    {
        
        if (isLogined)
        {
            screenStatus.text = "<color=green>Успешно</color>";
            StartCoroutine(closePanel());
        }
        else if (isNotLogin)
        {
            screenStatus.text = "<color=red>Логин или пароль введены не верно</color>";
        }
            
    }
    
    IEnumerator closePanel()
    {
        yield return new WaitForSeconds(5.0f);
        LoginPanel.SetActive(false);
        
    }


}
