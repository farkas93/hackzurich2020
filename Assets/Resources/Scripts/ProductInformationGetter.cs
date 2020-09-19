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

public class ProductInformationGetter : MonoBehaviour
{


    public Transform productInformationCanvas;
    private List<Allergen> allergens;

    static string username;
    static string password;
    static string credentials;

    static string base_url;
    static string product_url;

    public static void Initialize()
    {
        username = ConfigsManager.ApiConfig.username;
        password = ConfigsManager.ApiConfig.password;
        base_url = ConfigsManager.ApiConfig.base_url;
        product_url = ConfigsManager.ApiConfig.product_url;

        credentials = CreateCredentials(username, password);
    }


    /// <summary>
    /// If we identify a product, get the information to that product from the 
    /// database and visualize it.
    /// </summary>
    public void GetProductInformation()
    {
        string productID = "";

            
        if (ConfigsManager.MappingConfig.TryGetValue(gameObject.name, out productID))
        {
            Debug.Log("For key = \""+ gameObject.name + "\", productID =" +  productID);

            StartCoroutine(GetProduct(productID));
        }
        else
        {
            Debug.LogWarning("Key = \"" + gameObject.name + "\" is not found.");
        }
    }


    /// <summary>
    /// Gets Product by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IEnumerator GetProduct(string id)
    {
        Product res_prod = new Product();

        string url = GetUrl(product_url, id);
        Debug.Log("REQUEST: " + url);
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.timeout = 30;
            request.SetRequestHeader("AUTHORIZATION", credentials);

            request.SendWebRequest();
            while (!request.isDone)
                yield return null;

            if (request.isNetworkError || request.isHttpError) // Error
            {
                Debug.Log(request.error);
            }
            else // Success
            {
                string response = request.downloadHandler.text;
                JsonSerializer serializer = new JsonSerializer();
                Debug.Log("RECEIVED: " + response);
                res_prod = JsonConvert.DeserializeObject<Product>(response);
            }
        }


        if (res_prod != null)
        {
            productInformationCanvas.GetComponent<AccessInfoPrefabTexts>().SetProductInformation(res_prod);
            allergens = res_prod.allergens;
            productInformationCanvas.gameObject.SetActive(true);
            if (allergens.Count > 0)
            {
                foreach (Allergen a in allergens)
                {
                    Debug.Log("code: " + a.code + " ; name: " + a.name);
                }
            }

        }
        else
            Debug.LogError("APP WAS NOT ABLE TO FETCH PRODUCT FROM DATABASE");
    }




    /// <summary>
    /// Builds url with saved base_url
    /// </summary>
    /// <param name="suffix"></param>
    /// <returns></returns>
    string GetUrl(params string[] suffixes)
    {
        string res = base_url;
        for (int i = 0; i < suffixes.Length; i++)
        {
            res = res + "/" + suffixes[i];
        }
        return res;
    }

    static string CreateCredentials(string username, string password)
    {
        string auth = username + ":" + password;
        auth = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;
        return auth;
    }

}
