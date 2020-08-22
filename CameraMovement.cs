using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    private GameObject target;
    public float smoothing = 5f;

    Vector3 offset;
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        
        Debug.Log(target.transform);
        offset = new Vector3 (0,10,-10);
    }
    Vector3 cameraPosition ; 
    // Start is called before the first frame update
    private void LateUpdate() {
        target = GameObject.Find("PlayerA(Clone)");
        Debug.Log(target.transform);
        cameraPosition.x = target.transform.position.x + offsetX;
        cameraPosition.y = target.transform.position.y + offsetY;
        cameraPosition.z = target.transform.position.z + offsetZ;

        transform.position = cameraPosition;
        //Vector3 targetCamPos = target.position + offset;

          
        //transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
