using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            // Check if an instance already exists
            if (instance == null)
            {
                // Search for an existing instance in the scene
                instance = FindObjectOfType<T>();

                // If no instance exists, create a new one
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    instance = singletonObject.AddComponent<T>();
                }

                // Make the instance persist across scene changes
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        // Check if another instance already exists
        if (instance != null && instance != this)
        {
            // Destroy the duplicate instance
            Destroy(gameObject);
        }
        else
        {
            // Set the instance if it's not already set
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}