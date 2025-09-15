using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour , IDropHandler
{
    private DragAndDrop component;
    private PokemonData currentPokemon;
    public TeamManager teamManager;
    private GameObject currentUiSprite;
    private void Awake()
    {
        currentPokemon = null;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        DragAndDrop component = dropped.GetComponent<DragAndDrop>();
        if(currentPokemon == null)
        {
            component.valideZone = true;
            SetPokemonOnDropZone(component.GetDropPokemon() ,dropped);
            SetPositionToMiddle(dropped);
            currentUiSprite = dropped;
            teamManager.AddPokemonToTeam(currentPokemon);
            Destroy(dropped.GetComponent<DragAndDrop>());
        }else
        {
            Debug.Log("A Pokemon Is already in this position");
        }
    }

    public void SetPositionToMiddle(GameObject dropped)
    {
        float width = GetComponent<RectTransform>().rect.width;
        float height = GetComponent<RectTransform>().rect.height;
        dropped.GetComponent<RectTransform>().anchoredPosition = new Vector2(width/2 , -height/2);
    }
    public void SetPokemonOnDropZone(PokemonData pokemonOnDrop, GameObject pokemonSprite)
    {
        currentPokemon = pokemonOnDrop;
        currentUiSprite = pokemonSprite;
        GetComponentInChildren<Text>().text = pokemonOnDrop.name;
        pokemonSprite.transform.SetParent(transform);
        GameObject button = GetDeleteButton();
        button.SetActive(true);
    }

    public void DeleteFromTeam()
    {
        GameObject button = GetDeleteButton();
        button.SetActive(false);
        GetComponentInChildren<Text>().text = "Empty";
        Destroy(currentUiSprite);
        teamManager.DeletePokemonFromTeam(currentPokemon);
        currentPokemon = null;
        Debug.Log("pokemon deleted from team");

    }
    private GameObject GetDeleteButton()
    {
        foreach(Transform child in transform)
        {
            if(child.CompareTag("DeleteButton"))
            {
                GameObject button = child.gameObject;
                return button;
            }
        }
        return null;
    }
}
