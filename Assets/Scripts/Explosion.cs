using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent(typeof(Player))) {
            Player player = other.GetComponent<Player>();
            player.DamagePlayer(20);
        }
        if (other.GetComponent(typeof(Enemy))) {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.DamageEnemy(10);
        }
    }
}
