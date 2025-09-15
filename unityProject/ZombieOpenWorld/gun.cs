using UnityEngine;

public class gun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject bullet;
    public Transform muzzle;
    public float bulletVelocity = 2000f;
    public GunData gunData;
    public PlayerMouvement playerMouvement;
    private float rotationSpeed = 50f;
    private Vector3 initialPosition;
    private Vector3 intialScale;

    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if(playerMouvement.getCurrentGun() != null && muzzle.transform.parent.name == playerMouvement.getCurrentGun().transform.name)
        {
        
            if(gunData.automatic)
            {
                if (Input.GetMouseButton(0))
                {
                    shoot();
                }
            }else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    shoot();
                }
            }
        }
        else{
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }


    private void shoot()
    {
        Vector3 randomOffset = new Vector3(
        Random.Range(-0.01f, 0.01f), 
        Random.Range(-0.01f, 0.01f),
        Random.Range(-0.01f, 0.01f)  
        );
        // muzzle.rotation pour aligner les bullets correctement
            GameObject bullets = Instantiate(bullet ,muzzle.position , muzzle.rotation);
            bullets.GetComponent<Rigidbody>().AddForce((muzzle.forward + randomOffset).normalized * bulletVelocity);
            bullets.GetComponent<BulletDamage>().setDamage(gunData.bulletDamage);
            Destroy(bullets, 3f);
        
    }
}
