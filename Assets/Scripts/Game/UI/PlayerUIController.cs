using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Image bg;
    [SerializeField] TMP_Text elixirText;
    [SerializeField] Button spawn1, spawn2, spawn3;
    [SerializeField] Button spawnWall1, spawnWall2, spawnWall3;

    public void ChangeColor()
    {
        Color color = playerController.GetTeamColor();
        color.a = 0.3f;
        bg.color = color;
    }
    public void UpdateElixir(float elixir, float elixirSpeed)
    {
        elixirText.SetText("Ёликсир: " + elixir.ToString("0.0") + " (+" + elixirSpeed.ToString("0.00") + "/с)");

        spawn1.image.color = (elixir >= 1f) ? Color.white : Color.gray;
        spawn2.image.color = (elixir >= 1f) ? Color.white : Color.gray;
        spawn3.image.color = (elixir >= 1f) ? Color.white : Color.gray;

        spawnWall1.image.color = ((elixir >= 15f) && playerController.CanPlaceWall(0)) ? Color.white : Color.gray;
        spawnWall2.image.color = ((elixir >= 15f) && playerController.CanPlaceWall(1)) ? Color.white : Color.gray;
        spawnWall3.image.color = ((elixir >= 15f) && playerController.CanPlaceWall(2)) ? Color.white : Color.gray;
    }
}
