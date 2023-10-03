using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : InteractableBase
{
    public override void Interact()
    {
        Debug.Log("這是一件可互動的物品");
    }
}
