using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banka : MonoBehaviour ,IDataPersistence
{
    [SerializeField] Text bankaManagerCanvasTitleText;
    public GameObject bankaManagerCanvas;
    public List<ParaTasiyici> paraTasiyicilar;
    public YukcuManager manager;
    public GameObject _manager;
    [SerializeField] int gold;
    [SerializeField] Text goldText;
    BankaYoneticiAtama bankaYoneticiAtama;
    void Start()
    {
        bankaYoneticiAtama = FindObjectOfType<BankaYoneticiAtama>();
    }
    public void SetGold(int value)
    {  
        // gold += value;
        // goldText.text = gold.ToString();
        // StartCoroutine(GoldAnim.instance.EarnGoldAnim(value,5,this.transform));
        GoldAnim.instance.EarnGoldAnim2(value,5,this.transform);
    }
    public void LoadData(GameData data)
    {
        // manager = data.bankaManager;
    }
    public void SaveData(ref GameData data)
    {
        // data.bankaManager = manager;
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
