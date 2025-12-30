using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float walkingSpeed = 10f;
    public float runningSpeed = 15f;

    private float speed;

    private CharacterController controller;
    private Vector3 moveVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runningSpeed;
        }
        else
        {
            speed = walkingSpeed;
        }
        controller.Move(move * speed * Time.deltaTime);
    }
}
