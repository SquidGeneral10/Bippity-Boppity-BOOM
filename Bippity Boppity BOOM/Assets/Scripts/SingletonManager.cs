#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance { get; private set; }
    public int Knowledge { get; set; }
    public float Health { get; set; } = 1f;
    public bool InDangerZone { get; set; }

    private float alpha;

    [SerializeField] private Image woahDanger;
    [SerializeField] private Text healthText;


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

    private void Update()
    {
        LerpRedBorder();
    }
    private void LerpRedBorder()
    {
        if(InDangerZone)
        {
            Health -= 0.05f * Time.deltaTime;
            alpha += Time.deltaTime;
        }
        else
        {
            if (alpha > 0)
            {
                alpha -= 1f * Time.deltaTime;
            }
        }

        woahDanger.color = new Color(1, 1, 1, alpha);
    }
}
