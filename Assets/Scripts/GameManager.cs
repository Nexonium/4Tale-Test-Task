
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Settings of the main game state, turns, and play
/// </summary>

public class GameManager : MonoBehaviour
{

    public PlayerEntity player;
    public EnemyEntity enemy;

    public Deck deck;
    public UIManager uiManager;

    public Button endTurnButton;

    private enum PlayTurn { PlayerTurn, EnemyTurn }
    private PlayTurn currentTurn;
    
    private void Start()
    {
        StartBattle();
        endTurnButton.onClick.AddListener(OnEndTurnButtonClicked);
    }

    void StartBattle()
    {
        player.Initialize();
        enemy.Initialize();
        deck.Initialize();

        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        currentTurn = PlayTurn.PlayerTurn;
        endTurnButton.enabled = true;

        player.RestoreEnergy(player.maxEnergy);
        player.ResetDefence();

        deck.DrawCards(player.handSize);

        uiManager.UpdateHand(deck.handPile);
        uiManager.UpdateEnergy(player.energy);

        Debug.Log("Player's turn starts!");
    }

    public void EndPlayerTurn()
    {

        deck.DiscardHand();
        uiManager.UpdateHand(deck.handPile);

        Debug.Log("Player's turn ends!");

        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        currentTurn = PlayTurn.EnemyTurn;
        endTurnButton.enabled = false;
        Debug.Log("Enemy's turn starts!");

        enemy.ResetDefence();

        if (enemy.plannedAction != null)
        {
            // Default attack target is Player
            // Defaul defence and heal target is self
            Entity defaultTarget = enemy.plannedAction is EnemyAttack ? (Entity)player : (Entity)enemy;
            Debug.Log("Enemy makes action: " + enemy.plannedAction.actionName);

            // Play animations and stuff
            yield return new WaitForSeconds(1f);
            enemy.ExecutePlannedAction(defaultTarget);
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Enemy's turn ends!");

        StartPlayerTurn();
        
        enemy.PlanNextAction();
    }

    public void PlayCard(Card card, Entity target)
    {
        if (player.CanPlayCard(card))
        {
            player.PlayCard(card, target);
            deck.DiscardCard(card);
            uiManager.UpdateHand(deck.handPile);
            uiManager.UpdateEnergy(player.energy);
        }
    }

    public void OnEndTurnButtonClicked()
    {
        if (currentTurn == PlayTurn.PlayerTurn)
        {
            EndPlayerTurn();
        }
    }

    public bool isTargetedEntity(Entity entity)
    {
        return uiManager.isTargetedEntity(entity);
    }

    public bool isOutOfHand()
    {
        return uiManager.isOutOfHand();
    }

    public void ShowPlayerMessage()
    {
        uiManager.ShowPlayerMessage();
    }

    public void WinBattle(bool winState)
    {
        var endText = winState == true ? "You won!" : "You died";

        uiManager.ShowEndBattleText(endText);
        EndBattle();
    }

    public void EndBattle()
    {
        Destroy(endTurnButton.gameObject);
        Destroy(player.gameObject);
        Destroy(enemy.gameObject);
        deck.DiscardHand();
    }
}
