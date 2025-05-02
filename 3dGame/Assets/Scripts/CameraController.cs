using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera firstPersonCamera;   // Прив’яжи сюди камеру очей

    [Header("Look Settings")]
    public float lookSpeed = 2f;        // Чутливість миші
    public float maxUpAngle = 50f;       // Наскільки можна дивитися вгору
    public float maxDownAngle = 50f;     // Наскільки можна дивитися вниз

    private float _rotationX = 0f;       // Поточний кут по X (вертикаль)

    void Start()
    {
        // Активуємо тільки першу особу
        firstPersonCamera.gameObject.SetActive(true);

        // Блокуємо курсор
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 1) Обертання вздовж горизонталі (Y) — тіло гравця
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        transform.Rotate(Vector3.up * mouseX);

        // 2) Обертання по вертикалі (X) — камера
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -maxDownAngle, maxUpAngle);

        firstPersonCamera.transform.localEulerAngles = new Vector3(_rotationX, 0f, 0f);
    }
}
