using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance; 

    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] List<Image> upgradesDisplay = new();
    [SerializeField] List<Image> activesUpgradesDisplay = new();

    [ColorUsageAttribute(true, true)]
    [SerializeField] List<Color> upgradesColors = new();

    [SerializeField] private int scoreSize;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateScore(int Score)
    {
        string scoreToDisplay = "";

        for (int i = 0; i < scoreSize - Score.ToString().Length; i++)
        {
            scoreToDisplay += "0";
        }

        scoreDisplay.text = scoreToDisplay + Score.ToString();
    }

    public void UpdateUpgrades(List<RewardTypes> upgrades)
    {
        foreach (Image display in upgradesDisplay)
        {
            display.enabled = false;
        }

        for (int i = 0;i < upgrades.Count;i++)
        {
            upgradesDisplay[i].enabled = true;
            upgradesDisplay[i].color = upgradesColors[(int)upgrades[i]];
        }
    }

    public void UpdateActiveUpgrades(Vector3 UpgradesTimers, Vector3 UpgradesDurations)
    {
        activesUpgradesDisplay[0].fillAmount = Mathf.Max(UpgradesTimers.x / UpgradesDurations.x, 0);
        activesUpgradesDisplay[1].fillAmount = Mathf.Max(UpgradesTimers.y / UpgradesDurations.y, 0);
        activesUpgradesDisplay[2].fillAmount = Mathf.Max(UpgradesTimers.z / UpgradesDurations.z, 0);
    } 
}
