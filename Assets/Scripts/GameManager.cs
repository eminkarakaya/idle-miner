using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class GameManager : MonoBehaviour , IDataPersistence
{
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
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        banka = FindObjectOfType<Banka>();
        unlockPrefab.transform.GetChild(0).GetComponent<TextMesh>().text = CaclText(allLevels[level+1].unlockCost);
        nakitText.text = CaclText(nakit);
    }
    
    public void LoadData(GameData data)
    {
        
        // if(data.isFirst)
        asansor = FindObjectOfType<Asansor>();
            asansor.activeLevels.Clear();
        //     return;
        nakit = data.nakit;
        level = data.level;
        for (int i = 0; i < level+1; i++)
        {
            allLevels[i].gameObject.SetActive(true);
            allLevels[i].kacinciLevel = i;
            if(allLevels[i].manager != null)
            {
                allLevels[i].manager.GetComponent<AttackRateManager>()._level = i;
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
    }
    public void SetGold(int count)
    {
        nakit += count;
        nakitText.text = CaclText(nakit);
        GoldAnim.instance.EarnGoldAnim2(count,5,banka.transform);
    }
    public void UnlockLevel()
    {
        if(nakit < allLevels[level+1].unlockCost)
        {
            
        }
        else
        {
            level ++;
            allLevels[level].gameObject.SetActive(true);
            allLevels[level].kacinciLevel = level;
            asansor.activeLevels.Add(allLevels[level]);
            unlockPrefab.transform.position = allLevels[level+1].transform.position;
            unlockPrefab.transform.GetChild(0).GetComponent<TextMesh>().text = CaclText(allLevels[level+1].unlockCost);
            // allLevels[level].SetActiveLevel();
        }
    }
    public string CaclText(float value)
    {
        if(value >= 1000 && value < 1000000)
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
}
