using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public GameObject[] multipleEnemy; // array for multiple enemy
    public Transform closestEnemy;

    [SerializeField] private GameObject theCompanion;
    [SerializeField] private GameObject bulletprefab;
    private float bulletForce = 20f;
    [SerializeField] private Transform bulletspawn;
    [SerializeField] private float Timebetween;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float StartTimebetween = 1;
    private float speed = 5;
    private Transform target;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>(); //Rigibody for rotation

        Timebetween = StartTimebetween; //Time restart every start

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // The player that the companion follows
    }

    void Update()
    {

        closestEnemy = getClosestEnemy(); //To make Companion see who's enemy is the closest

        if (Vector2.Distance(transform.position, target.position) > 5)  //Range if player was far away Companion will follow
        {

            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime); //Following the player
        }

        if (Vector2.Distance(transform.position, closestEnemy.position) <= 15) // Range where Companion will shoot and look at the enemy.
        {

            Vector3 direction = closestEnemy.position - transform.position; // To rotate the Companion and aim for the Enemy
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;

            if (Timebetween < 0) // Time before shooting
            {
                SoundManager.Playsound("Bang");
                Shoot();
                Timebetween = StartTimebetween;
            }
        }

        Timebetween -= Time.deltaTime; // Time countdown

    }

    public Transform getClosestEnemy() //Code to see which enemy is the closest
    {

        multipleEnemy = GameObject.FindGameObjectsWithTag("Enemy"); // putting the enemy inside the array
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in multipleEnemy)
        {

            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistance < closestDistance)
            {

                closestDistance = currentDistance;
                trans = go.transform;
            }
        }

        return trans;

    }
    void Shoot() // Code for shooting
    {
        GameObject bullet = Instantiate(bulletprefab, bulletspawn.position, bulletspawn.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletspawn.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, 6);
    }
}
