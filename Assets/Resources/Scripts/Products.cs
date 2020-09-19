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

    public string slug;
    public string short_description;

    public string ingredients;

    public PriceSummary price;
    public List<Allergen> allergens;
}



/// <summary>
/// Price Info Wrapper class
/// </summary>
[System.Serializable]
public class Allergen
{
    public string code;
    public string name;

    public string contamination_code;
    public string contamination;
    
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
    public bool varying_quantity;
    public string display_quantity;


}