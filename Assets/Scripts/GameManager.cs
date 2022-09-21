using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
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
        asansor = FindObjectOfType<Asansor>();
    }
    public void SetGold(int count)
    {
        nakit += count;
        nakitText.text = nakit.ToString();
        GoldAnim.instance.EarnGoldAnim2(count,5,banka.transform);
    }
    public void UnlockLevel()
    {
        level ++;
        allLevels[level].gameObject.SetActive(true);
        asansor.activeLevels.Add(allLevels[level]);
        unlockPrefab.transform.position = allLevels[level+1].transform.position;
        unlockPrefab.transform.GetChild(0).GetComponent<TextMesh>().text = allLevels[level+1].unlockCost.ToString();
        // allLevels[level].SetActiveLevel();
    }
}
