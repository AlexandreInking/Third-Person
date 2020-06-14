using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryController : Controller
{
    /// <summary>
    /// Closer Reference
    /// </summary>
    public Item Equipped { get; protected set; }
}
