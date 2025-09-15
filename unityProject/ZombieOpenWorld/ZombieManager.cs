using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float fieldOfSearch = 40f;
    private Animator animator;
    private bool died = false;
    private GameObject player;
    public int attack = 10;
    private float cooldownAttack = 10f;
    private float currentTime = 0f;
    public Material disolvingMat;
    private SkinnedMeshRenderer skinnedRenderer;
    void Start()
    {
         animator = GetComponent<Animator>();
         player = GameObject.Find("player");
         skinnedRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(died == false)
        {
            findPlayer();
            StickToGround();
        }
        
    }
    private void findPlayer()
    {
        foreach(Collider col in Physics.OverlapSphere(transform.position , 15f))
        {

            if(col.tag == "Player"){
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if(distance < 2f)
                {
                    animator.SetFloat("action" , 6);
                    Vector3 direction = col.transform.position - transform.position;
                    direction.y = 0;
                    transform.rotation = Quaternion.LookRotation(direction);
                    if(currentTime <= cooldownAttack)
                    {
                        currentTime++;

                    }else{
                        currentTime = 0;
                        Attackplayer();
                    }
                }else{
                    animator.SetFloat("action" , 4);
                    Vector3 direction = col.transform.position - transform.position;
                    direction.y = 0;
                    transform.rotation = Quaternion.LookRotation(direction);
                }
                break;
            }else{
                if(col.tag != "bullet")
                {
                    walking();
                }
            }
        }

    }
    
    private void walking()
    {
        
        foreach(Collider col in Physics.OverlapSphere(transform.position , 2f))
        {
            if(col.tag == "obstacle"){
                animator.SetFloat("action" , 2);
                Vector3 direction = transform.position - col.transform.position;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction);
            }else{
                animator.SetFloat("action" , 2);
            }
        }

    }
    public void kill()
    {
        died = true;
        if (skinnedRenderer != null)
        {
           skinnedRenderer.material = disolvingMat;
        }

    }
    void StickToGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 5f))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
    private void Attackplayer()
    {
        player.GetComponent<PlayerHealth>().PlayerDamage(attack);
    }
}
