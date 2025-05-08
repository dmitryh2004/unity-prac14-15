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
    [SerializeField] List<Transform> spawnpoints;
    [SerializeField] FireController fireController;
    [SerializeField] List<Waypoints> ways;

    private void Start()
    {
        InitHealth();
        for (int i = 0; i < model.childCount; i++)
        {
            model.GetChild(i).GetComponent<MeshRenderer>().material.color = teamColor;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
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
