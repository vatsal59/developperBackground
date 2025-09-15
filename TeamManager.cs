using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
public class TeamManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject teamSelectionPanel;
    private List<PokemonData> currentTeam = new List<PokemonData>();
    public SaveInventory saveInventory;
    public Image[] imageSlots = new Image[6];
    public PokedexManager pokedexManager;


    private void Start()
    {
        string pathToInventory = Application.persistentDataPath + "/save.json";
        if (File.Exists(pathToInventory)){
           string existingJson = File.ReadAllText(pathToInventory);
           PokedexManager.PokemonList list = JsonUtility.FromJson<PokedexManager.PokemonList>(existingJson);
           for(int i = 0; i < list.pokemonTeam.Count; i++)
           {
               PokemonData pokemon = new PokemonData(list.pokemonTeam[i]);
               currentTeam.Add(pokemon);
               LoadTeamUi(pokemon, i);
           }
        }
    }


    public void ClosePanel()
    {
        teamSelectionPanel.SetActive(false);
        SaveTeam();
    }
    public void OpenPanel()
    {
        teamSelectionPanel.SetActive(true);
    }

    public void AddPokemonToTeam(PokemonData pokemon)
    {
        currentTeam.Add(pokemon);
    }
    public void DeletePokemonFromTeam(PokemonData pokemon)
    {
        currentTeam.RemoveAll(x => x.pokemonId == pokemon.pokemonId);
        GameObject pokemonObject = pokedexManager.CreateGridElement(pokemon);
        pokedexManager.CreateTeamElement(pokemonObject , pokemon);
    }

    public void SaveTeam()
    {
        if(currentTeam.Count > 0)
        {
            saveInventory.SaveTeam(currentTeam);
        }
    }

    private void LoadTeamUi(PokemonData pokemon , int index)
    {
        string ressourceName = pokemon.name + "Icon";
        Sprite sprite = Resources.Load<Sprite>("Sprites/" + pokemon.name + "/" + ressourceName);
        GameObject image = new GameObject("pokemonSprite");
        Image imageComponent  = image.AddComponent<Image>();
        RectTransform rect = image.GetComponent<RectTransform>();

        rect.sizeDelta = new Vector2(99.3f*0.27f, 75.5f*0.47f);
        imageComponent.sprite = sprite;
        imageSlots[index].GetComponent<DropZone>().SetPokemonOnDropZone(pokemon , image);
        imageSlots[index].GetComponent<DropZone>().SetPositionToMiddle(image);
    }

    public  List<PokemonData> getTeam()
    {
        return currentTeam;
    }
}
