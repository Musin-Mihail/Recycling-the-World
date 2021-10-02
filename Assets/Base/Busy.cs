using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Busy : MonoBehaviour
{
    public int _busy;
    public int _empty;
    public int _numberBots;
    void Start() 
    {
        _empty = 1;
        _numberBots = 0;
        _busy = 0;
    }
}