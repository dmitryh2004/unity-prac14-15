using TMPro;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void UpdateHealth(int health, int maxHealth)
    {
        healthText.SetText("" + health + " / " + maxHealth);
    }
}
