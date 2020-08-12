using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSwipeAnimationFinished : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<WeaponSwipe>().AttackSwipeFinished();
    }
}
