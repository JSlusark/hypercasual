using UnityEngine;


/*
 * Used only in Inputmanager for now
 * 
 * T 's type is specified in the derivative class. Example:
 * InputManager : Singleton<InputManager>
 * Using T will allow us to not create duplicates for the same class type
 * and use this script as a base class for other managers (example for gamemanager)
 *
 * Also we make sure here that the derivative class is a monobehaviour
 * so we can use it as a component and attach it to a gameobject in the scene
 * and also use the unity lifecycle methods like awake, start, update etc...
 * 
 */
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance; //

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();// using any instead of first as in the awake we already manage on not creating multiples
            }
            return _instance;
        }
    }

    // virtual allows derivatives to override the function
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T; // ensures that the created instance is of type T (matches the type specified in the derivative class)
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}