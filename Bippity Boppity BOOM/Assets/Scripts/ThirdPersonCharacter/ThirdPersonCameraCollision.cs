#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class ThirdPersonCameraCollision : MonoBehaviour
{
    private float minDist = -1f; // The minimum distance away from the player that the camera can be.
    private float maxDist = 5f; // The furthest away from the player that the camera can be.
    [SerializeField] private float smoothing = 10f;
    private Vector3 dollyDirection;
    private float distance; // calcules the current distance between the player and camera.

    void Start() // Start is called before the first frame update
    {
        dollyDirection = transform.localPosition.normalized;
    }

    void Update() // Update is called once per frame
    {
        Vector3 desiredCamPos = transform.parent.TransformPoint(dollyDirection * maxDist);

        if(Physics.Linecast(transform.parent.position, desiredCamPos, out RaycastHit hit))
        {
            distance = Mathf.Clamp(hit.distance, minDist, maxDist) - 0.9f; // If the player moves close to a wall, the camera can go through them to prevent any jank.
        }
        else
        {
            distance = maxDist; // when the camera isn't up against any walls, it shouldn't be close to the player - this code makes sure of that.
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDirection * distance, smoothing * Time.deltaTime);
    }
}
