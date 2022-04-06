using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DataManager.Instance.LoginDatasController.SetLoginData();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DataManager.Instance.LoginDatasController.PrintSavedDatas();
        }

    }
}
