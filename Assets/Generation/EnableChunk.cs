using UnityEngine;

public class EnableChunk : MonoBehaviour
{
    public GameObject Black;

    void Start()
    {
        if (Vector2.Distance(Vector2.zero, transform.position) < 50)
        {
            Destroy(Black);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}