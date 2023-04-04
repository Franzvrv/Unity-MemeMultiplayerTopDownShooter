using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class KamikazeEnemy : Enemy
{
    private GameObject explosion;
    private SpriteRenderer sprite;
    private Collider2D col;
    void Start()
    {
        health = 6;
        target = GameManager.Instance.playerTransform;
        aipath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        aipath.maxSpeed = 5;
        explosion = gameObject.transform.Find("Explosion").gameObject;
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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent(typeof(Player))) {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode() {
        aipath.canMove = false;
        rb.velocity = new Vector3(0, 0, 0);
        sprite.color = new Vector4(52,144,54,1);
        yield return new WaitForSeconds(1.5f);
        explosion.SetActive(true);
        Destroy(col);
        sprite.color = new Vector4(1,1,1,0);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        yield break;
    }
}
