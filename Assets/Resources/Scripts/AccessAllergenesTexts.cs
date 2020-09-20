using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessAllergenesTexts : MonoBehaviour
{
    public Transform allergenePrefab;
    public Transform content;
    List<GameObject> allergenList;
    public Sprite[] allergeneIcons;


    public void Awake()
    {
        allergenList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    public void SetAllergeneList(List<Allergen> allergens)
    {
        if(allergenList.Count > 0)
        {
            foreach (GameObject o in allergenList)
            {
                Destroy(o);
            }
        }

        foreach(Allergen a in allergens)
        {
            Transform g = Instantiate(allergenePrefab, content);
            Sprite s = null;
            g.GetComponent<AccessAllergenEntries>().UpdateInfo(a, s);
            allergenList.Add(g.gameObject);
        }
    }

}
