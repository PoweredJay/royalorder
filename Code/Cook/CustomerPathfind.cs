using Pathfinding;
using UnityEngine;

public class CustomerPathfind : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private float moveSpeed;
    [SerializeField] public Transform target;
    bool arrived;
    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        path.maxSpeed = moveSpeed;
        path.destination = target.position;
        if (!arrived && Vector2.Distance(this.GetComponent<Transform>().position, target.position) <= 0.015)
        {
            arrived = true;
        }
    }
}
