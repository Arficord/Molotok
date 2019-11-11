using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public string name="";
    public bool isComputer = false;

    public PlayerController(string name, bool isComputer=false)
    {
        this.name = name;
        this.isComputer = isComputer;
    }
}
