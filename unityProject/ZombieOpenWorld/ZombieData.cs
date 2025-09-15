using UnityEngine;
using UnityEngine.UI;

public class ZombieData : MonoBehaviour
{
    private float health = 1000;
    private Animator animator;
    public ZombieManager manager;
    public GameObject zombieCanvas;
    public Vector3 offset = new Vector3(0f , 0f , -0.8f);
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            BulletDamage bulletData = collision.gameObject.GetComponent<BulletDamage>();
            float damage = bulletData != null ? bulletData.getDamage() : 5f; // Valeur par défaut au cas où
            health -= damage;
            GameObject canvasInstance = Instantiate(zombieCanvas);
            canvasInstance.transform.SetParent(transform);    
            canvasInstance.transform.position = collision.transform.position + offset;
            canvasInstance.transform.rotation = Camera.main.transform.rotation;
            Text text = canvasInstance.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = damage.ToString();
            }
            if(health <= 0)
            {
                manager.kill();
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
                animator.SetFloat("action" , 8);
                
                Destroy(gameObject , 3f);
            }
              Destroy(canvasInstance, 0.5f);
        }

    }
}

