using TMPro;
using UnityEngine;

public class ElixirAccelerator : MonoBehaviour
{
    Color noOwnerColor = Color.white;
    Color team1Color = Color.red;
    Color team2Color = Color.blue;
    int owner = 0; // 0 - никто, другое - номер команды
    int influence = 0; // степень влияния. влияет на силу эффекта
    int maxInfluence = 10;
    [SerializeField] float maxElixirBoost = 0.5f; //максимальный баф к скорости появления эликсира

    MeshRenderer mr;

    [SerializeField] TMP_Text acceleratorText;

    [SerializeField] TownHall th1, th2;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        team1Color = th1.teamColor;
        team2Color = th2.teamColor;
    }
    private void Update()
    {
        switch (owner)
        {
            case 0:
                mr.material.color = noOwnerColor;
                break;
            case 1:
                mr.material.color = team1Color;
                break;
            case 2:
                mr.material.color = team2Color;
                break;
        }
    }

    public void AddInfluence(int team)
    {
        if (owner != team)
        {
            influence = 0;
            owner = team;
        }
        influence = Mathf.Min(influence + 1, maxInfluence);
        acceleratorText.SetText("" + influence + " / " + maxInfluence);
    }

    public int GetOwner()
    {
        return owner;
    }

    public int GetInfluence(int team)
    {
        return (owner == team) ? influence : 0;
    }

    public float GetElixirBoost()
    {
        return maxElixirBoost * influence / maxInfluence;
    }
}
