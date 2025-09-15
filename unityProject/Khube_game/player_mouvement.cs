using UnityEngine;

public class player_mouvement : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float forwardForce = 250f;
    public float xAxisForce = 600f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    // on utilise le FixedUpdate pour les truc de physics a la place de 
    // update()
    void FixedUpdate()
    {
        // probleme de ordi a des TimeFrames different , donc on le resous de cette facons
        // avec le deltaTime
        rigidBody.AddForce(0, 0, forwardForce * Time.deltaTime);
        if(transform.position.y < -1f)
        {
            FindObjectOfType<GameManager>().gameOver();
        }
        else{

            if(Input.GetKey("d"))
            {
                rigidBody.AddForce(xAxisForce * Time.deltaTime, 0, 0 , ForceMode.VelocityChange);
            }
             if(Input.GetKey("a"))
            {
                rigidBody.AddForce(-xAxisForce * Time.deltaTime, 0, 0 , ForceMode.VelocityChange);
            }
        }
    }
}
