using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    
    [Header("Login Input")]
    [SerializeField] private InputField usernameInputField;
    [SerializeField] private InputField passwordInputField;
    [SerializeField] private Button loginButton;

    [Header("Login Output")]
    [SerializeField] private GameObject loginOutputPanel;
    [SerializeField] private Text loginOutputText;

    private void Awake()
    {
        loginButton.onClick.AddListener(Login);
        Hide();
    }

    private void OnEnable()
    {
        loginOutputPanel.SetActive(false);
    }

    private void Login()
    {
        var username = usernameInputField.text.ToLower();
        var password = passwordInputField.text;

        var isUserExist = DataManager.Instance.LoginDatasController.IsUserExist(username);
        if (!isUserExist)
        {
            loginOutputText.text = "Login Failed(User doesn't exits!!)!!";
        }
        else
        {
            var realHashedPass = DataManager.Instance.LoginDatasController.GetPassword(username);
            var hashedPassword = Hash.HashSHA256(password);
            loginOutputText.text = hashedPassword.Equals(realHashedPass) ? "Login Success!!" : "Login Failed (wrong password) !!";
        }
        
        loginOutputPanel.SetActive(true);

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
