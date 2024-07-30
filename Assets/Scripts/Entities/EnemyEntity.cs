
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Setting of possible actions, planning and executing actions of the enemy
/// </summary>

public class EnemyEntity : Entity
{

    public List<EnemyAction> possibleActions;
    public EnemyAction plannedAction;
    public EnemyState currentState;

    private void Start()
    {
        //Initialize();
    }

    public override void Initialize()
    {
        healthBar.Initialize(maxHealth, health);

        // Subscribing healthbar to the events
        OnHealthChanged += healthBar.SetHealth;
        OnDefenceChanged += healthBar.SetDefence;

        possibleActions = new List<EnemyAction>
        {
            new EnemyAttack { actionEffectValue = 10 },
            new EnemyDefence { actionEffectValue = 5 },
            new EnemyHeal { actionEffectValue = 8 }
        };

        PlanNextAction();
    }

    protected override void Die()
    {
        Debug.Log("Enemy has died!");

        // TODO: Game end

        Destroy(gameObject);
    }

    public void PlanNextAction()
    {
        if (possibleActions.Count > 0)
        {
            plannedAction = possibleActions[Random.Range(0, possibleActions.Count)];
            currentState.ChangeStateImage(plannedAction);
            Debug.Log("Enemy plans to: " + plannedAction.actionName);
        }
    }

    public void ExecutePlannedAction(Entity defaultTarget)
    {
        if (plannedAction != null)
        {
            plannedAction.ExecuteAction(defaultTarget);
        }
    }
}


#region Actions
public abstract class EnemyAction
{

    public string actionName;
    public int actionEffectValue;
    public Entity target;

    protected EnemyAction(string name)
    {
        actionName = name;
    }

    public abstract void ExecuteAction(Entity target);
}


public class EnemyAttack : EnemyAction
{

    public EnemyAttack() : base("Attack") { }

    public override void ExecuteAction(Entity defaultTarget)
    {
        target = target ?? defaultTarget;
        target.TakeDamage(actionEffectValue);

        Debug.Log($"Enemy strikes, dealing {actionEffectValue} damage to {target}!");
    }
}

public class EnemyDefence : EnemyAction
{

    public EnemyDefence() : base("Defence") { }

    public override void ExecuteAction(Entity defaultTarget)
    {
        target = target ?? defaultTarget;
        target.GainDefence(actionEffectValue);

        Debug.Log($"Enemy defends, gaining {actionEffectValue} block to {target}!");
    }
}

public class EnemyHeal : EnemyAction
{

    public EnemyHeal() : base("Heal") { }

    public override void ExecuteAction(Entity defaultTarget)
    {
        target = target ?? defaultTarget;
        if (target.health < target.maxHealth)
        {
            target.GainHealth(actionEffectValue);
        }

        Debug.Log($"Enemy rests, regaining {actionEffectValue} health to {target}!");
    }
}

#endregion