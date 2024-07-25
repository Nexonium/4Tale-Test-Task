using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public PlayerEntity player;
    public EnemyEntity enemy;

    private enum TurnState { PlayerTurn, EnemyTurn }
    private TurnState currentState;


    void Start()
    {

        // Fight init

        //player.maxHealth = 50;
        //player.health = 50;

        player.maxEnergy = 3;
        player.energy = player.maxEnergy;


        //enemy.maxHealth = 80;
        //enemy.health = 80;

        enemy.PlanNextAction();

        currentState = TurnState.PlayerTurn;
    }


    void Update()
    {
        
        switch (currentState)
        {
            case TurnState.PlayerTurn:
                // TODO play cards
                // Targeting and stuff
                break;

            case TurnState.EnemyTurn:
                ExecuteEnemyTurn();
                break;
        }
    }

    public void EndPlayerTurn()
    {
        // Play some end turn animations and stuff
        currentState = TurnState.EnemyTurn;
    }

    private void ExecuteEnemyTurn()
    {
        // Default attack target is Player
        // Defaul defence and heal target is enemy

        Entity defaultTarget = enemy.plannedAction is EnemyAttack ? (Entity)player : (Entity)enemy;

        enemy.ExecutePlannedAction(defaultTarget);
        enemy.PlanNextAction();

        // After finishing animations and stuff, end enemy turn
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {

        // Restoring player energy
        player.energy = player.maxEnergy;

        currentState = TurnState.PlayerTurn;
    }
}
