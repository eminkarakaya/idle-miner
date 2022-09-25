using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banka : MonoBehaviour
{
    [SerializeField] Text bankaManagerCanvasTitleText;
    public GameObject bankaManagerCanvas;
    public Manager manager;
    [SerializeField] int gold;
    [SerializeField] Text goldText;

    public void SetGold(int value)
    {  
        // gold += value;
        // goldText.text = gold.ToString();
        // StartCoroutine(GoldAnim.instance.EarnGoldAnim(value,5,this.transform));
        GoldAnim.instance.EarnGoldAnim2(value,5,this.transform);
    }
    public int GetGold()
    {
        return gold;
    }
    public void BankaManagerCanvas()
    {
        bankaManagerCanvas.GetComponent<Canvas>().enabled = true;
        bankaManagerCanvasTitleText.text = "Asansor Yoneticisi";
    }
    public void Carpi()
    {
        bankaManagerCanvas.GetComponent<Canvas>().enabled = false;
    }
}
