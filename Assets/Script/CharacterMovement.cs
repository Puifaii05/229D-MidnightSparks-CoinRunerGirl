using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;

    CharacterController characterController;
    public float speed = 6.0f;

    public float roatationSpeeed = 25;
    public float jumpSpeed = 7.5f;
    public float gravity = 20.0f;

    Vector3 inputVec;

    Vector3 targetDirection;
    private Vector3 moveDirection = Vector3.zero;

    //bool item
    public bool isItemDetect = false;
 
    //GameController
    GameController gameController;

    void Start()
    {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        // เชื่อม class
        if(gameController == null)
        {
            GameObject _tempGameController = GameObject.FindGameObjectWithTag("GameController") as GameObject;
            gameController = _tempGameController.GetComponent<GameController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = -(UnityEngine.Input.GetAxisRaw("Vertical"));
        float z = UnityEngine.Input.GetAxisRaw("Horizontal");
        inputVec = new Vector3(x, 0, z);


        animator.SetFloat("Input X", z);
        animator.SetFloat("Input Z", -(x));

        if (x != 0 || z != 0)
        {
            animator.SetBool("Moving", true);
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
        }

        //jump
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0.0f, UnityEngine.Input.GetAxis("Vertical"));
            moveDirection *= speed;
        }
        characterController.Move(moveDirection * Time.deltaTime);
        UpdateMovement();


        //Irem
        if (isItemDetect)
        {
            //กรณีเช็คว่า ItemStay เป็นจริง
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                animator.SetBool("Item", true);
                StartCoroutine(WaitForStanding(4.7f));
                gameController.GetCoin();
            }

        }

    }

    IEnumerator WaitForStanding(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("Item", false);
    }


    void UpdateMovement()
    {
        Vector3 motion = inputVec;
        motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1) ? .7f : 1;
        RotateTowardMovementDirection();
        getCameraRealtive();
    }


    void RotateTowardMovementDirection()
    {
        if (inputVec != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * roatationSpeeed);
        }
    }
    void getCameraRealtive()
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        float v = UnityEngine.Input.GetAxisRaw("Vertical");
        float h = UnityEngine.Input.GetAxisRaw("Horizontal");
        targetDirection = (h * right) + (v * forward);
    }


    //item
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "item")
        {
            isItemDetect = true;
            Debug.Log("True");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isItemDetect = false;
        Debug.Log("False");
    }
}
