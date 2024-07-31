using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAnimBase : MonoBehaviour
{
    public abstract void ChangeAnim(PlayerController.PLAYER_STATE state, bool isLopp , Action<string> onEvent = null, bool isForce = false, bool isRandom = true);
 
}
