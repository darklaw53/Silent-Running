using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public float startSize = 1f;
    public float endSize = 5f;
    public float expandDuration = 3f;
    public float fadeSpeed = 1f;
    public string targetTag = "YourTargetTag";
    public float sonarRange = 10f;

    private SpriteRenderer spriteRenderer;
    private List<RevealerPoint> revealerPoints = new List<RevealerPoint>();

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Collect all RevealerPoints in the scene
        RevealerPoint[] allRevealers = FindObjectsOfType<RevealerPoint>();
        revealerPoints.AddRange(allRevealers);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartSonarWave();
        }
    }

    public void StartSonarWave()
    {
        StartCoroutine(ExpandAndFadeRing());
    }

    private IEnumerator ExpandAndFadeRing()
    {
        float timer = 0f;

        // Expand the ring
        while (timer < expandDuration)
        {
            timer += Time.deltaTime;
            float currentSize = Mathf.Lerp(startSize, endSize, timer / expandDuration);
            transform.localScale = Vector3.one * currentSize;

            // Calculate the current range of the sonar wave
            float currentRange = sonarRange * (currentSize / endSize);

            // Sort the RevealerPoints based on their distance to the pulse center
            revealerPoints.Sort((a, b) =>
            {
                float distanceA = Vector2.Distance(transform.position, a.transform.position);
                float distanceB = Vector2.Distance(transform.position, b.transform.position);
                return distanceA.CompareTo(distanceB);
            });

            // Trigger fade-out in RevealerPoints within the sonar range
            foreach (RevealerPoint revealer in revealerPoints)
            {
                if (Vector2.Distance(transform.position, revealer.transform.position) <= currentRange)
                {
                    revealer.FadeOutSpriteMask();
                }
                else
                {
                    // Since the RevealerPoints are sorted by distance, we can break the loop if a revealer is outside the current range
                    break;
                }
            }

            yield return null;
        }

        // Fade out the ring
        while (spriteRenderer.color.a > 0f)
        {
            float newAlpha = spriteRenderer.color.a - fadeSpeed * Time.deltaTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, newAlpha);
            yield return null;
        }

        // Reset the size and color
        transform.localScale = Vector3.one * startSize;
        spriteRenderer.color = Color.white;
    }
}