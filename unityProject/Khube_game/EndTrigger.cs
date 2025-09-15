using UnityEngine;

public class EndTrigger : MonoBehaviour
{
	// les objets peuvent se traverser..difference entre le OnCollisionEnter
	public GameManager manager;
	void OnTriggerEnter()
	{
		manager.completeLevel();

	}

}
