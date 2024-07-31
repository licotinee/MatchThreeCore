using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamgeAble  
{
    public IDamgeAble GetHit(float damage, DamageType damageType);

    public enum DamageType
    {
        Physic = 0,
        Magic = 1
    }

}


