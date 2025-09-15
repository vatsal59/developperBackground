using UnityEngine;

public class player_collision : MonoBehaviour
{
	public player_mouvement mouvement;
	void OnCollisionEnter(Collision collisionInfo)
	{	
		if(collisionInfo.collider.tag == "obstacle")
		{
			Debug.Log(collisionInfo.collider.tag);
			mouvement.enabled = false;
			FindObjectOfType<GameManager>().gameOver();
		}

	}
}
