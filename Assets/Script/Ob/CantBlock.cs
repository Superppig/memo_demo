using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

public class CantBlock : Obstacle
{
    void Start()
    {
        StartThing();
        canDes = false;
    }
}
