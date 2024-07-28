using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Settings of battle, player turn, enemy turn, and end turn button
/// </summary>
public class BattleManager : MonoBehaviour
{

    public PlayerEntity player;
    public EnemyEntity enemy;
    public Button endTurnButton;
    public Deck deck;
    public HandFieldController handFieldController;

    private enum TurnState { PlayerTurn, EnemyTurn }
    private TurnState currentState;


    void Start()
    {
        endTurnButton.onClick.AddListener(OnEndTurnButtonClicked);
        StartBattle();
    }

    void StartBattle()
    {
        // Fight init
        enemy.PlanNextAction();
        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        currentState = TurnState.PlayerTurn;
        player.energy = player.maxEnergy;
        endTurnButton.interactable = true;
        Debug.Log("Player's turn starts!");
    }

    void EndPlayerTurn()
    {
        currentState = TurnState.EnemyTurn;
        endTurnButton.interactable = false;
        deck.DiscardHand();
        handFieldController.UpdateHandDisplay(deck.handPile.ToArray());

        Debug.Log("Player's turn ends!");
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy's turn starts!");

        yield return new WaitForSeconds(1f);

        if (enemy.plannedAction != null)
        {
            // Default attack target is Player
            // Defaul defence and heal target is enemy
            Entity defaultTarget = enemy.plannedAction is EnemyAttack ? (Entity)player : (Entity)enemy;
            enemy.ExecutePlannedAction(defaultTarget);

            Debug.Log("Enemy makes action: " + enemy.plannedAction.actionName);
        }

        yield return new WaitForSeconds(1f);

        // After finishing animations and stuff, end enemy turn
        Debug.Log("Enemy's turn ends!");
        StartPlayerTurn();
        enemy.PlanNextAction();
    }

    void OnEndTurnButtonClicked()
    {
        if (currentState == TurnState.PlayerTurn)
        {
            EndPlayerTurn();
        }
    }

}
