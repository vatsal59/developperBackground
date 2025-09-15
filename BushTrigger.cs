using UnityEngine;
using UnityEngine.SceneManagement;


public class BushTrigger : MonoBehaviour
{
    private string sceneName = "PokemonBattle";
    public float chanceTeleportation = 0.20f;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Ca detecte trigger");
        if(collider.CompareTag("Player")){
            if(Random.value < chanceTeleportation)
            {
                float xPosition = collider.gameObject.transform.position.x;
                float yPosition = collider.gameObject.transform.position.y;
                float zPosition = collider.gameObject.transform.position.z;
                PlayerPrefs.SetFloat("xPosition" , xPosition);
                PlayerPrefs.SetFloat("yPosition" , yPosition);
                PlayerPrefs.SetFloat("zPosition" , zPosition);
                SceneManager.LoadScene(sceneName);
            }else
            {
                Debug.Log("aucun pokemon");
            }
        }

    }
    void Update()
    {
        
    }
}
