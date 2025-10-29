using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform weapon;
    public float pullAngle = 45f;     // Угол замаха вверх
    public float hitAngle = -30f;     // Угол удара ниже исходной
    public float returnSpeed = 12f;   // Скорость возврата
    private Quaternion originalRotation;
    private bool isPulling = false;
    private bool isReturning = false;

    void Start()
    {
        originalRotation = weapon.localRotation;
    }

    void Update()
    {
        // Замах вверх
        if (Input.GetMouseButtonDown(0))
        {
            isPulling = true;
            isReturning = false;
        }

        // Удар вниз
        if (Input.GetMouseButtonUp(0))
        {
            isPulling = false;

            // Мгновенно опускаем меч ниже исходной позиции
            weapon.localRotation = Quaternion.Euler(hitAngle, 0, 0);

            // Проверка попадания
            RaycastHit hit;
            if (Physics.Raycast(weapon.position, weapon.forward, out hit, 2f))
            {
                var damageable = hit.collider.GetComponent<HeadDamage>();
                if (damageable != null)
                {
                    Vector3 direction = weapon.forward * 10f;
                    damageable.TakeHit(hit.point, direction);
                }
            }

            // Запускаем возврат
            isReturning = true;
        }

        // Замах вверх (плавно)
        if (isPulling)
        {
            Quaternion targetRotation = Quaternion.Euler(-pullAngle, 0, 0);
            weapon.localRotation = Quaternion.Lerp(weapon.localRotation, targetRotation, Time.deltaTime * returnSpeed);
        }

        // Возврат к исходному положению (плавно)
        if (isReturning)
        {
            weapon.localRotation = Quaternion.Lerp(weapon.localRotation, originalRotation, Time.deltaTime * returnSpeed);

            if (Quaternion.Angle(weapon.localRotation, originalRotation) < 1f)
                isReturning = false;
        }
    }
}