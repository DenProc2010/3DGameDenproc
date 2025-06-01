using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] private float walkSpeed = 8f;
    [SerializeField] private float runSpeed = 16f;
    [SerializeField] private float crouchSpeed = 3f;

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpPower = 6f;
    [SerializeField] private float gravity = 10f;

    [Header("Crouch Settings")]
    [SerializeField] private float defaultHeight = 5f;
    [SerializeField] private float crouchHeight = 1f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [SerializeField] private float _jumpForce;



    private bool _isGrounded = true;

    private Rigidbody _rb;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        // Курсор
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // напрямки руху
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // біг
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;

        // локальний напрям руху
        Vector3 moveDir = new
        (
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        // правильний напрям руху в ігровому світі
        Vector3 transformedDir = transform.TransformDirection(moveDir);
        _rb.velocity = new(
            transformedDir.x * speed,
            _rb.velocity.y,
            transformedDir.z * speed
        );

        // розрахунок швидкості по осях
        animator.SetFloat("Speed", _rb.velocity.magnitude);

        //стрибок
        if (Input.GetButton("Jump") && _isGrounded) StartCoroutine(nameof(Jump));
    }

    void OnCollisionEnter(Collision collision)
    {
        _isGrounded = collision.gameObject.CompareTag("Ground");
    }

    private IEnumerator Jump()
    {
        _isGrounded = false;
        animator.SetTrigger("Jump");
        yield return new WaitForSeconds(0.2f);
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}
