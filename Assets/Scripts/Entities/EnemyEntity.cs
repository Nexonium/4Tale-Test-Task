
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{

    public List<EnemyAction> possibleActions;
    public EnemyAction plannedAction;

    private void Start()
    {

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
        Destroy(gameObject);
    }

    public void PlanNextAction()
    {
        if (possibleActions.Count > 0)
        {
            plannedAction = possibleActions[Random.Range(0, possibleActions.Count)];
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
    }
}

public class EnemyDefence : EnemyAction
{

    public EnemyDefence() : base("Defence") { }

    public override void ExecuteAction(Entity defaultTarget)
    {
        target = target ?? defaultTarget;
        target.defence += actionEffectValue;
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
            int healthAfterHealing = Mathf.Min(target.health + actionEffectValue, target.maxHealth);
            target.health = healthAfterHealing;
        }
    }
}

#endregion