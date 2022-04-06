using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignupPanel : MonoBehaviour
{
    [Header("Signup Input")]
    [SerializeField] private InputField usernameInputField;
    [SerializeField] private InputField passwordInputField;
    [SerializeField] private Button signupButton;

    [Header("Signup Output")]
    [SerializeField] private GameObject signupOutputPanel;
    [SerializeField] private Text signupOutputText;
   
    private void Awake()
    {
        signupButton.onClick.AddListener(Signup);
        Hide();
    }
    
    private void OnEnable()
    {
        signupOutputPanel.SetActive(false);
    }

    private void Signup()
    {
        var loginDatas = new LoginDatas
        {
            Username = usernameInputField.text.ToLower(),
            Password = passwordInputField.text
        };

        var isUsernameExist = DataManager.Instance.LoginDatasController.IsUserExist(loginDatas.Username);

        if (isUsernameExist)
        {
            signupOutputText.text = "Signup Failed (Username already exist) !!";
        }
        else
        {
            var isAdded = DataManager.Instance.LoginDatasController.AddLoginDatas(loginDatas);
            if(isAdded) signupOutputText.text = "Signup Success!!";
        }
        
        signupOutputPanel.SetActive(true);
        // Debug.Log($"Username : {loginDatas.Username}, Password : {loginDatas.Password}\n");
        ResetPlaceHolder();
    }

    private void ResetPlaceHolder()
    {
        usernameInputField.text = string.Empty;
        passwordInputField.text = string.Empty;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
