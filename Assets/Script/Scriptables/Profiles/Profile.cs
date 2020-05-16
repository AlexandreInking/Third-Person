using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : ScriptableObject
{
    public string title;
    [TextArea]
    public string description;
}
