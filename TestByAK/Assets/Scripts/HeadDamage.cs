using UnityEngine;
using UnityEngine.VFX;

public class HeadDamage : MonoBehaviour
{
    public VisualEffect bloodVFXPrefab; // Префаб эффекта крови через VFX Graph
    public AudioClip hitSound;          // Звук удара
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Получаем источник звука
    }

    // Метод получения удара: точка и направление
    public void TakeHit(Vector3 hitPoint, Vector3 hitDirection)
    {
        // Спавн эффекта крови
        if (bloodVFXPrefab)
        {
            VisualEffect vfx = Instantiate(bloodVFXPrefab, hitPoint, Quaternion.LookRotation(hitDirection));
            vfx.SetVector3("HitDirection", hitDirection); // Передаём направление удара
            vfx.Play();
        }

        // Воспроизведение звука
        if (hitSound && audioSource)
            audioSource.PlayOneShot(hitSound);

        // Тряска камеры
        FindObjectOfType<CameraShake>()?.TriggerShake();
    }
}