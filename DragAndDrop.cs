using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 startPosition;
    private PokemonData pokemon;
    private CanvasGroup canvasGroup;
    public bool valideZone = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        valideZone = false;
        startPosition = rectTransform.position;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if(!valideZone)
        {
            rectTransform.position = startPosition;
        }
    }
    public void SetupPokemon(PokemonData pokemonData)
    {
        pokemon = pokemonData;
    }
    public PokemonData GetDropPokemon()
    {
        return pokemon;
    }
}
