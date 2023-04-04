using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera cam;
    private Rigidbody2D rb;
    private int health = 100;
    private int speed = 5;
    private bool invincible = false;
    private SpriteRenderer sprite;

    Vector2 mousepos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime);

        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousepos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }

    public void DamagePlayer(int damage) {
        if (invincible == false) {
            health -= damage;
            GameManager.Instance.ChangeHealth(health);
            StartCoroutine(Invincible());
            if (health <= 0) {
                KillPlayer();
            }
        }

    }

    public void KillPlayer() {
        GameManager.Instance.ReduceLife();
        health = 100;
    }

    IEnumerator Invincible() {
        invincible = true;
        sprite.color = new Vector4(1f, 1f, 1f, 0.4f);
        yield return new WaitForSeconds(1);
        sprite.color = new Vector4(1f, 1f, 1f, 1f);
        invincible = false;
    }
}
