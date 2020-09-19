using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ConfigsManager {
    
    private static ApiConfig _apiConfig;
    private static TargetIDMapping _mappingConfig;

    public static ApiConfig ApiConfig { get => _apiConfig; }
    public static TargetIDMapping MappingConfig { get => _mappingConfig; }

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
        _mappingConfig = new TargetIDMapping(
            new string[] { "120974000000", "101956300000", "120299300000", "110518600000", "104209800000", "101055600000", "204514100000" }, 
            new string[] { "Cola_Bottle", "KEZZ_Chips", "IceTea_Peach", "Konfektwaffeln", "Farmer", "Gomz", "HighProtein_Drink" });

        // TESTING
        if(_mappingConfig.ids.Length > 0)
        {

            foreach (string prod_id in _mappingConfig.ids)
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


[System.Serializable]
public class TargetIDMapping
{
    public string[] ids;
    public string[] targetImageNames;

    public TargetIDMapping(string[] ids, string[] target_imgs)
    {
        this.ids = ids;
        this.targetImageNames = target_imgs;
    }
}