using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NPC : Damagable
{
    Transform target;
    NavMeshAgent agent;
    List<Transform> waypoints;
    [SerializeField] int damage;
    [SerializeField] float speed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        InitHealth();
    }
    public void SetWaypoints(List<Transform> waypoints)
    {
        this.waypoints = new List<Transform>(waypoints);
        SelectNextTarget();
    }


    void SelectNextTarget()
    {
        if (agent == null)
        {
            if (!TryGetComponent<NavMeshAgent>(out agent))
            {
                Debug.Log(gameObject.name + ": unable to find navmeshagent");
            }
        }
        if (waypoints.Count == 0)
        {
            target = null; return;
        }
        target = waypoints[0];
        if (target == null) return;
        agent.SetDestination(target.position);
        waypoints.RemoveAt(0);
    }

    void Update()
    {
        if (target != null)
        {
            if (agent.remainingDistance < 0.25f)
            {
                SelectNextTarget();
            }
        }
        else
        {
            TakeDamage(GetMaxHealth());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + ": collision detected");
        Damagable targetDamagable;
        if (other.gameObject.TryGetComponent<Damagable>(out targetDamagable))
        {
            Debug.Log(gameObject.name + ": Collision with damagable detected");
            if (targetDamagable.GetTeam() != GetTeam())
            {
                Debug.Log(gameObject.name + " (team " + GetTeam() + "): collided with " + other.gameObject.name + " (team " + targetDamagable.GetTeam() + ")");
                targetDamagable.TakeDamage(damage);
            }
        }
    }
}
