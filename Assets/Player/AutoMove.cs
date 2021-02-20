using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AutoMove : MonoBehaviour
{
    //Авто перемещение персонажем
    Rigidbody2D _Rigidbody;
    int layerMask = 1 << 8; //Block
    int layerMask2 = 1 << 17; //TopBlock
    int layerMask3;
    int busy = 1;
    int full = 0;
    Collider2D[] hitColliders;
    public List<Collider2D> _ListBlock;
    public GameObject _Hand;
    public GameObject OldBuildings;
    public Vector3 _StartVector3;
    int radius = 20;
    void Start()
	{
        layerMask3 = layerMask | layerMask2;
        _StartVector3 = new Vector3 (0,0,0);
        _Rigidbody = GetComponent<Rigidbody2D>();
        // hitColliders = Physics2D.OverlapCircleAll(transform.position, 1, layerMask);
        Invoke("StartMove", 5.0f);
        StartCoroutine(ReSort());
    }
    void Update()
    {
        if (_ListBlock.Count == 0 && busy == 0)
        {
            busy = 1;
            SearchBlock();
        }
        if (full == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,_StartVector3, 0.5f);
            _Hand.transform.rotation = Quaternion.LookRotation(Vector3.forward, _StartVector3 - _Hand.transform.position);
        }
        else if (_ListBlock.Count > 0)
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

        if (Global.storageCount >= 200)
        {
            SearchNearestBase();
            full = 1;
        }
        else if (Global.storageCount == 0 && full == 1)
        {
            full = 0;
        }
        RaycastHit2D hit = Physics2D.Raycast(_Hand.transform.position, _Hand.transform.up,30,layerMask3);
        if(hit && hit.collider.name == "Top")
        {
            Destroy(hit.collider.gameObject);
        }
        // Debug.DrawRay(_Hand.transform.position, _Hand.transform.up*10,Color.green,0.5f);
    }
    void SearchBlock()
    {
        if(Global.BuildingsDiger.Count >0)
        {
            hitColliders = Physics2D.OverlapCircleAll(Global.BuildingsDiger[0].transform.position, radius, layerMask);
            if (hitColliders.Length > 0)
            {
                _ListBlock = hitColliders.ToList();
            }
            else
            {
                Global.BuildingsDiger.RemoveAt(0);
            }
        }
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
    void SearchNearestBase()
    {
        var test = Global.BuildingsCharge.Where(x => x != null).OrderBy(x => Vector2.Distance(transform.position,x.transform.position)).ToList(); 
        _StartVector3 = test[0].transform.position;
    }
    void StartMove()
    {
        busy = 0;
    }
}