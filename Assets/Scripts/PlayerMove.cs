using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rayDistance = 100f;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody rb;
    private Vector3 movement;
    private Quaternion targetRotation = Quaternion.identity;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        targetRotation = rb.rotation;
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    void Start()
    {

    }
    void Update()
    {
        AimTowardMouse();
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        movement = new Vector3(x, 0f, z).normalized;
    }
   
    void AimTowardMouse()
    {
        if (mainCamera == null)
        {
            return;
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, groundLayer))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0f;
            if(lookDir.sqrMagnitude > 0.01f)
            {
                targetRotation = Quaternion.LookRotation(lookDir);
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = movement * speed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        rb.MoveRotation(targetRotation);
    }
}
