using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lives : MonoBehaviour
{
    private TMP_Text text;
    void Awake() 
    {
        text = this.GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = GameManager.Instance.GetLives() + " ";
    }
}
