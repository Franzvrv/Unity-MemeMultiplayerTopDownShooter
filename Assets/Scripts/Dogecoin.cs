using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dogecoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent(typeof(Player))) {
            GameManager.Instance.AddCoin();
            StartCoroutine(AlternateSpeed());
        }
    }

    IEnumerator AlternateSpeed() {
        AudioSource collectSound = this.GetComponent<AudioSource>();
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        collectSound.Play();
        Destroy(sprite);
        yield return new WaitForSeconds(collectSound.clip.length);
        Destroy(this.gameObject);
        yield break;
    }
}


