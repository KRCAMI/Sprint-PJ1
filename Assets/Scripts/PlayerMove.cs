using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody rb;
    Vector3 movement;
    public LayerMask groundLayer;
    Quaternion targetRotation = Quaternion.identity;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetRotation = rb.rotation;
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            Vector3 lookDir = hit.point - rb.position;
            lookDir.y = 0f;
            if(lookDir.sqrMagnitude > 0f)
            {
                targetRotation = Quaternion.LookRotation(lookDir);
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        rb.MoveRotation(targetRotation);
    }
}
