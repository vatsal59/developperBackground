using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    public Animator controler;
    public float speedMouvement = 9f;
    void Start()
    {
        if (PlayerPrefs.HasKey("xPosition") && PlayerPrefs.HasKey("yPosition") && PlayerPrefs.HasKey("zPosition"))
        {
            float x = PlayerPrefs.GetFloat("xPosition");
            float y = PlayerPrefs.GetFloat("yPosition");
            float z = PlayerPrefs.GetFloat("zPosition");

            transform.position = new Vector3(x, y, z);
        }
        controler.SetBool("isRunning" , false);
        
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            player.transform.forward = new Vector3(0f , 0f , 1f);
            controler.SetBool("isRunning" , true);
            UpdatePosition(player.transform.forward);
        }else if(Input.GetKey(KeyCode.W))
        {
            player.transform.forward = new Vector3(0f , 0f , -1f);
            controler.SetBool("isRunning" , true);
            UpdatePosition(player.transform.forward);
        }else if(Input.GetKey(KeyCode.A))
        {
            player.transform.forward = new Vector3(1f , 0f , 0f);
            controler.SetBool("isRunning" , true);
            UpdatePosition(player.transform.forward);
        }else if(Input.GetKey(KeyCode.D))
        {
            player.transform.forward = new Vector3(-1f , 0f , 0f);
            controler.SetBool("isRunning" , true);
            UpdatePosition(player.transform.forward);

        }else
        {
             controler.SetBool("isRunning" , false);
        }
        
    }
   
    void UpdatePosition(Vector3 direction)
    {
        Vector3 lastPosition = player.transform.position;
        float moveAmount = speedMouvement * Time.deltaTime;

        if (direction == new Vector3(1f, 0f, 0f))
        {
            Vector3 newPosition = lastPosition + new Vector3(moveAmount, 0f, 0f);
            player.transform.position = newPosition;
        }
        else if (direction == new Vector3(-1f, 0f, 0f))
        {
            Vector3 newPosition = lastPosition + new Vector3(-moveAmount, 0f, 0f);
            player.transform.position = newPosition;
        }
        else if (direction == new Vector3(0f, 0f, 1f))
        {
            Vector3 newPosition = lastPosition + new Vector3(0f, 0f, moveAmount);
            player.transform.position = newPosition;
        }
        else if (direction == new Vector3(0f, 0f, -1f))
        {
            Vector3 newPosition = lastPosition + new Vector3(0f, 0f, -moveAmount);
            player.transform.position = newPosition;
        }
    }

}
