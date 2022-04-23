using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseItemState : PlayerAbilityState
{
    public PlayerUseItemState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) :
    base(currentContext, playerStateFactory, stateFactory)
    { }

    public override void EnterState()
    {
        base.EnterState();

        ConsumeItem();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    private void ConsumeItem()
    {
        Events.OnUseHealthCrystal.Invoke();
        _context.ApplyHealth(50);

        _stateMachine.ChangeState(_factory.IdleState);
    }
}