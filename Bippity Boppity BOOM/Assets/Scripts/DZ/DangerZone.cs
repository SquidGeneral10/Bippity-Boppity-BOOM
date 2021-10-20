using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SingletonManager.Instance.InDangerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SingletonManager.Instance.InDangerZone = false;
        }
    }

    private bool inDangerZone;

    void Start() // Start is called before the first frame update
    {

    }

    void Update()     // Update is called once per frame
    {

    }

}
