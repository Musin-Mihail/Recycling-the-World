using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    int layerMask = 1 << 8;
    int layerMask2 = 1 << 10;
    int layerMask3;
    public float DistancePlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        layerMask3 = layerMask | layerMask2;
    }

    void Update()
    {
        DistancePlayer = Vector3.Distance(transform.position, PlayerGlobal.Player.transform.position);

        if (DistancePlayer < 10)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, PlayerGlobal.Player.transform.position - transform.position, 20, layerMask3);
            if (hit && hit.collider.name == "Body" && Global.EnergyCount > 0)
            {
                rb.transform.position = Vector3.MoveTowards(transform.position, PlayerGlobal.Player.transform.position, 0.5f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Laser")
        {
            Destroy(gameObject);
        }
        else if (other.name == "Collector" && Global.EnergyCount > 0)
        {
            Destroy(gameObject);
            Global.EnergyCount -= 100;
        }
    }
}