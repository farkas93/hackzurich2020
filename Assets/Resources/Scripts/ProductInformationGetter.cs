using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInformationGetter : MonoBehaviour
{

    public Transform infoPrefab;

    private Transform productInformation;

    public void getProductInformation()
    {
        string productID = "";

            
        if (ConfigsManager.MappingConfig.TryGetValue(gameObject.name, out productID))
        {
            Debug.Log("For key = \""+ gameObject.name + "\", productID =" +  productID);
            Product p = API.GetProduct(productID);


            //TODO: set texts in game objects accordingly
            Instantiate(infoPrefab, this.transform);
        }
        else
        {
            Debug.LogWarning("Key = \"" + gameObject.name + "\" is not found.");
        }
    }

    public void destroyProductInformation()
    {
        Destroy(productInformation);
    }

}
