using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Product class
/// </summary>
[System.Serializable]
public class Product
{
    public string id;
    public string language;

    public string name;
    public string product_name;
    public string product_type;

    public string slug;
    public string short_description;

    public string ingredients;

    public PriceSummary price;
}


/// <summary>
/// Price Info Wrapper class
/// </summary>
[System.Serializable]
public class PriceSummary
{
    public string valid_from;
    public string valid_to;

    public string currency;
    public string source;

    public Price item;
}

/// <summary>
/// Price class
/// </summary>
[System.Serializable]
public class Price
{
    public float price;
    public int quantity;

    public string unit;
    public bool display_quantity;
    
}