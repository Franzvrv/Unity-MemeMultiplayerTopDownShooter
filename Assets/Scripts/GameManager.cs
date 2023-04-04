using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform playerTransform;
    public GameObject splashText;
    public GameObject boss;
    public GameObject transition;
    public SplashText splashTextScript;
    private int health = 100;
    private int lives = 3;
    private int dogeCoins = 0;
    public int zombieCount = 0;
    public bool canSpawn = true;
    void Awake()
    {
        if (!Instance) {
            Instance = this;
            StartGame();
        }
    }

    void Update() {
        if (GameObject.FindGameObjectWithTag("Player")) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    public void ReduceLife() {
        if (lives == 0) {
            StartCoroutine(TransOut(2));
        }
        else {
            lives -= 1;
            health = 100;
            playerTransform.position = new Vector3(0,0);
            Debug.Log("Player has died there are now " + GameManager.Instance.lives + " lives");
        }
    }

    public void WinScene()
    {
        StartCoroutine(TransOut(3));
    }

    public void ChangeHealth(int amount) {
        health = amount;
    }

    public int GetHealth() {
        return health;
    }

    public int GetLives() {
        return lives;
    }

    public int GetCoins() {
        return dogeCoins;
    }

    public void AddCoin() {
        dogeCoins += 1;
        switch(dogeCoins)
        {
            case 2:
                splashTextScript.ShowText("You hear an echo from a distance");
                break;
            case 4:
                splashTextScript.ShowText("You felt a headache as you strive");
                break;
            case 6:
                splashTextScript.ShowText("You feel a sharp pain with each step taken");
                break;
            case 8:
                splashTextScript.ShowText("The God of Shiba has awoken to take back what was once his, God help us all");
                PlayBoss();
                Instantiate(boss, new Vector3(0,0),Quaternion.identity);
                break;
        }
    }

    public void AddZombie() {
        zombieCount += 1;
    }

    public void RemoveZombie() {
        zombieCount -= 1;
    }

    IEnumerator TransIn() 
    {
        Image sprite = transition.GetComponent<Image>();
        sprite.color = new Vector4(0,0,0,1);
        yield return new WaitForSeconds(0.1f);
        while(sprite.color.a > 0) {
            sprite.color = new Vector4(0,0,0,sprite.color.a - 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    IEnumerator TransOut(int scene) 
    {
        Image sprite = transition.GetComponent<Image>();
        yield return new WaitForSeconds(0.1f);
        while(sprite.color.a < 1) {
            sprite.color = new Vector4(0,0,0,sprite.color.a + 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene(scene);
        yield break;
    }

    public void StartGame() {
        splashTextScript = splashText.GetComponent<SplashText>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        splashTextScript.ShowText("Collect all 8 Dogecoins");
        zombieCount = 0;
        health = 100;
        lives = 3;
        dogeCoins = 0;
        StartCoroutine(TransIn());
    }

    private void PlayBoss() {
        AudioSource bossMusic = GetComponentInChildren<AudioSource>();
        bossMusic.Play();
    }
}
