using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

            StartCoroutine(ProductGetterCoroutine(productID));

        }
        else
        {
            Debug.LogWarning("Key = \"" + gameObject.name + "\" is not found.");
        }
    }
    
    IEnumerator ProductGetterCoroutine(string id)
    {

        Product p = new Product();
        Thread asyncThread = new Thread(() => API.GetProduct(id, out p));
        asyncThread.Start();
        while (asyncThread.IsAlive)
        {
            yield return null;
        }
        if(p != null)
        {
            productInformationCanvas.GetComponent<AccessInfoPrefabTexts>().SetProductInformation(p);
            productInformationCanvas.gameObject.SetActive(true);

        }
        else
            Debug.LogError("APP WAS NOT ABLE TO FETCH PRODUCT FROM DATABASE");
       
    }

}
