using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffAble 
{
    public IBuffAble GetBuff(float buffValue, BuffType buffType);

     public enum BuffType
    {
        PhysicsDef = 0,
        MagicsDef = 1,
        HP = 2
    }
}
