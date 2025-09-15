using UnityEngine;
using System.IO;
using System.Collections.Generic;



public class SaveInventory : MonoBehaviour
{
    private string path;

    private void Awake()
    {
        path = Application.persistentDataPath + "/save.json";
    }

    public void AddAndSaveToPokedex(PokemonData pokemonData)
    {
        PokedexManager.PokemonList pokemonList = GetPokemonList();
        PokedexManager.PokemonDataSave dataToSave = new PokedexManager.PokemonDataSave(pokemonData);
        pokemonList.pokemonDataList.Add(dataToSave);
        
        string message = "pokemon added to inventory";
        // pokemonList.pokemonDataList.Clear(); // TEST
        UpdateJsonSave(message , pokemonList);
    }

    public void SaveTeam(List<PokemonData> pokemons)
    {
        PokedexManager.PokemonList pokemonList = GetPokemonList();
        pokemonList.pokemonTeam.Clear();

        for(int i = 0; i < pokemons.Count; i++)
        {
            PokedexManager.PokemonDataSave dataToSave = new PokedexManager.PokemonDataSave(pokemons[i]);
            pokemonList.pokemonTeam.Add(dataToSave);
        }
        string message = "Team Saved";
        // pokemonList.pokemonTeam.Clear(); // TEST
        UpdateJsonSave(message , pokemonList);
    }
    public PokedexManager.PokemonList GetPokemonList()
    {
        PokedexManager.PokemonList pokemonList;
        if (File.Exists(path)){
            string existingJson = File.ReadAllText(path);
            pokemonList = JsonUtility.FromJson<PokedexManager.PokemonList>(existingJson);
            if (pokemonList == null)
                pokemonList = new PokedexManager.PokemonList();
        }
        else
        {
            pokemonList = new PokedexManager.PokemonList(); 
            Debug.Log("pokemonList empty");
        }
        return pokemonList;
    }

    private void UpdateJsonSave(string message , PokedexManager.PokemonList pokemonList )
    {
        string json = JsonUtility.ToJson(pokemonList, true);
        File.WriteAllText(path, json);
        Debug.Log(message);
    }
}
