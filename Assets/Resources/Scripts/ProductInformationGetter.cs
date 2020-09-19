using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInformationGetter : MonoBehaviour
{

    public Transform infoPrefab;

    private Transform productInformation;

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
            Product p = API.GetProduct(productID);


            //TODO: set texts in game objects accordingly
            productInformation = Instantiate(infoPrefab, transform);
            productInformation.GetComponent<AccessInfoPrefabTexts>().SetProductInformation(p);


        }
        else
        {
            Debug.LogWarning("Key = \"" + gameObject.name + "\" is not found.");
        }
    }

    /// <summary>
    /// Destroy the Product Information Panel
    /// </summary>
    public void DestroyProductInformation()
    {
        Destroy(productInformation);
    }

}
