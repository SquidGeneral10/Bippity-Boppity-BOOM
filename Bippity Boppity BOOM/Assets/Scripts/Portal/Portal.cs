#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion
public class Portal : MonoBehaviour
{
    public enum Destination { MainMenu, Level1, Level2};
    [SerializeField] private Destination destination;
    private string sceneToLoad;
    private bool inZone; // Checks if the player is in the right slot for scene transitions.

    void Start() // Start is called before the first frame update
    {
        sceneToLoad = destination.ToString(); // Converts the enum's entries into strings - will only work if they share names with my scenes.
    } 

    void Update() // Update is called once per frame
    {
        if (Input.GetButton("Interact")) // Checks for you to press Xbox X / Playstation Square / Keyboard E before teleporting
        {
            if(inZone)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = false;
        }
    }
}
