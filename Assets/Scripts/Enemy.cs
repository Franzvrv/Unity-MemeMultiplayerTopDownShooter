using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    //Health of Enemy
    protected int health;
    protected Transform target;
    protected GameObject player;
    protected Rigidbody2D rb;
    protected AIPath aipath;

    //ReduceHealth is called when enemy is attacked in any way
    virtual public void DamageEnemy(int damage)
    {
        health -= damage;
        if (health <= 0) {
            KillEnemy();
        }
    }

    public void FollowPlayer() {
        if (target) {
            aipath.destination = target.position;
            aipath.canMove = true;
        }
        else {
            target = GameManager.Instance.playerTransform;
        }
    }

    //Function that makes enemy not alive
    virtual public void KillEnemy() {
        Destroy(this.gameObject);
        GameManager.Instance.RemoveZombie();
    }
}
