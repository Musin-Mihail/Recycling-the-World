using UnityEngine;

public class TestVelocity : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_rigidbody.velocity);
    }
}