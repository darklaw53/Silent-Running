using UnityEngine;

public class MoveHandler : MonoBehaviour
{
    public Transform mainMenu, treasures, settings;
    public GameObject cam;

    public Animator subAnim;

    bool movingToTreasure, movingToSettings, movingToMainT, movingToMainS, moving;

    public float movementSpeed = 2f;
    public AudioClip diveSound;
    public float diveSoundVolume = 1f;
    public AudioClip hoverSound;
    public float hoverSoundVolume = 1f;
    public AudioClip clickSound;
    public float clickSoundVolume = 1f;

    private AudioSource audioSource;

    private float t = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (movingToTreasure || movingToSettings || movingToMainT || movingToMainS)
        {
            t += Time.deltaTime * movementSpeed;

            if (movingToSettings)
            {
                cam.transform.position = Vector3.Lerp(mainMenu.position, treasures.position, t);
            }
            else if (movingToTreasure)
            {
                cam.transform.position = Vector3.Lerp(mainMenu.position, settings.position, t);
            }
            else if (movingToMainS)
            {
                cam.transform.position = Vector3.Lerp(treasures.position, mainMenu.position, t);
            }
            else if (movingToMainT)
            {
                cam.transform.position = Vector3.Lerp(settings.position, mainMenu.position, t);
            }

            if (t >= 1f)
            {
                t = 0f;
                movingToTreasure = false;
                movingToSettings = false;
                movingToMainT = false;
                movingToMainS = false;
                moving = false;
            }
        }
    }

    public void moveToTreasure()
    {
        PlayClick();
        if (!moving)
        {
            moving = true;
            movingToTreasure = true;
        }
    }

    public void moveToSettings()
    {
        PlayClick();
        if (!moving)
        {
            moving = true;
            movingToSettings = true;
        }
    }

    public void moveToMainMenuT()
    {
        PlayClick();
        if (!moving)
        {
            moving = true;
            movingToMainT = true;
        }
    }

    public void moveToMainMenuS()
    {
        PlayClick();
        if (!moving)
        {
            moving = true;
            movingToMainS = true;
        }
    }

    public void StartGame()
    {
        PlayClick();
        subAnim.speed = 0.1f;
        moving = true;
        audioSource.PlayOneShot(diveSound,diveSoundVolume);
    }

    public void PlayHover(){
        audioSource.PlayOneShot(hoverSound,hoverSoundVolume);
    }

    public void PlayClick(){
        audioSource.PlayOneShot(clickSound,clickSoundVolume);
    }
}
