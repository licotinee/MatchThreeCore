using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;
using Spine;

[Serializable]
public class AnimGroup
{
    [SerializeField] public string _nameAnimGroup;
    [SerializeField]
    [SpineAnimation] public List<string> _anims = new List<string>() ;
}
public class PlayerAnimSpine : PlayerAnimBase
{
    SkeletonAnimation _animation;
    PlayerController.PLAYER_STATE _currentState;

    [SerializeField]
    List<AnimGroup> _animGroups;

    Dictionary<string, List<string>> _useAbleAnimGroups = new Dictionary<string, List<string>>();

    // Start is called before the first frame update
    void Start()
    {
        _animation = this.GetComponent<SkeletonAnimation>();
        foreach (AnimGroup a in _animGroups)
        {
            _useAbleAnimGroups.Add(a._nameAnimGroup, a._anims);
        }
    }


    public override void ChangeAnim(PlayerController.PLAYER_STATE state, bool isLoop, Action<string>  onEvent = null, bool isForce = false, bool isRandom = true)
    {
        if (!isForce && _currentState == state)
        {
            return;
        }

        _currentState = state;
        string newAnim = _useAbleAnimGroups[state.ToString()][0];
        Spine.TrackEntry entry;
        if (isRandom)
        {
            int ranIndexAnim = UnityEngine.Random.Range(0, _useAbleAnimGroups[state.ToString()].Count); 
            newAnim = _useAbleAnimGroups[state.ToString()][ranIndexAnim];
            entry = _animation.AnimationState.SetAnimation(0, newAnim, isLoop);
        }
        else
        {
            entry = _animation.AnimationState.SetAnimation(0, newAnim, false);
            List<string> sequanceAnim = _useAbleAnimGroups[state.ToString()];

            for (int i = 1; i< sequanceAnim.Count;i++)
            {
                _animation.AnimationState.AddAnimation(0, sequanceAnim[i], false, 0 );
            }
            
        }
        
        
        entry.Start += (TrackEntry trackEntry) =>
        {
            onEvent?.Invoke("Start");
        };

        entry.Complete += (TrackEntry trackEntry) =>
        {
            onEvent?.Invoke("End");
        };


        entry.Event += (TrackEntry trackEntry, Spine.Event e) =>
        {
            onEvent?.Invoke(e.Data.Name);
        };
    }

    

    
}
