using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
 private void Start()
 {
          GameObject Player = GameObject.FindGameObjectWithTag("Player");     
          Physics2D.IgnoreCollision(Player.GetComponent<CapsuleCollider2D>(), GetComponent<CapsuleCollider2D>());
 }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);

   GameObject Player = gameObject.GetComponent<GameObject>();

        if (collision.transform.GetComponent(typeof(Enemy)))
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            enemy.DamageEnemy(3);
        }
      
    }

    
}
