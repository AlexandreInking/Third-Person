using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : ScriptableObject
{
    public Dictionary<Slot, Item> Library = new Dictionary<Slot, Item>();

    protected virtual void OnEnable()
    {
        InitializeLibrary(25);
    }

    public void InitializeLibrary(int slotSize)
    {
        for (int i = 0; i < slotSize; i++)
        {
            Slot slot = new Slot()
            {
                index = i,
                isEquipped = false
            };

            Library.Add(slot, null);
        }
    }

    public KeyValuePair<Slot, Item> GetBlankEntry()
    {
        KeyValuePair<Slot, Item> keyPair = Library.FirstOrDefault(pair => !pair.Key.isEquipped);

        if (keyPair.Value != null)
        {
            Debug.LogError($"This is Not an Empty Entry");
        }

        if (keyPair.Key == null)
        {
            Debug.LogError($"All Slots Equipped");
        }


        return keyPair;
    }

    public void AddItem(Item item)
    {
        if (item is Magazine)
        {
            AddMagazine(item as Magazine);

            return;
        }

        KeyValuePair<Slot, Item> keyPair = GetBlankEntry();

        Library.Remove(keyPair.Key);

        keyPair.Key.isEquipped = true;

        Library.Add(keyPair.Key, item);
    }

    public void RemoveItem(int slotIndex)
    {
        KeyValuePair<Slot, Item> keyPair = GetInventoryEntry(slotIndex);

        Library.Remove(keyPair.Key);

        keyPair.Key.isEquipped = false;

        Library.Add(keyPair.Key, null);
    }

    /// <summary>
    /// Changes Made to The returned instance Will Not Apply
    /// </summary>
    /// <param name="slotIndex">Index of Slot Entry</param>
    /// <returns></returns>
    public KeyValuePair<Slot, Item> GetInventoryEntry(int slotIndex)
    {
        KeyValuePair<Slot, Item> keyPair = Library.FirstOrDefault(pair => pair.Key.index.Equals(slotIndex));

        if (keyPair.Key == null)
            Debug.LogError($"Library Slot {slotIndex} Not Initialized : Out Of Bounds");

        return keyPair;
    }


    #region Magazine

    /// <summary>
    /// Gets Magazine by <see cref="slug"/> Type, Changes Made to The returned instance Will Not Apply
    /// </summary>
    /// <param name="slug">Slug Type</param>
    /// <returns>Magazine</returns>
    public Magazine GetMagazine(Slug slug)
    {
        KeyValuePair<Slot, Item> keyPair = GetMagazineEntry(slug);

        return keyPair.Value as Magazine;
    }

    /// <summary>
    /// Gets Magazine Inventory Entry by <see cref="slug"/> Type, Changes Made to The returned instance Will Not Apply
    /// </summary>
    /// <param name="slug">Slug Type</param>
    /// <returns>Magazine Inventory Entry</returns>
    public KeyValuePair<Slot, Item> GetMagazineEntry(Slug slug)
    {
        KeyValuePair<Slot, Item> keyPair = Library
            .FirstOrDefault(pair => pair.Value is Magazine && (pair.Value as Magazine).slug == slug);

        if (keyPair.Value == null)
        {
            Debug.LogError($"Can't Find {slug.name} in Library");
        }

        return keyPair;
    }


    /// <summary>
    /// Add <see cref="rounds"/> amount of Rounds to Magazine with <see cref="slug"/> type
    /// </summary>
    /// <param name="slug">Slug Type</param>
    /// <param name="rounds">Rounds To Load</param>
    public void LoadMagazine(Slug slug, int rounds)
    {
        KeyValuePair<Slot, Item> magazine = GetMagazineEntry(slug);

        (Library[magazine.Key] as Magazine).count += rounds;
    }

    /// <summary>
    /// Remove <see cref="rounds"/> amount of Rounds to Magazine with <see cref="slug"/> type
    /// </summary>
    /// <param name="slug">Slug Type</param>
    /// <param name="rounds">Rounds To UnLoad</param>
    public void UnLoadMagazine(Slug slug, int rounds)
    {
        KeyValuePair<Slot, Item> magazine = GetMagazineEntry(slug);

        if (rounds >= (magazine.Value as Magazine).count)
        {
            (Library[magazine.Key] as Magazine).count = 0;

            Debug.LogError("Can't Unload AnyMore ; Magazine Empty");
        }

        else
        {
            (Library[magazine.Key] as Magazine).count -= rounds;
        }
    }

    /// <summary>
    /// Empty out Magazine
    /// </summary>
    /// <param name="slug">Slug Type</param>
    public void ClearMagazine(Slug slug)
    {
        KeyValuePair<Slot, Item> magazine = GetMagazineEntry(slug);

        (Library[magazine.Key] as Magazine).count = 0;
    }

    /// <summary>
    /// Add Magazine
    /// </summary>
    /// <param name="magazine">Magazine To Add</param>
    public void AddMagazine(Magazine magazine)
    {
        //Check if Magazine Exists
        KeyValuePair<Slot, Item> old = GetMagazineEntry(magazine.slug);

        if (old.Value == null)
        {
            KeyValuePair<Slot, Item> blankEntry = GetBlankEntry();

            Library.Remove(blankEntry.Key);

            blankEntry.Key.isEquipped = true;

            Library.Add(blankEntry.Key, magazine);
        }

        else
        {
            //Add to Existing
            LoadMagazine(magazine.slug, magazine.count);
        }
    }

    #endregion
}
