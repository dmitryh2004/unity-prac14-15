using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int id;
    float elixirCount = 0.0f;
    float maxElixirCount = 15f;
    [SerializeField] float baseElixirGenerationSpeed = 0.5f;
    float elixirGenerationSpeed;

    [SerializeField] List<ElixirAccelerator> accelerators;
    [SerializeField] PlayerUIController uiController;
    [SerializeField] TownHall townHall;
    [SerializeField] TownHall enemyTownHall;

    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject victoryScreen;

    [SerializeField] Color teamColor;

    public Color GetTeamColor()
    {
        return teamColor;
    }

    private void Start()
    {
        deathScreen.SetActive(false);
        victoryScreen.SetActive(false);
        elixirGenerationSpeed = baseElixirGenerationSpeed;
        uiController.ChangeColor();
    }

    public void SetTownHalls(TownHall th, TownHall eth)
    {
        townHall = th;
        enemyTownHall = eth;
        if (townHall)
            townHall.OnDeathEvent += TownHallDestroyed;
        if (enemyTownHall)
            enemyTownHall.OnDeathEvent += EnemyTownHallDestroyed;
    }

    public void SetEA(List<ElixirAccelerator> ea)
    {
        accelerators = new List<ElixirAccelerator>(ea);
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

    public bool CanPlaceWall(int index)
    {
        return !townHall.HasWall(index);
    }

    public void SpawnWall(int spawnpointIndex)
    {
        if ((elixirCount >= 15f) && (!townHall.HasWall(spawnpointIndex)))
        {
            elixirCount -= 15f;
            townHall.SpawnWall(spawnpointIndex);
        }
    }

    public void RecalculateElixirGenerationSpeed()
    {
        elixirGenerationSpeed = baseElixirGenerationSpeed;
        foreach(ElixirAccelerator ea in accelerators)
        {
            elixirGenerationSpeed += ea.GetElixirBoost(id);
        }
    }

    void TownHallDestroyed()
    {
        deathScreen.SetActive(true);
        enemyTownHall.OnDeathEvent -= EnemyTownHallDestroyed;
    }

    void EnemyTownHallDestroyed()
    {
        victoryScreen.SetActive(true);
        townHall.OnDeathEvent -= TownHallDestroyed;
    }
}
