using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RevealerPoint : MonoBehaviour
{
    public SpriteRenderer spriteMask; // Reference to the Sprite Mask component
    public float fadeDuration = 1f; // Duration of the fade effect

    private Coroutine fadeCoroutine; // Reference to the fade coroutine

    public void FadeOutSpriteMask()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        Color solid = new Color(0, 0, 0, 0);
        spriteMask.color = solid;
        fadeCoroutine = StartCoroutine(IEFadeOutSpriteMask());
    }

    private IEnumerator IEFadeOutSpriteMask()
    {
        Color tmpColor = spriteMask.color;

        while (tmpColor.a < 1)
        {
            tmpColor.a += Time.deltaTime / fadeDuration;
            spriteMask.color = tmpColor;

            if (tmpColor.a >= 1f)
            {
                tmpColor.a = 1f;
            }
            yield return null;
        }

        spriteMask.color = tmpColor;
    }
}