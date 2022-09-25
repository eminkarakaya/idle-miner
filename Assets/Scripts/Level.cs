using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour , IDataPersistence
{
    [System.Serializable]
    public struct IyilestirDatas{
        public int madenciSayisi;
        public float yurumeHizi;
        public float attackRate;
        public int iyilestirCost;
        public int cantaKapasitesi;
    }
    
    [SerializeField] public List<IyilestirDatas> levelIyilestirDatas;
    IyilestirPanel iyilestirPanel;
    public int unlockCost;
    [SerializeField] Text kasaText;
    [SerializeField] int kasa;
    public LevelManager manager;
    public int isciKapasitesi;
    public int level;
    public int levelLevel;
    public List<Miner> levelMiners;
    public int iyilestirCost;
    void Start()
    {
        iyilestirPanel = FindObjectOfType<IyilestirPanel>();
        iyilestirPanel.gameObject.GetComponent<Canvas>().enabled = false;
    }
    public void LoadData(GameData data)
    {
        levelLevel = data.levelLevel;
        manager = data.levelManager;
    }
    public void SaveData(ref GameData data)
    {
        data.levelLevel = levelLevel;
        data.levelManager = manager;
    }
    public void SetKasa(int value)
    {
        kasa = value;
        kasaText.text = kasa.ToString();
    }
    public int GetKasa()
    {
        return kasa;
    }
    public void LevelUpButton()
    {
        iyilestirPanel.gameObject.GetComponent<Canvas>().enabled = true;
        iyilestirPanel.level = this;
        iyilestirPanel.LoadIyılestirDatas();
    }
    
}
