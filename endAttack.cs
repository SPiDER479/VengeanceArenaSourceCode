using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endAttack : StateMachineBehaviour
{
    //called when the state is being exited
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attacking", false);
    }
}
