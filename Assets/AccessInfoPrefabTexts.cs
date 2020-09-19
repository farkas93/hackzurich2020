using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class AccessInfoPrefabTexts : MonoBehaviour
{
    public Text ingredientsText;
    public Text productNameText;
    public Text productPriceText;

    public Transform allergenCanvas; 

    /// <summary>
    /// Set the UI objects text to the information stored within our product.
    /// </summary>
    /// <param name="p"> The instance of the Product to visualize</param>
    public void SetProductInformation(Product p)
    {
        ingredientsText.text = FilterXMLCode(p.ingredients);
        productNameText.text = p.name;
        productPriceText.text = p.price.item.price.ToString() + " " + p.price.currency;
    }

<<<<<<< HEAD
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    public void ToggleAllegernCanvas()
    {
        bool active = allergenCanvas.gameObject.activeSelf;
        allergenCanvas.gameObject.SetActive(!active);
    }

    /// <summary>
    /// Filters <strong>BOLD</strong> XML statements from strings
    /// </summary>
    /// <param name="inp"></param>
    /// <returns></returns>
    private string FilterXMLCode(string inp)
    {
        string res = Regex.Replace(inp, @"<strong>", "");
        res = Regex.Replace(res, @"</strong>", "");
        return res;
    }
    
=======
>>>>>>> d752e05a4ff63538bedd469ff608d61155a5fec9
}
