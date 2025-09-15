using UnityEngine;
using System;
using Random = UnityEngine.Random; // <-- alias
[System.Serializable]
public class PokemonData
{
    public GameObject pokemon;
    public string name;
    public int pv;
    public int def;
    public int atk;
    public int spAtk;
    public int spDef;
    public int speed;
    public string rarity;
    public int level;
    public string pokemonId;


    public PokemonData(PokedexManager.PokemonDataSave pokemonDataSave)
    {
        this.name = pokemonDataSave.name;
        this.pv = pokemonDataSave.pv;
        this.def = pokemonDataSave.def;
        this.spDef = pokemonDataSave.spDef;
        this.atk = pokemonDataSave.atk;
        this.spAtk = pokemonDataSave.spAtk;
        this.speed = pokemonDataSave.speed;
        this.rarity = pokemonDataSave.rarity;
        this.level = pokemonDataSave.level;
        if(string.IsNullOrEmpty(pokemonDataSave.pokemonId))
        {
            this.pokemonId = Guid.NewGuid().ToString();
        }
        else{
            this.pokemonId = pokemonDataSave.pokemonId;
        }
    }

    public void CalculateStats()
    {
        pv = Random.Range(0 , 100) + (level * 10);    
        atk = Random.Range(0 , 100) + (level * 5);     
        def = Random.Range(0 , 100) + (level * 3); 
        spAtk = Random.Range(0 , 100) + (level * 5);
        spDef = Random.Range(0 , 100) + (level * 5);
        speed = Random.Range(0 , 100);
    }
}
