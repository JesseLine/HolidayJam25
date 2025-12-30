using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float walkingSpeed = 10f;
    public float runningSpeed = 15f;

    private Rigidbody rb;
    private Vector3 moveVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * walkingSpeed;

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
