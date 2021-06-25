using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Drill : MonoBehaviour
{
    public GameObject _mainScript;
    public int _enable;
    public Collider2D[] _allBlock;
    public List<Collider2D> _allBlockList;
    int layerMask = 1 << 8; //Block
    int _inPlace;
    
    void Start()
    {
        _inPlace = 1;
        _enable = 0;
        StartCoroutine(ReSort());
    }
    void Update()
    {
        

        if(_mainScript.GetComponent<AutoMove>()._drill == 1 && _enable == 0)
        {
            // _allBlock = Physics2D.OverlapCircleAll(_mainScript.transform.position, 6, layerMask);
            // _allBlockList = _allBlock.ToList();

            if(_allBlockList.Count > 0)
            {
                // Sort();
                _enable = 1;
            }
            else
            {
                // _mainScript.GetComponent<AutoMove>()._drill = 0;
                StartCoroutine(MoveStart());
            }
        }
        else if(_enable == 1 && _allBlockList.Count > 0)
        {
            if (_allBlockList[0] == null)
            {
                _allBlockList.RemoveAt(0);
            }
            else if(_mainScript.GetComponent<AutoMove>()._energyfull == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position,_allBlockList[0].transform.position, 0.1f);
                 _allBlockList = _allBlockList.Where(x => x != null).OrderBy(x => Vector2.Distance(transform.position,x.transform.position)).ToList();
                _mainScript.GetComponent<AutoMove>()._energy -=5;
            }
            else
            {
                _allBlockList.Clear();
                // _enable = 0;
                StartCoroutine(MoveStart());
            }
        }
        else if (_inPlace == 1)
        {
            // _mainScript.GetComponent<AutoMove>()._drill = 0;
            // _enable = 0;
            StartCoroutine(MoveStart());
        }
    }
    IEnumerator ReSort()
    {
        while(true)
        {
            Sort();
            yield return new WaitForSeconds(0.5f);
        }
    }
    void Sort()
    {
        _allBlockList = _mainScript.GetComponent<AutoMove>()._ListBlock.Where(x => x != null).Where(x => Vector2.Distance(_mainScript.transform.position,x.transform.position) < 5 ).OrderBy(x => Vector2.Distance(transform.position,x.transform.position)).ToList();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.name == "Resource")
        {
            if( _mainScript.GetComponent<AutoMove>()._storageCount < UpdatePlayer.storageCountMax)
            {
                if(other.tag == "Yellow")
                {
                    Destroy(other.gameObject);
                    _mainScript.GetComponent<AutoMove>()._yellow ++;
                    _mainScript.GetComponent<AutoMove>()._energy -=5;
                }
                else if(other.tag == "Red")
                {
                    Destroy(other.gameObject);
                    _mainScript.GetComponent<AutoMove>()._red ++;
                    _mainScript.GetComponent<AutoMove>()._energy -=5;
                }
                else if(other.tag == "Blue")
                {
                    Destroy(other.gameObject);
                    _mainScript.GetComponent<AutoMove>()._blue ++;
                    _mainScript.GetComponent<AutoMove>()._energy -=5;
                }
            }
            _mainScript.GetComponent<AutoMove>()._storageCount = _mainScript.GetComponent<AutoMove>()._red + _mainScript.GetComponent<AutoMove>()._yellow + _mainScript.GetComponent<AutoMove>()._blue;
        }
    }
    IEnumerator MoveStart()
    {
        _mainScript.GetComponent<AutoMove>().SearchNearestObject();
        _inPlace = 0;
        while(transform.position != _mainScript.transform.position && _allBlockList.Count == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position,_mainScript.transform.position, 0.05f);
            yield return new WaitForSeconds(0.01f);
            if(_allBlockList.Count > 0)
            {
                break;
            }
        }
        _enable = 0;
        _mainScript.GetComponent<AutoMove>()._drill = 0;
        _inPlace = 1;
    }
}
