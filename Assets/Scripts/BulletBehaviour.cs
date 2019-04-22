using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject particleEffectPrefab;
    [SerializeField]
    private AudioClip explosion;
    private GameObject particleEffect;

    private void Start()
    {
        particleEffect = Instantiate(particleEffectPrefab, transform.position, transform.rotation);
        DisableParticleEffects();
    }
    void OnCollisionEnter(Collision collision)
    {
         AudioSource.PlayClipAtPoint(explosion, transform.position, 1);
         particleEffect.transform.position = transform.position;
         particleEffect.SetActive(true);
         transform.gameObject.SetActive(false);
         transform.position = transform.parent.position;
         Invoke("DisableParticleEffects", 1f);
    }

    private void DisableParticleEffects()
    {
        particleEffect.SetActive(false);
    }
}
