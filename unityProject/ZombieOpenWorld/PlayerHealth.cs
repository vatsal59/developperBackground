using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public HealthBar healthBar;
    public Text text;
    private Vector3 initialPosition = new Vector3(0f , 2f , 0f);


    void Update()
    {
        if(health <= 0)
        {
            transform.position = initialPosition;
            health = 100;
            healthBar.SetHealth(health);
            text.text = health.ToString();
        }
    }

    public void PlayerDamage(int damage)
    {
        health = health - damage;
        healthBar.SetHealth(health);
        text.text = health.ToString();
    }
}
