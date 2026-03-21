using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class DesktopFirstPersonController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    private CharacterController controller;
    private float verticalVelocity;
    private float cameraPitch = 0f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        Look();
        Move();
    }
    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        verticalVelocity += gravity * Time.deltaTime;
        Vector3 gravityMove = Vector3.up * verticalVelocity;
        controller.Move(gravityMove * Time.deltaTime);
    }
}
