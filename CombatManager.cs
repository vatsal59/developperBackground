using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class CombatManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PokemonData enemy;
    private PokemonData myPokemon;
    private List<AttackData> myAttacks;
    private List<AttackData> enemyAttacks;
    private PokedexManager.PokemonList pokemonList;
    public SaveInventory inventory;
    public TMP_Text[] buttonText;
    public Image enemyHealthUi;
    private int intialEnemyPv;
    private int initialPokemonPv;
    public Canvas attackBar; 
    public Text endMessage;
    public GameObject endMessagePanel;

    public Image myPokemonHealthBarUi;
    public Text myPokemonNameUi;
    public Text myPokemonLvlUi;


    
    void Start()
    {
        pokemonList = inventory.GetPokemonList();
        myPokemon =  new PokemonData(pokemonList.pokemonTeam[0]);
        initialPokemonPv = myPokemon.pv;
        myPokemonLvlUi.text = "lvl " + myPokemon.level.ToString();
        myPokemonNameUi.text = myPokemon.name;
        myAttacks = AttackDatabase.attacksByPokemon[myPokemon.name];
        enemyAttacks = AttackDatabase.attacksByPokemon[enemy.name];
        for(int i = 0; i < buttonText.Length; i++)
        {
            buttonText[i].text = myAttacks[i].GetAttackName();
        }
        enemyHealthUi.fillAmount = 100f;
        myPokemonHealthBarUi.fillAmount = 100f;

    }
    public void SetEnemy(PokemonData pokemon)
    {
        enemy = pokemon;
        intialEnemyPv = pokemon.pv;
    }

    private void AttackEnemy(AttackData attack)
    {
        float damageToEnemy = attack.CalculateDamage(enemy.level ,enemy.atk , enemy.def); 
        enemy.pv -= (int)damageToEnemy;
        enemyHealthUi.fillAmount = ((float)enemy.pv)/((float)intialEnemyPv);
        if(enemy.pv <= 0)
        {
            enemy.pv = 0;
            endMessagePanel.SetActive(true);
            endMessage.text = "Enemy defeated";
            Debug.Log($"enemy {enemy.name} defeated!");
        }
        Debug.Log($"{myPokemon.name} attack {enemy.name} with {attack.GetAttackName()} : {damageToEnemy} damage");
        Debug.Log($"enemy pv: {enemy.pv} myPv: {myPokemon.pv}");
        attackBar.gameObject.SetActive(false);
        if(enemy.pv > 0)
        {
            StartCoroutine(PokemonEnemyAttack(1f));
        }
    }

    private void EnemyAttack()
    {
         int randomIndex = Random.Range(0, enemyAttacks.Count);
         AttackData attack = enemyAttacks[randomIndex];
         float damageAttack = attack.CalculateDamage(myPokemon.level ,myPokemon.atk , myPokemon.def); 
         myPokemon.pv -= (int)damageAttack;
         myPokemonHealthBarUi.fillAmount = ((float)myPokemon.pv)/((float)initialPokemonPv);
         if(myPokemon.pv <= 0)
         {
             myPokemon.pv = 0;
             endMessagePanel.SetActive(true);
             attackBar.gameObject.SetActive(false);
             endMessage.text = "you've been defeated";
             Debug.Log($"my pokemon {myPokemon.name} perished in combat!");
         }else{
             StartCoroutine(showPanel(1f));
         }
         Debug.Log($"{enemy.name} attack {myPokemon.name} with {attack.GetAttackName()} : {damageAttack} damage");
         Debug.Log($"enemy pv: {enemy.pv} myPv: {myPokemon.pv}");
    }

    public void OnButtonClicked(Button clickedButton)
    {
        if (clickedButton.name == "Attack1")
        {
            AttackEnemy(myAttacks[0]);
        }
        else if (clickedButton.name == "Attack2")
        {
            AttackEnemy(myAttacks[1]);
        }
        else if (clickedButton.name == "Attack3")
        {
            AttackEnemy(myAttacks[2]);
        }
        else if (clickedButton.name == "Attack4")
        {
            AttackEnemy(myAttacks[3]);
        }
        else if (clickedButton.name == "UltimateAttack")
        {
           AttackEnemy(myAttacks[4]);
        }
    }
    IEnumerator PokemonEnemyAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnemyAttack();
    }
    IEnumerator showPanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        attackBar.gameObject.SetActive(true);
    }

    public void GoHome()
    {
        SceneManager.LoadScene("PokemonSafari");
    }




}
