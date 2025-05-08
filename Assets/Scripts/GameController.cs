using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController player1, player2;
    public static GameController Instance = null;

    public GameController()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public Color GetPlayerColor(int player)
    {
        if (player == 1) return player1.GetTeamColor(); else return player2.GetTeamColor();
    }
}
