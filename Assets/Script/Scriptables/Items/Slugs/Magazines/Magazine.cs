using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine<T> : Item where T : Slug
{
    public int count;

    public T slug;
}
