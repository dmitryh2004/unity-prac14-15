using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float elixirCount = 0.0f;
    float maxElixirCount = 15f;
    [SerializeField] float baseElixirGenerationSpeed = 0.5f;
    float elixirGenerationSpeed;

    [SerializeField] List<ElixirAccelerator> accelerators;
    [SerializeField] PlayerUIController uiController;
    [SerializeField] TownHall townHall;
    [SerializeField] Color teamColor;

    public Color GetTeamColor()
    {
        return teamColor;
    }

    private void Start()
    {
        elixirGenerationSpeed = baseElixirGenerationSpeed;
    }

    private void Update()
    {
        RecalculateElixirGenerationSpeed();
        
        elixirCount += elixirGenerationSpeed * Time.deltaTime;
        elixirCount = Mathf.Min(elixirCount, maxElixirCount);

        uiController.UpdateElixir(GetElixirCount(), elixirGenerationSpeed);
    }

    public float GetElixirCount()
    {
        return elixirCount;
    }

    public void SpawnNPC(int spawnpointIndex)
    {
        if (elixirCount >= 1f)
        {
            elixirCount -= 1f;
            townHall.SpawnNPC(spawnpointIndex);
        }
    }

    public void RecalculateElixirGenerationSpeed()
    {
        elixirGenerationSpeed = baseElixirGenerationSpeed;
        foreach(ElixirAccelerator ea in accelerators)
        {
            elixirGenerationSpeed += ea.GetElixirBoost();
        }
    }
}
