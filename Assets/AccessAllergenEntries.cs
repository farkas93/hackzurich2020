using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessAllergenEntries : MonoBehaviour
{
    public Image image;
    public Text text;

    public void UpdateInfo(Allergen a, Sprite s)
    {
        image.sprite = s;
        text.text = a.name;
    }
}
