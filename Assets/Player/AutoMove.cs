using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AutoMove : MonoBehaviour
{
    //Авто перемещение персонажем
    Rigidbody2D _Rigidbody;
    int layerMask = 1 << 8; //Block
    public int busy = 0;
    public int full = 0;
    Collider2D[] hitColliders;
    public List<Collider2D> _ListBlock;
    public GameObject _Hand;
    Vector3 _StartVector3;
    int radius = 1;
    void Start()
	{
        _StartVector3 = new Vector3 (0,0,0);
        _Rigidbody = GetComponent<Rigidbody2D>();
        hitColliders = Physics2D.OverlapCircleAll(transform.position, 1, layerMask);
        StartCoroutine(ReSort());
    }
    void Update()
    {
        if (full == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,_StartVector3, 0.5f);
            _Hand.transform.rotation = Quaternion.LookRotation(Vector3.forward, _StartVector3 - _Hand.transform.position);
        }
        if (busy == 0)
        {
            if (Global.storageCount >= 200)
            {
                full = 1;
            }
            else if (Global.storageCount == 0 && full == 1)
            {
                StartCoroutine(SearchBlock());
                full = 0;
            }
            if (_ListBlock.Count == 0)
            {
                busy = 1;
                StartCoroutine(SearchBlock());
            }
            else if (full == 0)
            {
                if (_ListBlock[0] == null)
                {
                    _ListBlock.RemoveAt(0);
                    
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position,_ListBlock[0].transform.position, 0.3f);
                    _Hand.transform.rotation = Quaternion.LookRotation(Vector3.forward, _ListBlock[0].transform.position - _Hand.transform.position);
                }
            }
        }
    }
    IEnumerator SearchBlock()
    {
        radius = 10;
        while (hitColliders.Length < 100)
        {
            hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
            radius +=10;
            yield return new WaitForSeconds(0.1f);
        }
        _ListBlock = hitColliders.ToList();
        hitColliders = Physics2D.OverlapCircleAll(transform.position, 1, layerMask);
        
        busy = 0;
    }
    IEnumerator ReSort()
    {
        while(true)
        {
            SearchNearestObject();
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    void SearchNearestObject()
    {
        _ListBlock = _ListBlock.Where(x => x != null).OrderBy(x => Vector2.Distance(transform.position,x.transform.position)).ToList();
    }
}