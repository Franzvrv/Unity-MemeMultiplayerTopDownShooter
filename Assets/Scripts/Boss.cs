using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class Boss : Enemy
{
    private SpriteRenderer sprite;
    ArrayList distances = new ArrayList();
    ArrayList speedUp = new ArrayList();
    private bool enraged = false;
    void Start()
    {
        health = 500;
        target = GameManager.Instance.playerTransform;
        sprite = GetComponent<SpriteRenderer>();
        aipath = GetComponent<AIPath>();
        aipath.maxSpeed = 4;
        aipath.canMove = true;
        BuildState();
        StartCoroutine(AlternateSpeed());
    }

    void BuildState() {
        this.distances.Add(6.4f);
        this.distances.Add(5.2f);
        this.distances.Add(6.5f);
        this.distances.Add(10.6f);
        this.distances.Add(20.7f);
        this.distances.Add(12.0f);
        this.distances.Add(8.0f);
        this.distances.Add(5.9f);
        this.distances.Add(7.7f);
        this.distances.Add(7.1f);
        this.speedUp.Add(true);
        this.speedUp.Add(false);
        this.speedUp.Add(false);
        this.speedUp.Add(true);
        this.speedUp.Add(true);
        this.speedUp.Add(false);
        this.speedUp.Add(true);
        this.speedUp.Add(false);
        this.speedUp.Add(true);
        this.speedUp.Add(false);
    }


    override public void KillEnemy() {
        Destroy(this.gameObject);
        GameManager.Instance.WinScene();
    }

    override public void DamageEnemy(int damage)
    {
        health -= damage;
        if (health <= 0) {
            KillEnemy();
        }
        else if (health < 250 && !enraged) {
            enraged = true;
        }
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

    IEnumerator AlternateSpeed() {
        while (this.gameObject) 
        {
            Transform playerTransform = GameManager.Instance.playerTransform;
            float distance = Mathf.Sqrt(Mathf.Pow(playerTransform.position.x - this.transform.position.x,2) + Mathf.Pow(playerTransform.position.y - this.transform.position.y,2));
            int trueAmount = 0;
            int falseAmount = 0;
            float averageDistanceFalse = 0;
            float averageDistanceTrue = 0;
            float varDistanceFalse = 0;
            float varDistanceTrue = 0;

            for(int i = 0; i < 10; i++) {
                float fdistance = (float) distances[i];
                if ((bool) speedUp[i]) {
                    trueAmount++;
                    averageDistanceTrue += fdistance;
                }
                else {
                    falseAmount++;
                    averageDistanceFalse += fdistance;
                }
            }
            averageDistanceTrue /= trueAmount;
            averageDistanceFalse /= falseAmount;

            for(int i = 0; i < 10; i++) {
                float fdistance = (float) distances[i];
                if ((bool) speedUp[i]) {
                    varDistanceTrue += Mathf.Pow(averageDistanceTrue - fdistance, 2);;
                }
                else {
                    varDistanceFalse += Mathf.Pow(averageDistanceFalse - fdistance, 2);;
                }
            }
            varDistanceTrue /= trueAmount;
            varDistanceFalse /= falseAmount;

            double posteriorDistanceTrue = 1/Mathf.Sqrt(((float) (2 * 3.14 * varDistanceTrue))) * Mathf.Pow(2.72f, Mathf.Pow(-(distance-averageDistanceTrue), 2) / (2 * varDistanceTrue));
            double posteriorDistanceFalse = 1/Mathf.Sqrt(((float) (2 * 3.14 * varDistanceFalse))) * Mathf.Pow(2.72f, Mathf.Pow(-(distance-averageDistanceFalse), 2) / (2 * varDistanceFalse));
            
            if (enraged) {
                if (posteriorDistanceTrue > posteriorDistanceFalse) {
                    sprite.color = new Vector4(1f, 0.6f, 0.6f, 1);
                    aipath.maxSpeed = 8;
                    distances.Add(distance);
                    speedUp.Add(true);
                    yield return new WaitForSeconds(2f);
                }
                else {
                    sprite.color = new Vector4(1f, 0.8f, 0.8f, 1);
                    aipath.maxSpeed = 2;
                    distances.Add(distance);
                    speedUp.Add(false);
                    yield return new WaitForSeconds(1f);
                }
                sprite.color = new Vector4(1f, 0.8f, 0.8f, 1);
                aipath.maxSpeed = 2;
                yield return new WaitForSeconds(1f);
            }
            else {
                if (posteriorDistanceTrue > posteriorDistanceFalse) {
                    aipath.maxSpeed = 5;
                    distances.Add(distance);
                    speedUp.Add(true);
                    yield return new WaitForSeconds(2f);
                }
                else {
                    aipath.maxSpeed = 3;
                    distances.Add(distance);
                    speedUp.Add(false);
                    yield return new WaitForSeconds(1f);
                }
                aipath.maxSpeed = 3;
                yield return new WaitForSeconds(1f);
            }
            distances.RemoveAt(0);
            speedUp.RemoveAt(0);
        }
        yield break;
    }
}
