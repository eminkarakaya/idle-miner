using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int unlockCost;
    [SerializeField] Text kasaText;
    [SerializeField] int kasa;
    public Manager manager;
    public int isciKapasitesi;
    public int level;
    public List<Miner> levelMiners;

    public void SetKasa(int value)
    {
        kasa = value;
        kasaText.text = kasa.ToString();
    }
    public int GetKasa()
    {
        return kasa;
    }
}
