using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
public class PokemonManager : MonoBehaviour
{
    public SaveInventory saveInventory;
    public CombatManager combatManager;
    public PokemonData[] pokemons;
    public Vector3 startPosition = new Vector3(1.69f , 0.88f , -2.51f);
    public Text levelText;
    public Text NameText;
    private PokemonData currentPokemon;

    void Start()
    {
        int lenghtTab = pokemons.Length;
        int index = Random.Range(0 , lenghtTab); 
        pokemons[index].CalculateStats();
        levelText.text = "Lvl " + pokemons[index].level.ToString();
        NameText.text = pokemons[index].name;
        GameObject obj = Instantiate(pokemons[index].pokemon, startPosition, Quaternion.identity);
        currentPokemon = pokemons[index];
        combatManager.SetEnemy(pokemons[index]);
    }

    public void CatchPokemon()
    {
        if(Random.value < 0.50)
        {
            if(currentPokemon != null)
            {
                Debug.Log("pokemon attraper");
                saveInventory.AddAndSaveToPokedex(currentPokemon);
            }
        }else
        {
            Debug.Log("cest rater , essaie encore !");
        }

    }
}
