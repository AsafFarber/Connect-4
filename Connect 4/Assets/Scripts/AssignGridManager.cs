using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonActive.Connect4;
using System;


public class AssignGridManager : MonoBehaviour
{
    [SerializeField] public GridManager gridManager;
    void Start()
    {
        GetComponent<IDisk>().StoppedFalling += gridManager.EndTurn;
    }
}
