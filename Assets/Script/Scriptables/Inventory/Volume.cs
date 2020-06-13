using System;
using UnityEngine;

[Serializable]
public class Volume
{
    public Item Item;

    public Adapter Adapter;

    public GameObject Instance;

    public void ClearVoulme()
    {
        UnityEngine.Object.Destroy(Instance);

        Adapter = null;

        Item = null;
    }
}
