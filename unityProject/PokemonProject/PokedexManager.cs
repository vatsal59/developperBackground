using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class PokedexManager : MonoBehaviour
{
    [System.Serializable]
    public class PokemonDataSave{
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
        public PokemonDataSave(PokemonData pokemonData)
        {
            this.name = pokemonData.name;
            this.pv = pokemonData.pv;
            this.def = pokemonData.def;
            this.spDef = pokemonData.spDef;
            this.atk = pokemonData.atk;
            this.spAtk = pokemonData.spAtk;
            this.speed = pokemonData.speed;
            this.rarity = pokemonData.rarity;
            this.level = pokemonData.level;
            if(string.IsNullOrEmpty(pokemonData.pokemonId))
            {
                this.pokemonId = Guid.NewGuid().ToString();
            }
            else{
                this.pokemonId = pokemonData.pokemonId;
            }
        }
    }

    [System.Serializable]
    public class PokemonList{
        public List<PokemonDataSave> pokemonDataList = new List<PokemonDataSave>();
        public List<PokemonDataSave> pokemonTeam = new List<PokemonDataSave>();
    }
    public GameObject panel;
    public GameObject gridElement;
    public Text name;
    public Text rarity;
    public Text atk;
    public Text def;
    public Text pv;
    public Text spAtk;
    public Text spDef;
    public Text speed;
    public GameObject pokemon;
    public GameObject pokedex;
    public bool teamPanel;
    public TeamManager teamManager;
    private PokemonList currentInventory;
    private string pathToInventory;


    void Start()
    {
         pathToInventory = Application.persistentDataPath + "/save.json";
         if (File.Exists(pathToInventory)){
            string existingJson = File.ReadAllText(pathToInventory);
            currentInventory = JsonUtility.FromJson<PokemonList>(existingJson);
            if (currentInventory.pokemonDataList.Count > 0)
            {
                LoadInventory();
                SetInfoOnScreen(new PokemonData(currentInventory.pokemonDataList[0]));
            }
            else
            {
                Debug.Log("Aucun Pokémon dans l'inventaire.");
            }
         }else
         {
             Debug.Log("error no inventory");
         }
    }
    public void LoadInventory()
    {
        Debug.Log(currentInventory);
        for(int i = 0; i < currentInventory.pokemonDataList.Count;i++)
        {
            PokemonData pokemonData = new PokemonData(currentInventory.pokemonDataList[i]);
            GameObject newElement = CreateGridElement(pokemonData);
            if(teamPanel)
            {
                CreateTeamElement(newElement , pokemonData);
            }
        }
    }


    public void SetInfoOnScreen(PokemonData data)
    {
        name.text = data.name;
        pv.text = data.pv.ToString();
        atk.text = data.atk.ToString();
        def.text = data.def.ToString();
        spAtk.text = data.spAtk.ToString();
        spDef.text = data.spDef.ToString();
        speed.text = data.speed.ToString();
        rarity.text = data.rarity;
        string ressourceName = data.name + "Icon";
        Sprite sprite = Resources.Load<Sprite>("Sprites/" + data.name + "/" + ressourceName);
        pokemon.GetComponent<Image>().sprite = sprite;

    }
    public void ClosePokedexActive()
    {

        pokedex.SetActive(false);

    }
    public void OpenPokedexActive()
    {
        pokedex.SetActive(true);
    }

    public GameObject CreateGridElement(PokemonData pokemonData)
    {
        GameObject newElement = Instantiate(gridElement);
        newElement.transform.SetParent(panel.transform , false);
        string ressourceName = pokemonData.name + "Icon";
        Sprite sprite = Resources.Load<Sprite>("Sprites/" + pokemonData.name + "/" + ressourceName);
        newElement.GetComponent<Image>().sprite = sprite;
        ClickableIcon iconComponent = newElement.AddComponent<ClickableIcon>();
        iconComponent.SetupClickable(this , pokemonData);

        return newElement;
    }
    public void CreateTeamElement(GameObject newElement ,PokemonData pokemonData)
    {
        CanvasGroup canvasGroup = newElement.AddComponent<CanvasGroup>();
        DragAndDrop dragComponent = newElement.AddComponent<DragAndDrop>();
        dragComponent.SetupPokemon(pokemonData);
        // delete From availaible pokemon to drag
        for(int i = 0; i < teamManager.getTeam().Count; i++)
        {
            if(teamManager.getTeam()[i].pokemonId == pokemonData.pokemonId)
            {
                Destroy(newElement);
            }
        }
    }
}
