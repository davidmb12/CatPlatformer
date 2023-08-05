using NarvalDev.Gameplay;
using NarvalDev.Runner;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
public class BlueCoin : MonoBehaviour, I_Item
{
    public string itemName { get; set; }
    public string itemDescription { get; set; }
    public Event OnItemPickedUp { get; set; }


    public void PickUp(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            
        }
        Destroy(this.gameObject);
    }

}
