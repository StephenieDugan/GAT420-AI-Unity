using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDanceState : AIState
{
    float timer = 0;
    public AIDanceState(AIStateAgent agent) : base(agent)
    {
        AIStateTransition transition = new AIStateTransition(nameof(AIIdleState));
        transition.AddCondition(new BoolCondition(agent.enemySeen));
        transition.AddCondition(new FloatCondition(agent.enemydistance, Condition.Predicate.LESS, 1));
        transitions.Add(transition);
    }

    public override void OnEnter()
    {
        agent.movement.Stop();
        agent.movement.Velocity = Vector3.zero;
        agent.animator?.SetTrigger("Hip Hop Dance");
        timer = Time.time + 5;
    }
    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }


  
}
