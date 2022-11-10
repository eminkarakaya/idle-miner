using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class GameManager : MonoBehaviour , IDataPersistence
{
    public GameObject levelPrefab;
    [SerializeField] public GameObject idleMoneyCanvas;
    [HideInInspector] public Text idleMoneyText;
    Button carpi;
    public Text levelManagerCanvasTitleText;
    public GameObject levelManagerAtamaCanvas;
    Banka banka;
    Asansor asansor;
    public GameObject unlockPrefab;
    public List<Level> allLevels;
    public int level;
    private static GameManager _instance;
    public static GameManager instance{
        get => _instance;
    }
    [SerializeField] private int nakit;
    [SerializeField] private Text nakitText;
    [SerializeField] private int superNakit;
    [SerializeField] private Text superNakitText;
    [SerializeField] private int bosNakit;
    [SerializeField] private Text bosNakitText;
    IdleMoney IdleMoney;
    
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        IdleMoney = GetComponent<IdleMoney>();
        idleMoneyText = idleMoneyCanvas.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        //String.Format("{0:0.00}", level.levelMiners[0].moveTime) + "s";
        asansor = FindObjectOfType<Asansor>();
        banka = FindObjectOfType<Banka>();
        unlockPrefab.transform.GetChild(0).GetComponent<TextMesh>().text = CaclText(allLevels[level+1].unlockCost);
        nakitText.text = CaclText(nakit);
        bosNakitText.text = CaclText((BirSaniyedeKazanilanGoldHesapla()/10)) + "/s";
        idleMoneyCanvas.GetComponent<Canvas>().enabled = true;
        idleMoneyText.text = CaclText(IdleMoney.GecenSureyiHesapla()*(BirSaniyedeKazanilanGoldHesapla()/10));
    }
    
    public void LoadData(GameData data)
    {
        allLevels[level].enabled = true;
        // if(data.isFirst)
        asansor = FindObjectOfType<Asansor>();
            asansor.activeLevels.Clear();
        //     return;
        nakit = data.nakit;
        level = data.level;
        for (int i = 0; i < level+1; i++)
        {
            allLevels[i].enabled = true;
            allLevels[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            // var obj =  Instantiate()
            allLevels[i].gameObject.SetActive(true);
            allLevels[i].kacinciLevel = i+1;
            if(allLevels[i].manager != null)
            {
                // allLevels[i].manager.GetComponent<AttackRateManager>()._level = i;
                // allLevels[i].manager.GetComponent<AttackRateManager>().level = allLevels[i];
            }
            asansor.activeLevels.Add(allLevels[i]);
        }
        unlockPrefab.transform.position = allLevels[level+1].transform.position;
        unlockPrefab.transform.GetChild(0).GetComponent<TextMesh>().text = CaclText(allLevels[level+1].unlockCost);
    }
    public void SaveData(ref GameData data)
    {
        data.nakit = nakit;
        data.level = level;
        Debug.Log(level);
        
    }
    public void SetGold(int count)
    {
        nakit += count;
        nakitText.text = CaclText(nakit);
        GoldAnim.instance.EarnGoldAnim2(count,5,banka.transform);
    }
    public int GetGold() => nakit;
    
    public void UnlockLevel()
    {
        if(nakit < allLevels[level+1].unlockCost)
        {
            
        }
        else
        {
            level ++;
            allLevels[level].enabled = true;
            allLevels[level].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            allLevels[level].kacinciLevel = level;
            asansor.activeLevels.Add(allLevels[level]);
            unlockPrefab.transform.position = allLevels[level+1].transform.position;
            unlockPrefab.transform.GetChild(0).GetComponent<TextMesh>().text = CaclText(allLevels[level+1].unlockCost);
            // allLevels[level].SetActiveLevel();
        }
    }
    public string CaclText(float value)
    {
        if(value < 1000)
        {
            return String.Format("{0:0.0}",value);
        }
        else if(value >= 1000 && value < 1000000)
        {
            return String.Format("{0:0.0}",value /1000 ) + "k";
        }
        else if(value >= 1000000 && value < 1000000000)
        {
            return String.Format("{0:0.0}",value /1000000) + "m";
        }
        else if(value >= 1000000000 && value < 1000000000000)
        { 
            return String.Format("{0:0.0}",value / 1000000000) + "b";
        }
        else if(value >= 1000000000000 && value < 1000000000000000)
        { 
            return String.Format("{0:0.0}",value / 1000000000000) + "t";
        }
        else if(value >= 1000000000000000 && value < 1000000000000000000)
        { 
            return String.Format("{0:0.0}",value / 1000000000000000) + "aa";
        }
        else if(value >= 1000000000000000000)
        { 
            return String.Format("{0:0.0}",value / 1000000000000000) + "ab";
        }
        return value.ToString();
    }
    public float BirSaniyedeKazanilanGoldHesapla()
    {
        List<float> kazanclar = new List<float>();
        float madenBirSaniyedeToplam = 0;
        for (int i = 0; i < asansor.activeLevels.Count; i++)
        {
            madenBirSaniyedeToplam += asansor.activeLevels[i].levelMiners[0].GetBirSaniyedeToplananMaden()*asansor.activeLevels[i].levelMiners.Count;
        }
        float bankaToplam =0;
        for (int i = 0; i < banka.paraTasiyicilar.Count; i++)
        {
            bankaToplam += banka.paraTasiyicilar[i].BankaBirSaniyedeToplananMaden();
        }
        kazanclar.Add(madenBirSaniyedeToplam);
        kazanclar.Add(bankaToplam);
        kazanclar.Add(asansor.AsansorBirSaniyedeToplananMaden());
        kazanclar.Sort();
        // for (int i = 0; i < kazanclar.Count; i++)
        // {
        //     Debug.Log(kazanclar[i]);
        // }
        return kazanclar[0];
    }
    public void IdleMoneyCanvasSetActive()
    {
        StartCoroutine(GoldAnim.instance.EarnGoldAnim((int)(IdleMoney.GecenSureyiHesapla()* (BirSaniyedeKazanilanGoldHesapla()/10)),20,idleMoneyText.transform));
        
    }
}