using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PolyCannon : RangedWeapon
{
    protected virtual void OnEnable()
    {
        //Add Live Slug
        if (!slugVariants.Contains(liveSlug))
        {
            slugVariants.Add(liveSlug);
        }
    }

    public List<Slug> slugVariants = new List<Slug>();

    public Slug NextVariant()
    {
        if (GetLiveSlugIndex() + 1 == slugVariants.Count)
        {
            return slugVariants[0];
        }

        else
        {
            return slugVariants[GetLiveSlugIndex() + 1];
        }
    }

    public Slug PreviousVariant()
    {
        if (GetLiveSlugIndex() == 0)
        {
            return slugVariants[slugVariants.Count - 1];
        }

        else
        {
            return slugVariants[GetLiveSlugIndex() - 1];
        }
    }

    public int GetLiveSlugIndex()
    {
        return slugVariants.IndexOf(liveSlug);
    }
}
