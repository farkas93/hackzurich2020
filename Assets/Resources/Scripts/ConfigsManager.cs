﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ConfigsManager {
    
    private static ApiConfig _apiConfig;
    private static Dictionary<string, string> _mappingConfig;

    public static ApiConfig ApiConfig { get => _apiConfig; }
    public static Dictionary<string, string> MappingConfig { get => _mappingConfig; }

    /// <summary>
    /// Load API config from json file
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadConfigFiles()
    {
        Debug.Log("Loading api config");
        var Result = (TextAsset)Resources.Load("Configuration/configuration_api");
        Debug.Log(JsonUtility.FromJson<ApiConfig>(Result.text));
        _apiConfig = JsonUtility.FromJson<ApiConfig>(Result.text);


        API.Initialize();


        Result = (TextAsset)Resources.Load("Configuration/target_id_mapping");
        Debug.Log(JsonUtility.FromJson<TargetIDMapping>(Result.text));
        _mappingConfig = new Dictionary<string, string>(){
            {"Cola_Bottle", "120974000000"},
            {"KEZZ_Chips", "101956300000"},
            {"IceTea_Peach", "120299300000"},
            {"Konfektwaffeln", "110518600000"},
            {"Farmer", "104209800000"},
            {"Gomz", "101055600000"},
            {"HighProtein_Drink", "204514100000"}
        };
    
        // TESTING
        if(_mappingConfig.Count > 0)
        {

            foreach (string prod_id in _mappingConfig.Values)
            {

                Product p = API.GetProduct(prod_id);
                Debug.Log(p.name);
                Debug.Log(p.ingredients);
                Debug.Log(p.price.item.price);
            }
        }
        else
        {
            Debug.LogWarning("YOU DO NOT HAVE ANY MAPPINGS!");
        }


    }


}

[System.Serializable]
public class ApiConfig
{
    public string username;
    public string password;
    public string base_url;
    public string product_url;
    public string discount_url;
}
