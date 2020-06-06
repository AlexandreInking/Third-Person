using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ScriptableObject
{
    public Dictionary<Slot, Item> Library = new Dictionary<Slot, Item>();
}
