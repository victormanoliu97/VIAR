using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {


    public float mouseSensivity = 5.0f;

    public float sprintMovementSpeed = 10.0f;

    float verticalRotation = 0;

    public float upDownRange = 60.0f;


    public float speed = 6.0f;

    public float jumpSpeed = 8.0f;

    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;


    


    // Use this for initialization
    void Start () {

        //Screen.lockCursor = true;
		
	}
	
	// Update is called once per frame
	void Update () {

        // Character Movement
        

        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            moveDirection = transform.TransformDirection(moveDirection);
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection = moveDirection * sprintMovementSpeed;
            }
            else
            {

                moveDirection = moveDirection * speed;
            }
            if (Input.GetButton("Jump"))
            {

                moveDirection.y = jumpSpeed;
            }

        }
        moveDirection.y = moveDirection.y -  gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);


        // Camera Movement

        bool firstPersonView = false;

        if(Input.GetKeyDown(KeyCode.V))
        {
            GameObject mainCamera = GameObject.FindWithTag("MainCamera");

            Vector3 poz = new Vector3(0f, 1f, 0f);

            mainCamera.transform.position = poz;

            firstPersonView = true;
        }
        
        
        if(Input.GetKeyDown(KeyCode.V) && firstPersonView == true)
        {
            GameObject mainCamera = GameObject.FindWithTag("MainCamera");

            Vector3 poz = new Vector3(0f, 1f, -10.8f);

            mainCamera.transform.position = poz;

            firstPersonView = true;

        }
        

        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensivity;

        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation = verticalRotation - Input.GetAxis("Mouse Y") * mouseSensivity;

        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        
        

	}
}
