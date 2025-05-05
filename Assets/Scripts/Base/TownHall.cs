using UnityEngine;
using System.Collections.Generic;

[System.Serializable] public class Waypoints
{
    public List<Transform> waypoints;
}
public class TownHall : Damagable
{
    [SerializeField] GameObject npcPrefab;
    [SerializeField] List<Transform> spawnpoints;
    [SerializeField] FireController fireController;
    [SerializeField] List<Waypoints> ways;

    private void Start()
    {
        InitHealth();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        fireController.UpdateFire((float) GetHealth() / GetMaxHealth());
    }

    public void SpawnNPC(int wayNumber)
    {
        GameObject npc = GameObject.Instantiate(npcPrefab, spawnpoints[wayNumber].position, Quaternion.Euler(0f, 0f, 0f), transform);
        npc.GetComponent<NPC>().SetWaypoints(ways[wayNumber].waypoints);
    }
}
