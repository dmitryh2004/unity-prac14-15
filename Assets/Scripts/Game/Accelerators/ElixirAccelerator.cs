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

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    public void UpdateColors()
    {
        team1Color = GameController.Instance.GetPlayerColor(1);
        team2Color = GameController.Instance.GetPlayerColor(2);
    }

    float MixColorComponent(float a, float b, float m)
    {
        return a + (b - a) * m;
    }

    Color BlendColor(int owner)
    {
        Color result = noOwnerColor;
        if (owner == 0) return result;

        Color blendingColor = (owner == 1) ? team1Color : team2Color;
        float mixFactor = (float)influence / maxInfluence;

        result.a = MixColorComponent(noOwnerColor.a, blendingColor.a, mixFactor);
        result.r = MixColorComponent(noOwnerColor.r, blendingColor.r, mixFactor);
        result.g = MixColorComponent(noOwnerColor.g, blendingColor.g, mixFactor);
        result.b = MixColorComponent(noOwnerColor.b, blendingColor.b, mixFactor);
        return result;
    }

    private void Update()
    {
        mr.material.color = BlendColor(owner);
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

    public float GetElixirBoost(int team)
    {
        return maxElixirBoost * GetInfluence(team) / maxInfluence;
    }
}
