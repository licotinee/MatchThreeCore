using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityData  
{
    [SerializeField]
    protected float _HP = 100;
    public float HP
    {
        get => _HP;
        set
        {
            if (value < 0)
                _HP = 0;
            else
                _HP = value;
        }
    }

    [SerializeField]
    protected float[] _defs = new float[2] {0,0};
    public float[] Defs => _defs;

    [SerializeField]
    protected float[] _baseDmgs = new float[2] {1,1}; 
    public float[] BaseDmgs => _baseDmgs;

    public float calculateDamage( IDamgeAble.DamageType damageType)
    {
        return 1 - (0.06f * this.Defs[(int)damageType] / (1 + 0.06f * this.Defs[(int)damageType]));
     
    }
}
