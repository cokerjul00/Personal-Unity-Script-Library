using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    //put on main camera and set the sensitivity
    //assign orientation

    public float sensX = 5f;
    public float sensY = 5f;

    float xRotation;
    float yRotation;



    // Update is called once per frame
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        

    }

}
