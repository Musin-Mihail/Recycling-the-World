using UnityEngine;
public class Chance : MonoBehaviour
{
    float size;
    // float sizeBlock = 0.6f;
    // int DisableColor = 0;
    Vector3 Target1;
    Vector3 Target2;
    Vector3 Target3;
    Vector3 Target4;
    // int check2 = 1;
    // public GameObject Parent;
    // int layerMask = 1 << 14; //Пещера
    // public float DistancePlayer;
    // public AudioSource DestroyRes;
    // public static float fps;
    // float PerlinNoiseCave;
    // float fff;
    // private float noiseScale = 0.06f;
    // private float threshold = 0.2f;
    // public float chance;
    void Start()
    {
        // PerlinNoiseCave = Mathf.PerlinNoise((transform.position.x+Global.RandomCave)*noiseScale, (transform.position.y+Global.RandomCave)*noiseScale);
        // fff = threshold + transform.localScale.x / 25;

        size = transform.localScale.x;
        Target1 = new Vector3(transform.position.x-size/4,transform.position.y-size/4,transform.position.z);
        Target2 = new Vector3(transform.position.x-size/4,transform.position.y+size/4,transform.position.z);
        Target3 = new Vector3(transform.position.x+size/4,transform.position.y-size/4,transform.position.z);
        Target4 = new Vector3(transform.position.x+size/4,transform.position.y+size/4,transform.position.z);
        Invoke("GenCave",Random.Range(0.4f,0.7f));
    }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
// //Уничтожение при попадания лазера
//         if(other.name == "Drill" && check2 == 1)
//         {
//             check2 = 0;
//             if(transform.localScale.x > sizeBlock) 
//             {
//                 // Destroy(gameObject);
//                 gameObject.SetActive(false);
//                 GenChank(Target1);
//                 GenChank(Target2);
//                 GenChank(Target3);
//                 GenChank(Target4);
//             } 
// //Создание ресурсов после уничтожения, если блок маленький
//             else if (transform.tag == "Red")
//             {
//                     // Destroy(gameObject);
//                     gameObject.SetActive(false);
//                     var Resource1 =  Instantiate(Global.Resource, transform.position, transform.rotation);
//                     Resource1.GetComponent<SpriteRenderer>().color = Color.red;
//                     Resource1.name = "Resource";
//                     Resource1.tag = "Red";   
//             }
//             else if (transform.tag == "Yellow")
//             {
//                 float chance = Random.Range(0.0f,10.0f);
//                 if(chance<=0.1f)
//                 {
//                     // Destroy(gameObject);  
//                     gameObject.SetActive(false); 
//                     var Resource2 = Instantiate(Global.Resource, transform.position, transform.rotation);
//                     Resource2.GetComponent<SpriteRenderer>().color = Color.blue;
//                     Resource2.name = "Resource";
//                     Resource2.tag = "Blue";
//                 }
//                 else
//                 {
//                     // Destroy(gameObject);   
//                     gameObject.SetActive(false);      
//                     var Resource3 = Instantiate(Global.Resource, transform.position, transform.rotation);
//                     Resource3.GetComponent<SpriteRenderer>().color = Color.yellow;
//                     Resource3.name = "Resource";
//                     Resource3.tag = "Yellow";
//                 } 
//             }
//             else
//             {
//                 // Destroy(gameObject);
//                 gameObject.SetActive(false);
//             }
//         }
//     }
//     public void GenCave()
//     {
//  //Генерация пещер
//         Collider2D hitColliders = Physics2D.OverlapBox(transform.position, new Vector2 (transform.localScale.x,transform.localScale.y), 0, layerMask);
//         if(PerlinNoiseCave < fff)
//         {
//             if(transform.localScale.x > sizeBlock)
//             {
//                 gameObject.SetActive(false);
//                 GenCaveChank(Target1);
//                 GenCaveChank(Target2);
//                 GenCaveChank(Target3);
//                 GenCaveChank(Target4);
//             }
//             else
//             {
//                 gameObject.SetActive(false);
//                 // GenEnemy(transform.position);
//             }
//         }
//     }
//     void GenEnemy (Vector3 Spawn)
//     {
// //Генерация врагов
//         float Chance = Random.Range(0.0f,10.0f);
//         if(Chance<0.5f)
//         {
//             var block2 = Instantiate(Global.Enemy, Spawn, transform.rotation);
//             block2.transform.parent = Global.EnemyStor.transform;
//         }
//     }
}