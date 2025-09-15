using UnityEngine;

public class folow_player : MonoBehaviour
{
    public Transform player;
    public Vector3 offsetCamera;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offsetCamera;
        
    }
}
