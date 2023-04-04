using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject player;
    private Transform Playertransform;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Playertransform = player.GetComponent<Transform>();
    }

    void Update() {
        GoToPlayer();
    }

    public void GoToPlayer() {
        transform.position = new Vector3(Playertransform.position.x, Playertransform.position.y, -10);
    }
}
