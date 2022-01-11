using Bullets;
using UnityEngine;
using ValueObjects;

// TODO : THIS CLASS dont work  
public class EnemyContactDamager : MonoBehaviour
{
   [SerializeField] private int playerLayer;
   [SerializeField] private Damage damageOnContact;
   [SerializeField] private float repulsion;
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.layer != playerLayer) return;
      
      other.gameObject.GetComponent<IDamageable>().TakeDamage(damageOnContact);
      
      // TODO: disable the movement of player
      var shipRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
      var position = shipRigidbody.position;
      var position1 = transform.position;
      var positionDiff = new Vector2(position.x - position1.x,
         position.y - position1.y);
      shipRigidbody.velocity += positionDiff.normalized * repulsion;
   } 
}
