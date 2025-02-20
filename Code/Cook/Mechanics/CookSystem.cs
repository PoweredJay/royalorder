using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookSystem : MonoBehaviour
{
    public int reputation;
    public int gold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ModifySystem(int rep, int g)
    {
        reputation += rep;
        gold += g;
    }
    public void ModifyRep(int rep)
    {
        reputation += rep;
    }
    public void ModifyGold(int g)
    {
        gold += g;
    }
}
