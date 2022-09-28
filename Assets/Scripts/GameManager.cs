using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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
        unlockPrefab.transform.GetChild(0).GetComponent<TextMesh>().text = allLevels[level+1].unlockCost.ToString();
        nakitText.text = nakit.ToString();
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
        unlockPrefab.transform.GetChild(0).GetComponent<TextMesh>().text = allLevels[level+1].unlockCost.ToString();
    }
    public void SaveData(ref GameData data)
    {
        
        data.nakit = nakit;
        data.level = level;
    }
    public void SetGold(int count)
    {
        nakit += count;
        nakitText.text = nakit.ToString();
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
            unlockPrefab.transform.GetChild(0).GetComponent<TextMesh>().text = allLevels[level+1].unlockCost.ToString();
            // allLevels[level].SetActiveLevel();
        }
    }
}
