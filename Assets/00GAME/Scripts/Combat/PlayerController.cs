using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour, IDamgeAble, IBuffAble
{
    [SerializeField] Slider _hpSlider;
    [SerializeField]
    EntityData _userInfo;
    PlayerAnimBase animControl;
    [SerializeField]
    PLAYER_STATE _playerState = PLAYER_STATE.Idle;
    public PLAYER_STATE playerState => _playerState;

    // Start is called before the first frame update
    void Start()
    {
        animControl = this.GetComponentInChildren<PlayerAnimBase>();
        animControl.ChangeAnim(_playerState, false);
    }

    

    public void Attack(PlayerController target, float damage, IDamgeAble.DamageType damageType)
    {
        if (_playerState == PLAYER_STATE.Skill)
        {
            return;
        }

        _playerState = PLAYER_STATE.Attack;
        animControl.ChangeAnim(_playerState, false, (eventName) =>
        {
            if (eventName == "End")
            {
                _playerState = PLAYER_STATE.Idle;
                animControl.ChangeAnim(_playerState, true);
                target.GetHit(damage, damageType); 
            }

            if (eventName == "attack1")
            {
               
                //spaw bullet
            }
        });

    }

    public IBuffAble GetBuff(float buffValue, IBuffAble.BuffType buffType)
    {
        if (_playerState != PLAYER_STATE.Skill)
        {
            _playerState = PLAYER_STATE.Buff;
            animControl.ChangeAnim(_playerState, false, onEvent: (eventName) =>
            {
                if (eventName == "End")
                {
                    _playerState = PLAYER_STATE.Idle;
                    animControl.ChangeAnim(_playerState, true);

                    if ((int)buffType < 2)
                        _userInfo.Defs[(int)buffType] += buffValue;
                    else
                        _userInfo.HP += buffValue;
                }
            });
        }


        return this;
    }

    public void ChargeStart()
    {
        _playerState = PLAYER_STATE.Skill;

        animControl.ChangeAnim(_playerState, false);
    }

    public void ChargeEnd()
    {
        Debug.LogError("cal charge end");
        _playerState = PLAYER_STATE.SkillEnd;
        animControl.ChangeAnim(_playerState, false, onEvent: (eventName) =>
        {
            if (eventName == "End")
            {
                _playerState = PLAYER_STATE.Idle;
                animControl.ChangeAnim(_playerState, true);
            }
        });
    }

    public IDamgeAble GetHit(float damage, IDamgeAble.DamageType damageType)
    {
        _playerState = PLAYER_STATE.Hurt;
        animControl.ChangeAnim(state: _playerState, isLopp: false, isForce: true, onEvent: (eventName) =>
        {
            if (eventName == "End")
            {
                _playerState = PLAYER_STATE.Idle;
                animControl.ChangeAnim(_playerState, true);

                _userInfo.HP -= damage * _userInfo.calculateDamage(damageType);
                _hpSlider.value = _userInfo.HP;
            }
        });

        return this;
    }

     

    public enum PLAYER_STATE
    {
        Idle = 0,
        Attack = 1,

        Skill = 2,
        SkillEnd= 7,

        Hurt = 3,
        Stun = 4,
        Die = 5,
        Buff = 6
    }
}
