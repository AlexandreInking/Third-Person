using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Magazine), menuName = "SO/Slugs/Magazine")]
public class Magazine : Item
{
    public int count;

    public Slug slug;
}
