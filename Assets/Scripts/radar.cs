using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radar : MonoBehaviour
{
    public string targetTag = "YourTargetTag";
    private List<RevealerPoint> revealerPoints = new List<RevealerPoint>();

    private void Start()
    {
        // Collect all RevealerPoints in the scene
        RevealerPoint[] allRevealers = FindObjectsOfType<RevealerPoint>();
        revealerPoints.AddRange(allRevealers);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            // Perform your desired action here
            collision.SendMessage("FadeOutSpriteMask", SendMessageOptions.DontRequireReceiver);
        }
    }
}
