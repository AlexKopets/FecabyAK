using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;     // Ссылка на объект Player
    public float sensitivity = 100f; // Чувствительность мыши

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Прячем курсор и фиксируем в центре
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // Ограничиваем наклон вверх/вниз

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Вращаем камеру по X
        playerBody.Rotate(Vector3.up * mouseX); // Вращаем игрока по Y
    }
}