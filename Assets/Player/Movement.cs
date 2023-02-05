using UnityEngine;

public class Movement : MonoBehaviour
{
    //Перемещение персонажем
    public float speed;
    Rigidbody2D m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Global.EnergyCount <= 0)
        {
            speed = 1;
        }
        else
        {
            speed = 8;
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            m_Rigidbody.velocity = new Vector2(0, 0);
        }
        else
        {
            if (Global.EnergyCount >= 0)
            {
                Global.EnergyCount--;
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                {
                    m_Rigidbody.velocity = new Vector2(-speed, 0);
                }
                else
                {
                    m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x - speed, m_Rigidbody.velocity.y);
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                {
                    m_Rigidbody.velocity = new Vector2(+speed, 0);
                }
                else
                {
                    m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x + speed, m_Rigidbody.velocity.y);
                }
            }

            if (Input.GetKey(KeyCode.W))
            {
                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                {
                    m_Rigidbody.velocity = new Vector2(0, +speed);
                }
                else
                {
                    m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y + speed);
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                {
                    m_Rigidbody.velocity = new Vector2(0, -speed);
                }
                else
                {
                    m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y - speed);
                }
            }

//Ограничение ускорения
            if (m_Rigidbody.velocity.x > speed)
            {
                m_Rigidbody.velocity = new Vector3(speed, m_Rigidbody.velocity.y);
            }
            else if (m_Rigidbody.velocity.x < -speed)
            {
                m_Rigidbody.velocity = new Vector3(-speed, m_Rigidbody.velocity.y);
            }

            if (m_Rigidbody.velocity.y > speed)
            {
                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, speed);
            }
            else if (m_Rigidbody.velocity.y < -speed)
            {
                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, -speed);
            }
        }
    }
}