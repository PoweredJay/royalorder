using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerHandler : MonoBehaviour
{
    public GameObject self;
    public GameObject customerTemplate;
    public GameObject chairs;
    [SerializeField] private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > 5)
        {
            for (int i = 0; i < chairs.transform.childCount; i++)
            {
                if (!chairs.transform.GetChild(i).GetComponent<Chair>().occupied)
                {
                    Instantiate(customerTemplate, self.transform);
                    self.transform.GetChild(self.transform.childCount - 1).GetComponent<CustomerPathfind>().target = chairs.transform.GetChild(i);
                    self.transform.GetChild(self.transform.childCount - 1).GetComponent<CustomerMechanics>().chair = chairs.transform.GetChild(i).GetComponent<Chair>();
                    chairs.transform.GetChild(i).GetComponent<Chair>().occupied = true;
                    break;
                }
            }
            timer = 0;
        }
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.GetComponent<CustomerMechanics>().currentState == CustomerState.LEAVE && this.gameObject.transform.GetChild(i).transform.position.x < -10)
            {
                this.transform.GetChild(i).GetComponent<CustomerMechanics>().chair.occupied = false;
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
    }
}
