using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Скорость движения

    void Update()
    {
        // Получаем ввод по осям
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Движение по плоскости
        Vector3 move = new Vector3(h, 0, v);
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }
}