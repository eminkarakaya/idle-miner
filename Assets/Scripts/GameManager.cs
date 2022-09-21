using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    Asansor asansor;
    public GameObject unlockPrefab;
    public List<Level> allLevels;
    public int level;
    private static GameManager _instance;
    public static GameManager instance{
        get; private set;
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
        unlockPrefab.transform.GetChild(0).GetComponent<TextMesh>().text = allLevels[level+1].unlockCost.ToString();
        asansor = FindObjectOfType<Asansor>();
    }
    public void SetGold(int count)
    {
        nakit += count;
        nakitText.text = nakit.ToString();
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
