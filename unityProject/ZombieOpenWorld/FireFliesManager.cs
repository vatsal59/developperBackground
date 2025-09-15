using UnityEngine;

public class FireFliesManager : MonoBehaviour
{
    private float intensityOfLight;
    private bool increasing = true;
    private float speed = 5f;
    private float timer = 0;
    private float speedForce = 4f;
    private Rigidbody rb;

    void Start()
    {
        intensityOfLight = 0f;
        GetComponent<Light>().intensity = 0f;
        rb = GetComponent<Rigidbody>();
        Collider playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
        Collider fireflyCollider = GetComponent<Collider>();

        Physics.IgnoreCollision(playerCollider, fireflyCollider);
    }

    void Update()
    {
        if (increasing)
        {
            intensityOfLight += speed*Time.deltaTime;
            GetComponent<Light>().intensity = intensityOfLight;

            if (intensityOfLight >= 20f)
            {
                increasing = false;
            }
        }
        else
        {
            intensityOfLight -= speed*Time.deltaTime;
            GetComponent<Light>().intensity = intensityOfLight;

            if (intensityOfLight <= 0f)
            {
                increasing = true;
            }
        }

        timer += speed*Time.deltaTime;
        if(timer >= 40)
        {
            Vector3 force = (Vector3.right + Vector3.up) * speedForce;
            rb.AddForce(force, ForceMode.Acceleration);
            transform.Rotate(Vector3.up * 90f * Time.deltaTime);
            timer = 0;
            ControlSpeed();
        }
        VerifyCollision();
    }


    private void VerifyCollision()
    {
        Ray ray = new Ray(transform.position , Vector3.down);
        RaycastHit hit;
        if(!Physics.Raycast(ray, out hit, 3.5f))
        {
            rb.AddForce(Vector3.down * speedForce/2, ForceMode.Acceleration);
            ControlSpeed();
        }
        ray = new Ray(transform.position , transform.forward);
        if(Physics.Raycast(ray, out hit, 2f))
        {
            rb.AddForce(-transform.forward * speedForce, ForceMode.Acceleration);
            ControlSpeed();
        }
    }

    private void ControlSpeed()
    {
        float maxSpeed = 2f;
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
    }
}
