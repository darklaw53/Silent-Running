using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class treasurePickup : MonoBehaviour
{
    public string targetTag = "YourTargetTag";
    public TextMeshProUGUI scoreText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Destroy(collision.gameObject);
            scoreText.text = "" + (int.Parse(scoreText.text) + 25);
        }
    }
}