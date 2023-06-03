using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, SEQUENCE }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerStation;
    public Transform enemyStation;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHud playerHUD;
    public BattleHud enemyHUD;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle() 
    {
        GameObject playerGO = Instantiate(playerPrefab, playerStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " appears...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
       
    IEnumerator PlayerAttack()
    {
        state = BattleState.SEQUENCE;

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerUnit.unitName + " did " + playerUnit.damage + " damage!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator PlayerHeal()
    {
        state = BattleState.SEQUENCE;
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = playerUnit.unitName + " heals 5 health!";

        yield return new WaitForSeconds(3f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You defeated " + enemyUnit.unitName + "!";
        }
        else if (state == BattleState.LOST) 
        {
            dialogueText.text = playerUnit.unitName + " has fainted!";
        }
    }

    IEnumerator EnemyTurn()
    {

        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(2f);


        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        dialogueText.text = enemyUnit.unitName + " did " + enemyUnit.damage + " damage!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void PlayerTurn() 
    {
        dialogueText.text = "Choose an action:";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerAttack());

    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        StartCoroutine(PlayerHeal());

    }
}