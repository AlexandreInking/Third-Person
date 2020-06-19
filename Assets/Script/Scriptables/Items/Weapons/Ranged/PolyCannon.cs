using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PolyCannon : RangedWeapon
{
    public List<Barrel> barrelVariants = new List<Barrel>();

    protected virtual void OnEnable()
    {
        //Add Live Slug
        if (!barrelVariants.Contains(liveBarrel))
        {
            barrelVariants.Add(liveBarrel);
        }
    }

    public Barrel NextVariant()
    {
        if (GetLiveSlugIndex() + 1 == barrelVariants.Count)
        {
            return barrelVariants[0];
        }

        else
        {
            return barrelVariants[GetLiveSlugIndex() + 1];
        }
    }

    public Barrel PreviousVariant()
    {
        if (GetLiveSlugIndex() == 0)
        {
            return barrelVariants[barrelVariants.Count - 1];
        }

        else
        {
            return barrelVariants[GetLiveSlugIndex() - 1];
        }
    }

    public int GetLiveSlugIndex()
    {
        return barrelVariants.IndexOf(liveBarrel);
    }
}
