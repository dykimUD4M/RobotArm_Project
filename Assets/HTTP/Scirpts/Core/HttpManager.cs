using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Http.Loader
{
    public class HttpManager
    {
        public static async void GetDataString(string url, Action<string> callback)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            await www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.error);
            else
                Debug.Log(www.downloadHandler.text);// Show results as text

            string data = www.downloadHandler.text;
            callback?.Invoke(data);

        }
        public static async void GetDataByte(string url, Action<byte[]> callback)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            await www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.error);
            else
                Debug.Log(www.downloadHandler.text);// Show results as text

            byte[] data = www.downloadHandler.data;
            callback?.Invoke(data);

        }
        public static async void Post(string url, List<IMultipartFormSection> formData)
        {
            UnityWebRequest www = UnityWebRequest.Post(url, formData);
            await www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.error);
            else
                Debug.Log("Form upload complete!");
        }
    }
}

