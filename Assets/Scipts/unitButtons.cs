using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitButtons : MonoBehaviour
{
    private TooltipScript tooltipScript;
    [SerializeField] GameObject tooltip;
    [SerializeField] UnitData levyData;
    [SerializeField] UnitData archerData;
    [SerializeField] UnitData healerData;
    [SerializeField] UnitData knightData;
    [SerializeField] UnitData heavyInfData;
    private void Awake() {
        tooltipScript = tooltip.GetComponent<TooltipScript>();
    }
    private void Start() {
        transform.Find("LevyButton").GetComponent<ButtonScript>().MouseOverOnceFunc = () => tooltipScript.ShowTooltipStatic("Levy costs " + levyData.cost + "$ and " + levyData.manUsage + " men.");
        transform.Find("LevyButton").GetComponent<ButtonScript>().MouseOutOnceFunc = () => tooltipScript.HideTooltipStatic();
        transform.Find("ArcherButton").GetComponent<ButtonScript>().MouseOverOnceFunc = () => tooltipScript.ShowTooltipStatic("Archer costs " + archerData.cost + "$ and " + archerData.manUsage + " men.");
        transform.Find("ArcherButton").GetComponent<ButtonScript>().MouseOutOnceFunc = () => tooltipScript.HideTooltipStatic();
        transform.Find("KnightButton").GetComponent<ButtonScript>().MouseOverOnceFunc = () => tooltipScript.ShowTooltipStatic("Knight costs " + knightData.cost + "$ and " + knightData.manUsage + " men.");
        transform.Find("KnightButton").GetComponent<ButtonScript>().MouseOutOnceFunc = () => tooltipScript.HideTooltipStatic();
        transform.Find("HealerButton").GetComponent<ButtonScript>().MouseOverOnceFunc = () => tooltipScript.ShowTooltipStatic("Healer costs " + healerData.cost + "$ and " + healerData.manUsage + " men.");
        transform.Find("HealerButton").GetComponent<ButtonScript>().MouseOutOnceFunc = () => tooltipScript.HideTooltipStatic();
        transform.Find("HeavyInfantryButton").GetComponent<ButtonScript>().MouseOverOnceFunc = () => tooltipScript.ShowTooltipStatic("Heavy Infantry costs " + heavyInfData.cost + "$ and " + heavyInfData.manUsage + " men.");
        transform.Find("HeavyInfantryButton").GetComponent<ButtonScript>().MouseOutOnceFunc = () => tooltipScript.HideTooltipStatic();
    }
}
