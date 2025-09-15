using UnityEngine;
using UnityEngine.UI;

public class SpriteHomeScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Sprite[] sprites;
    public Image panel;
    private int currentFrame = 0;
    private float timer = 0f;
    public float frameRate = 0.1f;
    public string spriteFolder = "Sprites/Ui/HomeScreenPokemon";
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>(spriteFolder);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % sprites.Length;
            panel.sprite = sprites[currentFrame];
        }


        
    }
}
