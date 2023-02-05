using UnityEngine;

public class TopBlock : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Drill")
        {
            Destroy(gameObject);
        }
    }
}