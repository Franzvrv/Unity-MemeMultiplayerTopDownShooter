using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeEnemy : Enemy
{
    void Start()
    {
        health = 6;
        target = GameManager.Instance.playerTransform;
        aipath = GetComponent<AIPath>();
        aipath.maxSpeed = 5;
        aipath.canMove = true;
    }

    void Update() {
        FollowPlayer();
        if (aipath.desiredVelocity.x >= 0.01f) 
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aipath.desiredVelocity.x <= -0.01f) 
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } 
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.GetComponent(typeof(Player))) {
            Player player = other.GetComponent<Player>();
            player.DamagePlayer(10);
        }
    }
}
