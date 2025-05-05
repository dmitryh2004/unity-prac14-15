using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float elixirCount = 0.0f;
    float maxElixirCount = 15f;
    [SerializeField] float elixirGenerationSpeed = 0.5f;

    private void Update()
    {
        elixirCount += elixirGenerationSpeed * Time.deltaTime;
        elixirCount = Mathf.Min(elixirCount, elixirGenerationSpeed);
    }

    public float GetElixirCount()
    {
        return elixirCount;
    }
}
