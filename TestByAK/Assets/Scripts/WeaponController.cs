using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform weapon;           // Объект оружия
    public float pullBackDistance = 0.5f; // Насколько оттягивается оружие
    public float hitRange = 2f;        // Дистанция удара
    public float hitForce = 10f;       // Сила удара
    private Vector3 originalPosition;
    private bool isPulling = false;

    void Start()
    {
        originalPosition = weapon.localPosition; // Сохраняем исходную позицию
    }

    void Update()
    {
        // Начало оттягивания
        if (Input.GetMouseButtonDown(0))
            isPulling = true;

        // Удар при отпускании
        if (Input.GetMouseButtonUp(0))
        {
            isPulling = false;
            weapon.localPosition = originalPosition;

            // Проверка попадания по цели
            RaycastHit hit;
            if (Physics.Raycast(weapon.position, weapon.forward, out hit, hitRange))
            {
                var damageable = hit.collider.GetComponent<HeadDamage>();
                if (damageable != null)
                {
                    Vector3 direction = weapon.forward * hitForce; // Направление удара
                    damageable.TakeHit(hit.point, direction);
                }
            }
        }

        // Оттягивание оружия назад
        if (isPulling)
        {
            weapon.localPosition = originalPosition - weapon.forward * pullBackDistance;
        }
    }
}