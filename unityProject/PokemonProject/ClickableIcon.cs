using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableIcon : MonoBehaviour , IPointerClickHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PokedexManager manager;
    private PokemonData pokemonData;

    public void SetupClickable(PokedexManager pokedexManager , PokemonData data)
    {
        manager = pokedexManager;
        pokemonData = data;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        manager.SetInfoOnScreen(pokemonData);

    }
}
