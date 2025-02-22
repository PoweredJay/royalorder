using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSelect : MonoBehaviour
{
    public List<GameObject> simple;
    public List<GameObject> royal;
    public List<GameObject> leafy;
    public List<GameObject> hearty;
    public List<GameObject> beverage;
    // Start is called before the first frame update
    public List<GameObject> Select(ItemType type)
    {
        if (type == ItemType.SIMPLE)
        {
            return simple;
        }
        else
        {
            return simple;
        }
    }
}
