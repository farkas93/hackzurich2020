using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessAllergenesTexts : MonoBehaviour
{
    public Transform allergenePrefab;
    public Transform content;
    List<GameObject> allergenList;
    public Sprite[] allergeneIcons;

    private static Dictionary<string, Sprite> _mappingPicture;


    public void Awake()
    {
        allergenList = new List<GameObject>();

        _mappingPicture = new Dictionary<string, Sprite>(){
            {"ALLG_GLUTEN", allergeneIcons[0]},
            {"ALLG_MILCH", allergeneIcons[1]},
            {"ALLG_EIER", allergeneIcons[2]},
            {"ALLG_SOJA", allergeneIcons[3]},
            {"ALLG_NUSS", allergeneIcons[4]},
            {"ALLG_MANDELN", allergeneIcons[5]},
            {"ALLG_HASELNU", allergeneIcons[6]},
            {"ALLG_BAUMNUS", allergeneIcons[7]},
            {"ALLG_MACADAM", allergeneIcons[8]},
            {"ALLG_PISTAZI", allergeneIcons[9]},
            {"ALLG_SESAM", allergeneIcons[10]}
        };
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
            Sprite s;
            if (_mappingPicture.TryGetValue(a.code, out s))
                g.GetComponent<AccessAllergenEntries>().UpdateInfo(a, s);
            else
                g.GetComponent<AccessAllergenEntries>().UpdateInfo(a, null);
            allergenList.Add(g.gameObject);
        }
    }

}
