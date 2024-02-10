using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float horizontalInput;
    public float mouseWheelInput;
    private float cameraSpeed = 12;
    private float zoomSpeed = 5;
    private float mapSize;
    private float boundPadding = 5; // How far away can the camera wander off of the map
    [SerializeField] private GameObject playerBase;

    void Start()
    {
        mapSize = -playerBase.transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        horizontalInput = Input.GetAxis("Horizontal");
        transform.position = transform.position + new Vector3(0, GetComponent<Camera>().orthographicSize - transform.position.y -1, 0); //Adjust camera height to camera size
        if((horizontalInput > 0 && transform.position.x < mapSize - boundPadding) || (horizontalInput < 0 && transform.position.x > -mapSize + boundPadding)) { // Lets the camera move sideways if in bounds
            transform.position = transform.position + new Vector3(horizontalInput * Time.deltaTime * cameraSpeed, 0, 0);
        }
        //if ((horizontalInput > 0 && transform.position.x < mapSize - boundPadding) || (horizontalInput < 0 && transform.position.x > -mapSize + boundPadding)) { // Lets the camera move sideways if in bounds
        //    transform.position = transform.position + new Vector3(horizontalInput * Time.deltaTime * cameraSpeed, 0, 0);
        //}
    }
    private void LateUpdate() {
        if((GetComponent<Camera>().orthographicSize > 5 && mouseWheelInput > 0) || (GetComponent<Camera>().orthographicSize < 13.5 && mouseWheelInput < 0)) {
            GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize + -mouseWheelInput * zoomSpeed;
            //boundPadding = GetComponent<Camera>().orthographicSize * GetComponent<Camera>().orthographicSize;
        }
    }
}
