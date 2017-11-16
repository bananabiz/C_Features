using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitWithPanAndZoom : MonoBehaviour
{
    public Transform target; //Target object to orbit around
    public float panSpeed = 5f; //Speed of panning
    public float sensitivity = 1f; //Sensitivity of mouse

    //Minimum and maximum zoom distance
    public float distanceMin = 0.5f;
    public float distanceMax = 15f;

    private float distance = 0f; //Current distance between target and camera
    //Stored X & Y euler rotation
    private float x = 0.0f;
    private float y = 0.0f;

    //Create an enum to use for mouse input (just for readability)
    public enum MouseButton
    {
        LeftMouse = 0,
        RightMouse = 1,
        MiddleMouse = 2,
    }

    void Start()
    {
        //Call target transform's SetParent(null)
        target.transform.SetParent(null);
        //... Detaches the target from children
        
        //Set distance = Vector3.Distance(targets's position, transform's position)
        distance = Vector3.Distance(target.transform.position, transform.position);
        //... Calculates distance to target

        //Let angles = transform's eulerAngles
        Quaternion angles = Quaternion.Euler(transform.position);
        //Set x = angles.x
        x = angles.x;
        //Set y = angles.y
        y = angles.y;
        //... Records the current euler rotation

    }

    void Orbit()
    {
        //Set x = x + Input Axis "Mouse X" x sensitivity
        x = x + Input.GetAxis("Mouse X") * sensitivity;
        //Set y = y - Input Axis "Mouse Y" x sensitivity
        y = y - Input.GetAxis("Mouse Y") * sensitivity;
    }

    void Movement()
    {
        //If target != null
        if (target != null)
        {
            //Let rotation = Quaternion Euler(x, y, 0)
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            //Let desiredDist = distance - Input Axis "Mouse ScrollWheel"
            float desiredDist = distance - Input.GetAxis("Mouse ScrollWheel");

            //Set desiredDist = desiredDist x sensitivity
            desiredDist *= sensitivity;
            //... Amplifies desiredDist by sensitivity (Scroll Speed)

            //Set distance = Mathf Clamp (desiredDist, distanceMin, distanceMax);
            distance = Mathf.Clamp(desiredDist, distanceMin, distanceMax);
            //... Clamps the result so that distance doesn't go outside of constraints

            //Let invDistanceZ = new Vector3(0, 0, -distance)
            Vector3 invDistanceZ = new Vector3(0, 0, -distance);

            //Set invDistanceZ = rotation * invDistanceZ
            invDistanceZ = rotation * invDistanceZ;
            //... Rotates the direction of vector to be local to camera

            //Let position = target.position + invDistanceZ
            Vector3 position = target.position + invDistanceZ;

            //Set transform.rotation = rotation
            transform.rotation = rotation;
            //Set transform.position = position
            transform.position = position;
        }
    }

    //Moves the target using X and Y mouse coordinates to create panning effect
    void Pan()
    {
        //Let inputX = -Input GetAxis "Mouse X"
        float inputX = -Input.GetAxis("Mouse X");
        //Let inputY = -Input GetAxis "Mouse Y"
        float inputY = -Input.GetAxis("Mouse Y");

        //Let inputDir = new Vector3(inputX, inputY)
        Vector3 inputDir = new Vector3(inputX, inputY);

        //Let movement = transform.TransformDirection(inputDir)
        Vector3 movement = transform.TransformDirection(inputDir);
        //Set target.transform.position += movement x panSpeed x deltaTime
        target.transform.position += movement * panSpeed * Time.deltaTime;
    }

    //Hides/Unhides the cursor
    void HideCursor(bool isHiding)
    {
        //If isHiding
        if(isHiding)
        {
            //Lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            //Hide the cursor
            Cursor.visible = false;
        }
        //Else
        else
        {
            //Unlock the cursor
            Cursor.lockState = CursorLockMode.None;
            //Unhide the cursor
            Cursor.visible = true;
        }
    }
    
    void LateUpdate()
    {
        //If Input MouseButton Right
        if (Input.GetMouseButton((int)MouseButton.RightMouse))
        {
            //Call HideCursor(true) ... Hides the cursor
            HideCursor(true);
            //Call Orbit() ... Update orbit of the camera
            Orbit();
        }
        //Else if Input MouseButton Middle
        else if (Input.GetMouseButton((int)MouseButton.MiddleMouse))
        {
            //Call HideCursor(true)
            HideCursor(true);
            //Call Pan() ... Pans the camera
            Pan();
        }
        //Else
        else
        {
            //Call HideCursor(false) ... Unhides the cursor
            HideCursor(false);
        }

        //Call Movement() ... Always update movement regardless of any input
        Movement();
    }
}
