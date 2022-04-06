using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
[DefaultExecutionOrder(-1)]
public class DataManager : MonoBehaviour
{
    public LoginDatasController LoginDatasController { get; set; }

    public static DataManager Instance;

    //==========================================================================//
    //--------------------------------------------------------------------------//
    //==========================================================================//

    private void Awake()
    {
        Instance = this;
        DataGameObjectAdded();
    }

    //============================================================================

    /// <summary>
    /// Dataların oldugu scriptleri componenet olarak ekler
    /// </summary>
    private void DataGameObjectAdded()
    {
        LoginDatasController = gameObject.AddComponent<LoginDatasController>();
        LoginDatasController.DatasInitialize();
    }

    //============================================================================

    /// <summary>
    /// Json dosyasını stringe parse eder
    /// </summary>
    /// <param name="fileName"> json dosyasının adı </param>
    /// <returns></returns>
    private string GetJsonString(string fileName)
    {
        string path = GetPath(fileName);

#if UNITY_EDITOR
        if (!File.Exists(path))
        {
            Debug.LogWarning(fileName + " Not Found");
            return "";
        }
        else
        {
            return File.ReadAllText(path);
        }
#elif UNITY_ANDROID
        WWW reader = new WWW(path);

        if (reader != null)
	    {
            while (!reader.isDone) { }

            return reader.text;
	    }
        else
        {
            return "";
        }
#elif UNITY_IOS
        /* ???? */
#else
        return "";
#endif

    }

    //============================================================================

    /// <summary>
    /// Datayı okunabilir bir hale getirmek için class objesine yazar
    /// </summary>
    /// <typeparam name="T"> Data tipi </typeparam>
    /// <param name="fileName"> json dosyasının ismi </param>
    /// <returns></returns>
    public T GetDatas<T>(string fileName) where T : class
    {
        string jsonString = GetJsonString(fileName);

        if (jsonString == null)
        {
            return default;
        }
        else
        {
            object resultValue = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);

            return (T) resultValue;
            // if (resultValue != null)
            // {
            //     return (T)Convert.ChangeType(resultValue, typeof(T));
            // }
            // else
            // {
            //     return default;
            // }
        }
    }

    //============================================================================

    /// <summary>
    /// Datayı setler
    /// </summary>
    /// <param name="obj"> Data tipi </param>
    /// <param name="fileName"> Json dosyasının ismi </param>
    public void SetDatas(object obj, string fileName)
    {
        if (obj != null)
        {
            string newJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            string path = GetPath(fileName);

            if (File.Exists(path))
            {
                File.WriteAllText(path, newJsonData);
            }
        }
    }

    //============================================================================

    /// <summary>
    /// Dosyanın pathini bulur
    /// </summary>
    /// <param name="jsonFileName"> Dosyanın ismi </param>
    /// <returns></returns>
    private string GetPath(string jsonFileName)
    {
        return Application.streamingAssetsPath + jsonFileName;
    }

    //============================================================================
}
