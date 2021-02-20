using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chance : MonoBehaviour
{
    float size;
    float sizeBlock = 0.6f;
    int DisableColor = 0;
    Vector3 Target1;
    Vector3 Target2;
    Vector3 Target3;
    Vector3 Target4;
    int check2 = 1;
    public GameObject Parent;
    int layerMask = 1 << 14; //Пещера
    public float DistancePlayer;
    public AudioSource DestroyRes;
    public static float fps;
    float PerlinNoiseCave;
    float fff;
    private float noiseScale = 0.06f;
    private float threshold = 0.2f;
    public float chance;
    void Start()
    {
        PerlinNoiseCave = Mathf.PerlinNoise((transform.position.x+Global.RandomCave)*noiseScale, (transform.position.y+Global.RandomCave)*noiseScale);
        fff = threshold+transform.localScale.x/25;

        size = transform.localScale.x;
        Target1 = new Vector3(transform.position.x-size/4,transform.position.y-size/4,transform.position.z);
        Target2 = new Vector3(transform.position.x-size/4,transform.position.y+size/4,transform.position.z);
        Target3 = new Vector3(transform.position.x+size/4,transform.position.y-size/4,transform.position.z);
        Target4 = new Vector3(transform.position.x+size/4,transform.position.y+size/4,transform.position.z);
        if(gameObject.tag == "Ground")
        {
            if(transform.localScale.x == 0.3125f)
            {
                GetComponent<SpriteRenderer>().color = new Color32(110,80,40,255);
            }
            if(transform.localScale.x == 0.625f)
            {
                GetComponent<SpriteRenderer>().color = new Color32(80,60,30,255);
            }
            if(transform.localScale.x == 1.25f)
            {
                GetComponent<SpriteRenderer>().color = new Color32(60,40,20,255);
            }
            if(transform.localScale.x == 2.5f)
            {
                GetComponent<SpriteRenderer>().color = new Color32(40,20,10,255);
            }
            if(transform.localScale.x == 5.0f)
            {
                GetComponent<SpriteRenderer>().color = new Color32(20,0,0,255);
            }
        }
        else
        {
            if(transform.localScale.x == 1.25f)
            {
                GetComponent<SpriteRenderer>().color = new Color32(60,40,20,255);
            }
            if(transform.localScale.x == 2.5f)
            {
                GetComponent<SpriteRenderer>().color = new Color32(40,20,10,255);
            }
            if(transform.localScale.x == 5.0f)
            {
                GetComponent<SpriteRenderer>().color = new Color32(20,0,0,255);
            }
        }
        Invoke("GenCave",Random.Range(0.4f,0.7f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
//Уничтожение при попадания лазера
        if(other.tag == "Laser" || other.name == "Body" && check2 == 1)
        {
            check2 = 0;
            if(transform.localScale.x > sizeBlock) 
            {
                Destroy(gameObject);
                GenChank(Target1);
                GenChank(Target2);
                GenChank(Target3);
                GenChank(Target4);
            } 
//Создание ресурсов после уничтожения, если блок маленький
            else if (transform.tag == "Red")
            {
                    Destroy(gameObject);
                    var Resource1 =  Instantiate(Global.Resource, transform.position, transform.rotation);
                    Resource1.GetComponent<SpriteRenderer>().color = Color.red;
                    Resource1.name = "Resource";
                    Resource1.tag = "Red";   
            }
            else if (transform.tag == "Yellow")
            {
                float chance = Random.Range(0.0f,10.0f);
                if(chance<=0.1f)
                {
                    Destroy(gameObject);   
                    var Resource2 = Instantiate(Global.Resource, transform.position, transform.rotation);
                    Resource2.GetComponent<SpriteRenderer>().color = Color.blue;
                    Resource2.name = "Resource";
                    Resource2.tag = "Blue";
                }
                else
                {
                    Destroy(gameObject);         
                    var Resource3 = Instantiate(Global.Resource, transform.position, transform.rotation);
                    Resource3.GetComponent<SpriteRenderer>().color = Color.yellow;
                    Resource3.name = "Resource";
                    Resource3.tag = "Yellow";
                } 
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    public void GenCave()
    {
 //Генерация пещер
        Collider2D hitColliders = Physics2D.OverlapBox(transform.position, new Vector2 (transform.localScale.x,transform.localScale.y), 0, layerMask);
        if(PerlinNoiseCave<fff)
        {
            if(transform.localScale.x > sizeBlock)
            {
                Destroy(gameObject);
                GenCaveChank(Target1);
                GenCaveChank(Target2);
                GenCaveChank(Target3);
                GenCaveChank(Target4);
            }
            else
            {
                Destroy(gameObject);
                // GenEnemy(transform.position);
            }
        }
    }
    void GenChank(Vector3 target)
    {
//Генерация блоков
        var block2 = Instantiate(Global.Chank2, target, transform.rotation);
        block2.transform.localScale = new Vector3(size/2,size/2,1);
        block2.name = "Block";
        block2.transform.parent = Parent.transform;
        block2.GetComponent<Chance>().Parent = Parent;
        chance = Random.Range(0.0f,10.0f);
        
        if(transform.tag == "Red")
        {
            if(chance<6.0f)
            {
                block2.tag = "Red";
                ColorChank(block2);
            }
            if(chance<0.1f)
            {
                block2.tag = "Yellow";
                ColorChank(block2);
            }
        }
        else if (transform.tag == "Yellow")
        {
            if(chance<4.0f)
            {
                block2.tag = "Yellow";
                ColorChank(block2);
            }
            if (chance<0.1f)
            {
                block2.tag = "Red";
                ColorChank(block2);
            }
        }
    }
    void GenCaveChank(Vector3 target)
    {
//Генерация пещеры
        var block2 = Instantiate(Global.Chank2, target, transform.rotation);
        
        block2.transform.localScale = new Vector3(size/2,size/2,1);
        
        block2.name = "Block";
        block2.transform.parent = Parent.transform;
        block2.GetComponent<Chance>().Parent = Parent;
        float chance = Random.Range(0.0f,10.0f);
        if(block2.transform.localScale.x<=1.0f)
        {
//Больше жёлтого чем обычно
            if(chance<8.0f)
            {
                block2.tag = "Yellow";
                ColorChank(block2);
            }
            if(chance<1.0f)
            {
                block2.tag = "Red";
                ColorChank(block2);
            }
        }
        else
        {
            if(transform.tag == "Red")
            {
                if(chance<6.0f)
                {
                    block2.tag = "Red";
                    ColorChank(block2);
                }
                if(chance<0.1f)
                {
                    block2.tag = "Yellow";
                    ColorChank(block2);
                }
            }
            else if (transform.tag == "Yellow")
            {
                if(chance<4.0f)
                {
                    block2.tag = "Yellow";
                    ColorChank(block2);
                }
                if (chance<0.1f)
                {
                    block2.tag = "Red";
                    ColorChank(block2);
                }
            }
        }
    }
    void ColorChank(GameObject test)
    {
//Покраска блоков
        if(DisableColor == 0)
        {
            if(test.tag == "Red")
            {
                test.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if(test.tag == "Yellow")
            {
                test.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
    }
    void GenEnemy (Vector3 Spawn)
    {
//Генерация врагов
        float Chance = Random.Range(0.0f,10.0f);
        if(Chance<0.5f)
        {
            var block2 = Instantiate(Global.Enemy, Spawn, transform.rotation);
            block2.transform.parent = Global.EnemyStor.transform;
        }
    }
}