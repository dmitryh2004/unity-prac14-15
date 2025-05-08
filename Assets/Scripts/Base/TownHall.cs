using UnityEngine;
using System.Collections.Generic;

[System.Serializable] public class Waypoints
{
    public List<Transform> waypoints;
}
public class TownHall : Damagable
{
    public Color teamColor;
    [SerializeField] Transform model;
    [SerializeField] GameObject npcPrefab;
    [SerializeField] GameObject wallPrefab;
    [SerializeField] List<Transform> spawnpoints;
    [SerializeField] FireController fireController;
    [SerializeField] HealthBarController healthBarController;
    [SerializeField] List<Waypoints> ways;
    List<GameObject> walls;
    [SerializeField] List<Transform> wallsSpawnpoints;

    private void Start()
    {
        InitHealth();
        walls = new List<GameObject>();
        walls.Add(null);
        walls.Add(null);
        walls.Add(null);
        teamColor = GameController.Instance.GetPlayerColor(GetTeam());
        for (int i = 0; i < model.childCount; i++)
        {
            model.GetChild(i).GetComponent<MeshRenderer>().material.color = teamColor;
        }
    }

    public void ClearWall(int index)
    {
        walls[index] = null;
    }

    public void SpawnWall(int index)
    {
        if (walls[index] == null)
        {
            GameObject wall = GameObject.Instantiate(wallPrefab, wallsSpawnpoints[index]);
            wall.GetComponent<DefensiveWall>().SetTownHall(this, index);
            wall.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = teamColor;
            walls[index] = wall;
        }
    }

    public bool HasWall(int index)
    {
        return walls[index] != null;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthBarController.UpdateHealth(GetHealth(), GetMaxHealth());
        fireController.UpdateFire((float) GetHealth() / GetMaxHealth());
    }

    public void SpawnNPC(int wayNumber)
    {
        GameObject npc = GameObject.Instantiate(npcPrefab, spawnpoints[wayNumber].position, Quaternion.Euler(0f, 0f, 0f), transform);
        NPC npcComp = npc.GetComponent<NPC>();
        npcComp.SetWaypoints(ways[wayNumber].waypoints);
        npcComp.SetTeam(GetTeam());
        MeshRenderer mr = npc.GetComponent<MeshRenderer>();
        mr.material.color = teamColor;
    }
}
