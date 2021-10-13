#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance { get; private set; }
    public int Knowledge { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}
