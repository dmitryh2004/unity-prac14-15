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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + ": collision detected");
        Damagable targetDamagable;
        if (collision.gameObject.TryGetComponent<Damagable>(out targetDamagable))
        {
            Debug.Log(gameObject.name + ": collided with " + collision.gameObject.name);
            targetDamagable.TakeDamage(damage);
        }
    }
}
