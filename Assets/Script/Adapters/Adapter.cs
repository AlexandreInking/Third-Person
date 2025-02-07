﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Adapter : MonoBehaviour
{
    protected Character actor;

    public Character Actor {

        get
        {
            return actor;
        }

        set
        {
            actor = value;

            Initialize();
        }
    }

    public abstract void Initialize();
}
