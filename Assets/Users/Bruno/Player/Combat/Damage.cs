using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damage
{
    void CalculateDamage();
    GameObject GetTarget();
}
