﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class AccessInfoPrefabTexts : MonoBehaviour
{
    public Text ingredientsText;
    public Text productNameText;
    public Text productPriceText;
    public Image productImg;

    public Transform allergenCanvas; 

    /// <summary>
    /// Set the UI objects text to the information stored within our product.
    /// </summary>
    /// <param name="p"> The instance of the Product to visualize</param>
    public void SetProductInformation(Product p, Sprite image)
    {
        ingredientsText.text = FilterXMLCode(p.ingredients);
        productNameText.text = p.name;
        productPriceText.text = p.price.item.price.ToString() + " " + p.price.currency;
        productImg.sprite = image;
    }

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
        if (inp != null)
        {
            string res = Regex.Replace(inp, @"<strong>", "");
            res = Regex.Replace(res, @"</strong>", "");
            return res;
        }
        else return "";
    }
}
