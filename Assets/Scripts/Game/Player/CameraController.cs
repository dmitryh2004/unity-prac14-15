using Unity.Netcode;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    [SerializeField] float edgeSize = 20f; // ширина зоны у кра€ экрана в пиксел€х
    [SerializeField] float moveSpeed = 10f; // скорость движени€ камеры

    [SerializeField] float xNegEdge = -20f, xPosEdge = 20f, zNegEdge = -20f, zPosEdge = 20f;

    void Update()
    {
        if (!IsOwner) return;
        Vector3 move = Vector3.zero;

        if (Input.mousePosition.x < edgeSize)
            move.x = -1;
        else if (Input.mousePosition.x > Screen.width - edgeSize)
            move.x = 1;

        if (Input.mousePosition.y < edgeSize)
            move.z = -1;
        else if (Input.mousePosition.y > Screen.height - edgeSize)
            move.z = 1;

        // ƒвижение камеры по направлению
        transform.position += move.normalized * moveSpeed * Time.deltaTime;

        float x = Mathf.Max(xNegEdge, Mathf.Min(transform.position.x, xPosEdge));
        float z = Mathf.Max(zNegEdge, Mathf.Min(transform.position.z, zPosEdge));

        transform.position = new Vector3(x, transform.position.y, z);
    }
}
