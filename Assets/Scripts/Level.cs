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
    public int kacinciLevel;
    LevelAtamaPaneli levelAtamaPaneli;
    public Transform atanmisCvParent;
    [SerializeField] public List<IyilestirDatas> levelIyilestirDatas;
    public Transform managerPlace;
    IyilestirPanel iyilestirPanel;
    public int unlockCost;
    [SerializeField] Text kasaText;
    [SerializeField] int kasa;
    public Manager manager;
    public GameObject _manager;
    public int isciKapasitesi;
    public int levelLevel;
    public List<Miner> levelMiners;
    public int iyilestirCost;
    void Start()
    {
        levelAtamaPaneli = FindObjectOfType<LevelAtamaPaneli>();
        iyilestirPanel = FindObjectOfType<IyilestirPanel>();
        // iyilestirPanel.gameObject.GetComponent<Canvas>().enabled = false;
        // manager.transform.SetParent(GameManager.instance.allLevels[levelAtamaPaneli.chosenLevel].atanmisCvParent);
        // var obj = Instantiate(levelAtamaPaneli.managerPrefab,transform.position,Quaternion.identity,GameManager.instance.allLevels[levelAtamaPaneli.chosenLevel].atanmisCvParent);
        
    }
    
    public void LoadData(GameData data)
    {
        // if(data.isFirst)
        //     return;
        levelLevel = data.levelLevel[kacinciLevel];
        manager = data.levelManager;
    }
    public void SaveData(ref GameData data)
    {
        
        data.levelLevel[kacinciLevel] = levelLevel;
        data.levelManager = manager;
    }
    public void SetKasa(int value)
    {
        kasa = value;
        kasaText.text = kasa.ToString();
    }public void SetKasaTopla(int value)
    {
        kasa += value;
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
    public void LevelManagerCanvas()
    {
        if(GameManager.instance.allLevels[levelAtamaPaneli.chosenLevel].atanmisCvParent.childCount != 0 && GameManager.instance.allLevels[levelAtamaPaneli.chosenLevel].atanmisCvParent.GetChild(0).TryGetComponent(out Cv cv))
        {
            levelAtamaPaneli.gorevlendirilmisManager = cv;
        }
        GameManager.instance.levelManagerAtamaCanvas.GetComponent<Canvas>().enabled = true;
        atanmisCvParent.GetChild(0).transform.parent = levelAtamaPaneli.gorevlendirilmisParent;
    }
    public void Carpi()
    {
        if(levelAtamaPaneli.gorevlendirilmisManager != null)
            levelAtamaPaneli.gorevlendirilmisManager.transform.SetParent(GameManager.instance.allLevels[levelAtamaPaneli.chosenLevel].atanmisCvParent);
        GameManager.instance.levelManagerAtamaCanvas.GetComponent<Canvas>().enabled = false;
    }
}
