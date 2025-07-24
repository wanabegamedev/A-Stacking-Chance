using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public abstract class BlockModifier : MonoBehaviour
{
    public string modifierName = "Default Modifier";
    public string modifierDescription = "Default Description";

    public abstract void ActivateModifier();
    
    
    
}
