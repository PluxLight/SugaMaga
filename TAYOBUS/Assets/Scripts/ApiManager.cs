using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using MiniJSON;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using System.Threading.Tasks;

public class ApiManager : MonoBehaviour
{
    // If Game Start or Login, Token Update
    // example is expired token
    private string ACCESS_TOKEN = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjFlOTczZWUwZTE2ZjdlZWY0ZjkyMWQ1MGRjNjFkNzBiMmVmZWZjMTkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vdGF5b3ZlcnNlLTQ5YWYxIiwiYXVkIjoidGF5b3ZlcnNlLTQ5YWYxIiwiYXV0aF90aW1lIjoxNjc5MjczNDQwLCJ1c2VyX2lkIjoiMU1ObEcwUUF2YVhBRmZZZ2Q0cDlPeWpwR0pxMiIsInN1YiI6IjFNTmxHMFFBdmFYQUZmWWdkNHA5T3lqcEdKcTIiLCJpYXQiOjE2NzkyNzM0NDAsImV4cCI6MTY3OTI3NzA0MCwiZW1haWwiOiJkZW1vMDFAZW1haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJmaXJlYmFzZSI6eyJpZGVudGl0aWVzIjp7ImVtYWlsIjpbImRlbW8wMUBlbWFpbC5jb20iXX0sInNpZ25faW5fcHJvdmlkZXIiOiJwYXNzd29yZCJ9fQ.A2IxXlmilbAIAjhma_FaaLdtznnPod11VlXGRa07w9IOd0iyRSzl0nRRS-UcMHZpcCKOFMVxe6lzGH2gfzT4mwC1tIIRdEfSOl9b4Nq8ZPgPPTrrx8ub4HJPPOu27QtMd-HcMImpteuK4Q0HlosSVho4PG_fzP3PU6E47JO1vZagKfN9cMS5MAYSe2GYGdIxChkz6j0Hm51V27_FK70t-d1VUOG3DfmVtsZoHd0JQYo5ra3LifCau3Do08p--oIFDS45jfesSLrCbdjobbMTtGen7WiVWZzPJyuhegffHZm7Tzj2fhE_-n6Uf8iyZHgVxsqPxCxNzf5snTpsVugOKw";
    private string REFRESH_TOKEN = "APJWN8epyB1N-XKKkiqV3B0HimZNzfbDEfkI8FldGXv14Y0JTw-pPlwKfScPkKm_zbq3ugjt6o-in27EQ4_wv27jG8h1hC6A_LaiOXbDbYhDCIIM3kak1vLqFtyJlWpaSdQMVwapQQ7b7d5fTwp6wnjnpyNuxMedwvIUyOglLgaWZlhRdZzNCgKTs_a3eN9lMpRkg2ttFm-B1mq575uf3M2Rh6EvkmTUWw";
    private string GOOGLE_KEY = "AIzaSyAbrcuxBD_VyzQDSlAJfxPUZOfsx8wfNy0";
    private string SERVER_URL = "http://localhost:18081/api/";
    
    static ApiManager instance;
    static GameObject container;
    static GameObject Container {
        get { return container; }
    }


    public static ApiManager Instance
    {
        get
        {
            if ( !instance )
            {
                container = new GameObject();
                container.name = "ApiManager";
                instance = container.AddComponent( typeof(ApiManager) ) as ApiManager;
            }

            return instance;
        }
    }


    public void GET(string url, string json, Action<UnityWebRequest> callback)
    {
        if (json != null)
        {
            var dict = Json.Deserialize(json) as Dictionary<string, object>;
            List<string> keys = new List<string>(dict.Keys);

            string temp = "?";

            foreach (string key in keys)
            {
                temp += key + "=" + dict[key] + "&";
            }
            temp = temp.Substring(0, temp.Length - 1);

            url += temp;
        }

        if ( !Verify() )
        {
            callback(null);
        }

        UnityWebRequest request = UnityWebRequest.Get(SERVER_URL + url);
        request.SetRequestHeader("accessToken", ACCESS_TOKEN);

        StartCoroutine( WaitRequest(request, callback) );
    }

    public void POST(string url, string json, Action<UnityWebRequest> callback)
    {
        /*if (!Verify())
        {
            callback(null);
        }*/

        UnityWebRequest request = UnityWebRequest.Post(SERVER_URL + url, json);
        /*request.SetRequestHeader("accessToken", ACCESS_TOKEN);*/
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(WaitRequest(request, callback));
    }

    public void PUT(string url, string json, Action<UnityWebRequest> callback)
    {
        byte[] data = null;

        if (json != null)
        {
            data = System.Text.Encoding.UTF8.GetBytes(json);
        }

        if (!Verify())
        {
            callback(null);
        }

        UnityWebRequest request = UnityWebRequest.Put(SERVER_URL + url, data);
        request.SetRequestHeader("accessToken", ACCESS_TOKEN);

        StartCoroutine(WaitRequest(request, callback));
    }


    public IEnumerator WaitRequest(UnityWebRequest request, Action<UnityWebRequest> callback)
    {
        yield return request.SendWebRequest();
        callback(request);
    }


    public bool Verify()
    {
        Debug.Log("Start Verify");

        bool verifyResult = true;
        UnityWebRequest request = UnityWebRequest.Get(SERVER_URL + "sign/verify");
        request.SetRequestHeader("accessToken", ACCESS_TOKEN);

        StartCoroutine( WaitRequest(request, delegate (UnityWebRequest result)
        {
            if (result.responseCode == 403)
            {
                Debug.Log("Refresh Forbidden");
                
                if (!RefreshToken())
                {
                    verifyResult = false;
                }
            }
            else if (request.responseCode == 400)
            {
                Debug.Log("Refresh Bad Request");
                verifyResult = false;
            }

        } ));

        Debug.Log("End Verify");

        return verifyResult;
    }


    public bool RefreshToken()
    {
        bool refreshResult = true;

        Debug.Log("Start RefreshToken");

        WWWForm form = new WWWForm();
        form.AddField("grant_type", "refresh_token");
        form.AddField("refresh_token", REFRESH_TOKEN);
        string refreshUrl = "https://securetoken.googleapis.com/v1/token?key=" + GOOGLE_KEY;

        UnityWebRequest request = UnityWebRequest.Post(refreshUrl, form);
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

        var operation = request.SendWebRequest();

        Debug.Log("End RefreshToken");

        return refreshResult;
    }

}