using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInformationGetter : MonoBehaviour
{


    public Transform productInformationCanvas;

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
            productInformationCanvas.GetComponent<AccessInfoPrefabTexts>().SetProductInformation(p);
            productInformationCanvas.gameObject.SetActive(true);


        }
        else
        {
            Debug.LogWarning("Key = \"" + gameObject.name + "\" is not found.");
        }
    }
    

}
