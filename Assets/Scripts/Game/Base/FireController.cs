using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] ParticleSystem fire1, fire2, fire3;
    public void UpdateFire(float healthRatio)
    {
        fire1.gameObject.SetActive(healthRatio <= 0.75f);
        fire2.gameObject.SetActive(healthRatio <= 0.5f);
        fire3.gameObject.SetActive(healthRatio <= 0.25f);
    }
}
