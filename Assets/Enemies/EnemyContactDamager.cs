using Bullets;
using UnityEngine;

public class EnemyContactDamager : MonoBehaviour
{
   [SerializeField] private int playerLayer;
   [SerializeField] private float damageOnContact;
   [SerializeField] private float repulsion;
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.layer != playerLayer) return;
      
      other.gameObject.GetComponent<IDamageable>().TakeDamage(damageOnContact);
      
      var shipRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
      var positionDiff = new Vector2(shipRigidbody.position.x - transform.position.x,
         shipRigidbody.position.y - transform.position.y);
      shipRigidbody.velocity += positionDiff.normalized * repulsion;
   } 
}
