using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class DesktopFirstPersonController : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 4f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    public bool podeMover = true;
    public MuseumUIController uiController;

    [Header("Interação")]
    public float distanciaInteracao = 3f;
    public LayerMask camadaInteracao;
    public GameObject textoPressioneE;

    private CharacterController controller;
    private float verticalVelocity;
    private float cameraPitch = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (textoPressioneE != null)
        {
            textoPressioneE.SetActive(false);
        }
    }

    void Update()
    {
       if (podeMover)
        {
            Look();
            Move();
            HandleInteraction();
        }
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f);

        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        }
    }

    void Move()
    {
        float x = 0f;
        float z = 0f;

        if (Input.GetKey(KeyCode.W)) z = 1f;
        if (Input.GetKey(KeyCode.S)) z = -1f;
        if (Input.GetKey(KeyCode.A)) x = -1f;
        if (Input.GetKey(KeyCode.D)) x = 1f;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        Vector3 gravityMove = Vector3.up * verticalVelocity;
        controller.Move(gravityMove * Time.deltaTime);
    }

    void HandleInteraction()
{
    if (cameraTransform == null)
        return;

    Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, distanciaInteracao, camadaInteracao))
    {
        if (textoPressioneE != null)
            textoPressioneE.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractableObject obj = hit.collider.GetComponent<InteractableObject>();

            if (obj != null && uiController != null)
            {
                uiController.AbrirPainel(obj.titulo, obj.textoInformacao, obj.narracao);
            }
        }
    }
    else
    {
        if (textoPressioneE != null)
            textoPressioneE.SetActive(false);
    }
}
}