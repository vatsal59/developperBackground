using UnityEngine;

public class BulletDamage : MonoBehaviour
{
  private float bulletDamage;
  public void setDamage(float damage)
  {
	  bulletDamage = damage;
  }
  public float getDamage()
  {
	  return bulletDamage;
  }
}
