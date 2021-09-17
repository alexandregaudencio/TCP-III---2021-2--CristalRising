using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Spell/Cure", order = 1)]
public class StatusCure : Status
{
    public float value;

    public override void Apply()
    {
        base.Apply();
    }
}
