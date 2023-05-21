using UnityEngine;

public class MoveHandler : MonoBehaviour
{
    public Transform mainMenu, treasures, settings;
    public GameObject cam;

    public Animator subAnim;

    bool movingToTreasure, movingToSettings, movingToMainT, movingToMainS, moving;

    public float movementSpeed = 2f;

    private float t = 0f;

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
        if (!moving)
        {
            moving = true;
            movingToTreasure = true;
        }
    }

    public void moveToSettings()
    {
        if (!moving)
        {
            moving = true;
            movingToSettings = true;
        }
    }

    public void moveToMainMenuT()
    {
        if (!moving)
        {
            moving = true;
            movingToMainT = true;
        }
    }

    public void moveToMainMenuS()
    {
        if (!moving)
        {
            moving = true;
            movingToMainS = true;
        }
    }

    public void StartGame()
    {
        subAnim.speed = 1;
        moving = true;
    }
}
