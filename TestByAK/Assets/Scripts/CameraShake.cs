using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.2f;      // Длительность тряски
    public float magnitude = 0.1f;     // Сила тряски
    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition; // Сохраняем начальную позицию
    }

    public void TriggerShake()
    {
        StartCoroutine(Shake()); // Запускаем корутину тряски
    }

    IEnumerator Shake()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            // Случайное смещение камеры
            transform.localPosition = originalPos + Random.insideUnitSphere * magnitude;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos; // Возврат к исходной позиции
    }
}