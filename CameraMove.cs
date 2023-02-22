using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //This script goes on the parent object called "CameraHolder"
    //Place the maincamera in the "cameraHolder"
    //create a child object on the player called "CameraPos" and put it in the camera possition 
    public Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
