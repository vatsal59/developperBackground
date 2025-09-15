using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public Transform transformCam;
    private Rigidbody rigid;
    public float forwardSpeed = 12f;
    public float rotationSpeed = 125f;
    public float jumpForce = 8f;
    public float gravityForce = -9.8f;
    private Vector3 axisY = new Vector3(0f ,1f , 0f);
    private Vector3 axisX = new Vector3(1f ,0f , 0f);
    private Vector3 axisZ = new Vector3(0f ,0f , 1f);
    private float verticalRotation = 1f;
    private GameObject currentGun = null;
    public Material glowingGunMaterial;
    public Material grayGunMaterial;
    public GameObject SpeakablePanel;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        // Empeche les force physique de rotate le perso , quand il bouge
        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationY;
        Cursor.lockState = CursorLockMode.Locked;

        
    }

    // Update is called once per frame
    void Update()
    {

        bool foundSpeakable = false;

        foreach (Collider col in Physics.OverlapSphere(transform.position, 3f))
        {
            if (col.CompareTag("Speakable"))
            {
                foundSpeakable = true;
                break; 
            }
        }
        SpeakablePanel.SetActive(foundSpeakable);
        
        foreach(Collider col in Physics.OverlapSphere(transform.position , 3f))
        {
            if(col.tag == "gun")
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (currentGun == null)
                    {
                        currentGun = col.gameObject;
                        MeshRenderer rend = currentGun.GetComponent<MeshRenderer>();
                        rend.material = grayGunMaterial;
                    }
                    else
                    {
                        float ancienPositionX = transform.position.x;
                        float ancienPositionZ = transform.position.z;
                        MeshRenderer rend = currentGun.GetComponent<MeshRenderer>();
                        rend.material = glowingGunMaterial;
                        currentGun.transform.SetParent(null);
                        currentGun.transform.position = new Vector3(ancienPositionX, 1.5f , ancienPositionZ);
                        currentGun.transform.LookAt(axisZ);
                        currentGun = col.gameObject;
                    }
                    col.transform.SetParent(Camera.main.transform);
                    Vector3 offset = new Vector3(0f , -0.2f , 1f);
                    col.transform.localPosition = offset;
                    col.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(0f, 180f, 0f);
                }
                break;
            }
        }

        // Rotation X et Y
            float mouseX = Input.GetAxis("Mouse X")*rotationSpeed * Time.deltaTime;
            transform.Rotate(mouseX*axisY);
            float mouseY = Input.GetAxis("Mouse Y")*rotationSpeed * Time.deltaTime;
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation , -90f , 90f);
            // quaternion , cest une rotation direct sans prendre en compte le truc prescedent -> empeche les gimbals locks
            transformCam.localRotation = Quaternion.Euler(verticalRotation , 0f , 0f);
        // direction de la mesh
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;
        // deplacement de la mesh
            Vector3 moveForward = Input.GetAxis("Vertical") * forward * forwardSpeed;
            Vector3 moveSides = Input.GetAxis("Horizontal") * right * forwardSpeed;
            Vector3 currentVelocity = rigid.linearVelocity;
            rigid.linearVelocity = new Vector3(moveForward.x + moveSides.x, currentVelocity.y, moveForward.z + moveSides.z);
        // jump
           if(IsGrounded())
           {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    rigid.AddForce(axisY * jumpForce , ForceMode.Impulse);
                }
           }
       // gravity
           if(rigid.linearVelocity.y < 0)
           {
               rigid.AddForce(axisY * gravityForce , ForceMode.Acceleration);
           }
    }
    public GameObject getCurrentGun()
    {
        return currentGun;
    }
    bool IsGrounded()
    {
        float distanceToGround = 2f; // ajuster selon taille personnage
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
    }

}
