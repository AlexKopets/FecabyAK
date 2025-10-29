using UnityEngine;

public class HeadDamage : MonoBehaviour
{
    public ParticleSystem bloodParticlePrefab; 
    public AudioClip hitSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeHit(Vector3 hitPoint, Vector3 hitDirection)
    {
        // Спавн эффекта крови
        if (bloodParticlePrefab)
        {
            ParticleSystem ps = Instantiate(bloodParticlePrefab, hitPoint, Quaternion.LookRotation(hitDirection));
            ps.Play();
        }

        // Звук удара
        if (hitSound && audioSource)
            audioSource.PlayOneShot(hitSound);

        // Тряска камеры
        FindObjectOfType<CameraShake>()?.TriggerShake();
    }
}