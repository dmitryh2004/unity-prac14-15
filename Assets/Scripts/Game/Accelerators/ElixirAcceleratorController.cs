using UnityEngine;

public class ElixirAcceleratorController : MonoBehaviour
{
    [SerializeField] ElixirAccelerator accelerator;

    private void OnTriggerEnter(Collider other)
    {
        NPC npc;
        if (other.gameObject.TryGetComponent<NPC>(out npc))
        {
            accelerator.AddInfluence(npc.GetTeam());
        }
    }
}
