using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class Base : MonoBehaviour
    {
        public int red;
        public int yellow;
        public int blue;
        private const int CostRed = 200;
        private const int CostYellow = 20;
        private int _allCostBuild;
        public List<GameObject> mainBase = new();

        void Start()
        {
            _allCostBuild = CostRed + CostYellow;
            GetComponent<Delivery>().Search(mainBase, gameObject, "MainBase");
            StartCoroutine(BaseBuild.Build(mainBase, Red: CostRed, Yellow: CostYellow, AllCost: _allCostBuild));
        }

        void Update()
        {
            for (int i = 0; i < 2; i++)
            {
                if (red > 0 || yellow > 0 || blue > 0)
                {
                    var transform1 = transform;
                    var res = Instantiate(Global.Resource2, transform1.position, transform1.rotation);
                    var position = res.transform.position;
                    position = new Vector2(Random.Range(position.x - 0.3f, position.x + 0.3f), Random.Range(position.y - 0.3f, position.y + 0.3f));
                    res.transform.position = position;
                    res.GetComponent<Resource>().ListBase = mainBase;
                    if (red > 0)
                    {
                        red--;
                        res.GetComponent<SpriteRenderer>().color = Color.red;
                        res.tag = "Red2";
                    }
                    else if (yellow > 0)
                    {
                        yellow--;
                        res.GetComponent<SpriteRenderer>().color = Color.yellow;
                        res.tag = "Yellow2";
                    }
                    else if (blue > 0)
                    {
                        blue--;
                        res.GetComponent<SpriteRenderer>().color = Color.blue;
                        res.tag = "Blue";
                    }
                }
            }
        }
    }
}