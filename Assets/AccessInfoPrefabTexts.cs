﻿using System.Collections;
using System.Collections.Generic;
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
        ingredientsText.text = p.ingredients;
        productNameText.text = p.name;
        productPriceText.text = p.price.item.price.ToString() + " " + p.price.currency;
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

}
