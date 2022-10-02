using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour , IDataPersistence
{
    public int madenciSayisi;
    public float yurumeHizi;
    public float attackRate;
    public int iyilestirCost;
    public int cantaKapasitesi;
    public Transform minerSpawnPoint;
    public int kacinciLevel;
    LevelAtamaPaneli levelAtamaPaneli;
    public Transform atanmisCvParent;
    public Transform managerPlace;
    IyilestirPanel iyilestirPanel;
    public int unlockCost;
    [SerializeField] Text kasaText;
    [SerializeField] int kasa;
    public Manager manager;
    public int isciKapasitesi;
    public int levelLevel;
    public List<Miner> levelMiners;
    void Start()
    {
        levelAtamaPaneli = FindObjectOfType<LevelAtamaPaneli>();
        iyilestirPanel = FindObjectOfType<IyilestirPanel>();   
        minerSpawnPoint = levelMiners[0].transform;
    }
    
    public void LoadData(GameData data)
    {
        levelLevel = data.levelLevel[kacinciLevel];
        manager = data.levelManager;
        for (int i = 0; i < data.levelMinerCount[kacinciLevel]-1; i++)
        {
            var obj = Instantiate(levelMiners[0].gameObject,new Vector3(levelMiners[0].kasaPoint.position.x,levelMiners[0].transform.position.y,levelMiners[0].kasaPoint.position.z),Quaternion.identity,levelMiners[0].transform.parent);
            levelMiners.Add(obj.GetComponent<Miner>());
        }
        
    }
    public void SaveData(ref GameData data)
    {
        data.levelMinerCount[kacinciLevel] = levelMiners.Count;
        data.levelLevel[kacinciLevel] = levelLevel;
        data.levelManager = manager;
    }
    public void SetKasa(int value)
    {
        kasa = value;
        kasaText.text = GameManager.instance.CaclText(kasa);
    }
    public void SetKasaTopla(int value)
    {
        kasa += value;
        kasaText.text = GameManager.instance.CaclText(kasa);
    }
    public int GetKasa()
    {
        return kasa;
    }
    public void LevelUpButton()
    {
        iyilestirPanel = FindObjectOfType<IyilestirPanel>(); 
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
        levelAtamaPaneli.gorevlendirilmisParent.GetChild(0).transform.localScale = Vector3.one;
    }
    public void Carpi()
    {
        if(levelAtamaPaneli.gorevlendirilmisManager != null)
            levelAtamaPaneli.gorevlendirilmisManager.transform.SetParent(GameManager.instance.allLevels[levelAtamaPaneli.chosenLevel].atanmisCvParent);
        GameManager.instance.levelManagerAtamaCanvas.GetComponent<Canvas>().enabled = false;
    }
}
