using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PolyBarrel), menuName = "SO/Barrels/Poly")]
public class PolyBarrel : Barrel
{
    public List<Slug> slugVariants = new List<Slug>();

    protected virtual void OnEnable()
    {
        //Add Live Slug
        if (!slugVariants.Contains(liveSlug))
        {
            slugVariants.Add(liveSlug);
        }
    }

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
