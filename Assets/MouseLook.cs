using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSens = 100f;
    public Transform player;

    private float xRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //hide cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float inputY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= inputY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.localEulerAngles = Vector3.right * xRotation;

        player.Rotate(Vector3.up * inputX);
    }
}
