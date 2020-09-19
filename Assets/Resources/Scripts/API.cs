using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public static class API
{
    static string username;
    static string password;
    static CredentialCache credentials;

    static string base_url;
    static string product_url;

    public static void Initialize()
    {
        username = ConfigsManager.ApiConfig.username;
        password = ConfigsManager.ApiConfig.password;
        base_url = ConfigsManager.ApiConfig.base_url;
        product_url = ConfigsManager.ApiConfig.product_url;

        credentials = new CredentialCache
        {
            { new Uri(base_url), "Basic", new NetworkCredential(username, password) }
        };
    }

    /// <summary>
    /// Gets Product by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static void GetProduct(string id, out Product res_prod)
    {
        string url = GetUrl(product_url, id);
        string result = DownloadString(url);
        JsonSerializer serializer = new JsonSerializer();
        res_prod = JsonConvert.DeserializeObject<Product>(result);
    }

    /// <summary>
    /// Download String with Webclient from given URL
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    static string DownloadString(string url)
    {
        WebClient WebClient = new WebClient
        {
            Credentials = credentials
        };
        string ret_val = "";
        try
        {
            ret_val = WebClient.DownloadString(new Uri(url));
        }
        catch
        {
            Debug.LogError("CONNTECTION TO SERVER FAILED");
        }
        return ret_val;
    }

    /// <summary>
    /// Builds url with saved base_url
    /// </summary>
    /// <param name="suffix"></param>
    /// <returns></returns>
    static string GetUrl(params string[] suffixes)
    {
        string res = base_url;
        for (int i = 0; i < suffixes.Length; i++)
        {
            res = res + "/" + suffixes[i];
        }
        return res;
    }

}




