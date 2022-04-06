using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class LoginDatasController : MonoBehaviour
{
    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    #region "JSON_FILE_NAME"
    public struct JsonFileName
    {
        private const string loginDataJsonFileName = "/LoginData.json";
        public string GetLoginDataJsonFileName() { return loginDataJsonFileName; }
    }
    #endregion
    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    #region "class:::LOGIN_DATAS"

    #endregion
    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

    public JsonFileName jsonFileName;
    private Datas datas;

    //==========================================================================//

    private void Start()
    {
        PrintSavedDatas();
    }

    public void PrintSavedDatas()
    {
        if (datas.LoginDatasDictionary.Count == 0) return;
        
        Debug.Log("Saved Datas :");
        
        foreach (var loginData in datas.LoginDatasDictionary)
        {
            Debug.Log($"Username : {loginData.Key}, Password : {loginData.Value}\n");
        }
    }

    public void DatasInitialize()
    {
        string fileName = jsonFileName.GetLoginDataJsonFileName();
        datas = DataManager.Instance.GetDatas<Datas>(fileName);

        SetLoginData();
    }

    //============================================================================

    public Datas GetLoginData()
    {
        return datas;
    }

    //============================================================================

    public void SetLoginData()
    {
        string fileName = jsonFileName.GetLoginDataJsonFileName();
        DataManager.Instance.SetDatas(datas, fileName);
    }

    public bool AddLoginDatas(LoginDatas signupData)
    {
        if (IsUserExist(signupData.Username))
        {
            Debug.Log($"{signupData.Username} is already exits!!");
            return false;
        }
        
        var hashedPassword = Hash.HashSHA256(signupData.Password);
        datas.LoginDatasDictionary.Add(signupData.Username, hashedPassword);
        SetLoginData();
        return true;
    }
    
    public bool IsUserExist(string username)
    {
        return datas.LoginDatasDictionary != null && datas.LoginDatasDictionary.ContainsKey(username);
    }

    public string GetPassword(string username)
    {
        return IsUserExist(username) ? datas.LoginDatasDictionary[username] : string.Empty;
    }
    
    //============================================================================
}

[Serializable]
public class LoginDatas
{
    public string Username;
    public string Password;
}

[Serializable]
public class Datas
{
    // public List<LoginDatas> loginDatasList;
    public Dictionary<string, string> LoginDatasDictionary = new Dictionary<string, string>();
}
