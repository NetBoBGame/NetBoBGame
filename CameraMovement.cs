using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject character;
    public float offsetX;
    public float offsetY;
    public float offsetZ;

    Vector3 cameraPosition ; 
    // Start is called before the first frame update
    private void LateUpdate() {
        cameraPosition.x = character.transform.position.x + offsetX;
        cameraPosition.y = character.transform.position.y + offsetY;
        cameraPosition.z = character.transform.position.z + offsetZ;

        transform.position = cameraPosition;
    }
}
