using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] TMP_Text elixirText;
    [SerializeField] Button spawn1, spawn2, spawn3;

    public void UpdateElixir(float elixir)
    {
        elixirText.SetText("Ёликсир: " + elixir.ToString("0.0"));

        spawn1.image.color = (elixir >= 1f) ? Color.white : Color.gray;
        spawn2.image.color = (elixir >= 1f) ? Color.white : Color.gray;
        spawn3.image.color = (elixir >= 1f) ? Color.white : Color.gray;
    }
}
