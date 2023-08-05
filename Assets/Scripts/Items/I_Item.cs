using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Item
{
    string itemName { get; set; }
    string itemDescription { get; set; }

    

    virtual void PickUp(Collider2D collision) 
    {
    }


}
