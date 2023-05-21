using UnityEngine;

public class MaskingScript : MonoBehaviour
{
    public Material maskedSpriteMaterial; // Material of the masked sprite
    public Material maskingSpriteMaterial; // Material of the masking sprite

    private void Start()
    {
        // Set the masking sprite's material to render first
        GetComponent<Renderer>().material = maskingSpriteMaterial;
        GetComponent<Renderer>().material.renderQueue = 1000;

        // Set the masked sprite's material to render after the masking sprite
        Renderer maskedSpriteRenderer = GameObject.Find("MaskedSprite").GetComponent<Renderer>();
        maskedSpriteRenderer.material = maskedSpriteMaterial;
        maskedSpriteRenderer.material.renderQueue = 2000;

        // Configure stencil operations for the masked sprite
        maskedSpriteRenderer.material.SetInt("_StencilRef", 1);
        maskedSpriteRenderer.material.SetInt("_StencilReadMask", 1);
        maskedSpriteRenderer.material.SetInt("_StencilComp", (int)UnityEngine.Rendering.CompareFunction.Equal);
        maskedSpriteRenderer.material.SetInt("_StencilPass", (int)UnityEngine.Rendering.StencilOp.Replace);
    }
}