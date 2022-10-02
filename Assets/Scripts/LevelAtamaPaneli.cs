using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAtamaPaneli : MonoBehaviour , IDataPersistence
{
    public List<Manager> levelManagers;
    public List<Sprite> levelManagerLevels;
    public GameObject cvPrefab;
    public Vector2Int carpanAraligi;
    public GameObject managerPrefab;
    string ozellikAdi;
    public Cv atanacakCv;
    public Manager atanacakManager;
    public Cv gorevlendirilmisManager;
    [SerializeField] Transform parent;
    public Transform gorevlendirilmisParent;
    public int chosenLevel;
    void Start()
    {
        // level = FindObjectOfType<Asansor>();
        if(GameManager.instance.allLevels[chosenLevel] != null)
        {
            if(gorevlendirilmisManager != null)
                gorevlendirilmisManager.manager = GameManager.instance.allLevels[chosenLevel].manager;
            GameManager.instance.levelManagerCanvasTitleText.text = "Level Yoneticisi " + chosenLevel;
        }
    }
    public void LoadData(GameData data)
    {    
        for (int i = 0; i < data.levelBeklemeSuresiTemp.Count; i++)
        {
            var cv = Instantiate(cvPrefab,transform.position,Quaternion.identity,parent);
            var manager = Instantiate(managerPrefab,new Vector3(-10,10,0),Quaternion.identity);
            levelManagers.Add(manager.GetComponent<AttackRateManager>());
            manager.GetComponent<AttackRateManager>().beklemeSuresiTemp = data.levelBeklemeSuresiTemp[i];
            manager.GetComponent<AttackRateManager>().deneyim = data.levelDeneyim[i];
            manager.GetComponent<AttackRateManager>()._level = data._level[i];
            if(manager.GetComponent<AttackRateManager>()._level != -1)
            {
                
                GameManager.instance.allLevels[manager.GetComponent<AttackRateManager>()._level].manager = manager.GetComponent<AttackRateManager>();
                manager.GetComponent<AttackRateManager>().transform.position = GameManager.instance.allLevels[manager.GetComponent<AttackRateManager>()._level].managerPlace.position;
                cv.transform.parent = GameManager.instance.allLevels[manager.GetComponent<AttackRateManager>()._level].atanmisCvParent;
            }
            manager.GetComponent<AttackRateManager>().kullanmaSuresi = data.levelKullanmaSuresi[i];
            manager.GetComponent<AttackRateManager>().managerSprite = data.levelManagerSprite[i];
            

            manager.GetComponent<AttackRateManager>().ozellikCarpani = data.levelOzellikCarpani[i];
            manager.GetComponent<AttackRateManager>().oldAttckrate = data.levelOldAttackRate[i];
            manager.GetComponent<AttackRateManager>().ozellikSprite = data.levelOzellikSprite[i];
            cv.GetComponent<Cv>().manager = manager.GetComponent<Manager>();
            
            // cv.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = data.levelOzellikSprite[i];
        }
        // Debug.Log(data.bankaManagerDatas[0].manager);
    }
    public void SaveData(ref GameData data)
    {
        data._level.Clear();
        data.levelBeklemeSuresiTemp.Clear();
        data.levelDeneyim.Clear();
        data.levelKullanmaSuresi.Clear();
        data.levelManagerSprite.Clear();
        data.levelOldAttackRate.Clear();
        data.levelOzellikCarpani.Clear();
        data.levelOzellikSprite.Clear();
        for (int i = 0; i < levelManagers.Count; i++)
        { 
            // newDatas.Add(newData);
            // Debug.Log(data.bankaManagerDatas[i].beklemeSuresiTemp);
            // data.bankaAtanmismi.Add(levelManagers[i].atanmismi);
            data.levelBeklemeSuresiTemp.Add(levelManagers[i].kullanmaSuresi);
            data.levelDeneyim.Add(levelManagers[i].deneyim);
            data.levelKullanmaSuresi.Add(levelManagers[i].kullanmaSuresi);
            data.levelManagerSprite.Add(levelManagers[i].managerSprite);
            data.levelOldAttackRate.Add(levelManagers[i].GetComponent<AttackRateManager>().oldAttckrate);
            data.levelOzellikCarpani.Add(levelManagers[i].ozellikCarpani);
            data.levelOzellikSprite.Add(levelManagers[i].ozellikSprite);
            data._level.Add(levelManagers[i].GetComponent<AttackRateManager>()._level);
        }
    }
    public void YoneticiIseAl()
    { 
        int _carpanAraligi = Random.Range(carpanAraligi.x,carpanAraligi.y);
        var obj = Instantiate(cvPrefab,Vector3.zero,Quaternion.identity,parent);
        var manager = Instantiate(managerPrefab,new Vector3(-10,10,0),Quaternion.identity);
        manager.GetComponent<Manager>().GetComponent<AttackRateManager>()._level = -1;
        obj.GetComponent<Cv>().manager = manager.GetComponent<Manager>();
        obj.GetComponent<Cv>().manager.SetOzellikCarpani(_carpanAraligi);
        obj.GetComponent<Cv>().manager.ozellikAdi = _carpanAraligi+"x AsansorHizi";
        // obj.GetComponent<Cv>().manager.ozellikSprite = ozellikAdi;
        if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Caylak)
        {
            obj.GetComponent<Cv>().manager.managerSprite = levelManagerLevels[0];
            obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = levelManagerLevels[0];
            obj.GetComponent<Cv>().manager.managerSprite = levelManagerLevels[0];
        }
        else if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Idareci)
        {
            obj.GetComponent<Cv>().manager.managerSprite = levelManagerLevels[2];
            obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = levelManagerLevels[2];
            obj.GetComponent<Cv>().manager.managerSprite = levelManagerLevels[2];
        }
        if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Kidemli)
        {
            obj.GetComponent<Cv>().manager.managerSprite = levelManagerLevels[1];
            obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = levelManagerLevels[1];
            obj.GetComponent<Cv>().manager.managerSprite = levelManagerLevels[1];
        }
        levelManagers.Add(obj.GetComponent<Cv>().manager);
        
    }
    public void Gorevlendir()
    {
        if(GameManager.instance.allLevels[chosenLevel].manager != null)
        {
            if(atanacakManager.TryGetComponent(out AttackRateManager attackRateManager))
            {
                attackRateManager._level = chosenLevel;
                attackRateManager.level = GameManager.instance.allLevels[attackRateManager._level];
            }
            if(gorevlendirilmisManager.manager.TryGetComponent(out AttackRateManager attackRateManager1))
            {
                attackRateManager1.level = null;
            }
            gorevlendirilmisManager.transform.SetParent(parent);
            // levelManagers.Remove(atanacakManager);
            levelManagers.Add(gorevlendirilmisManager.manager);
            atanacakCv.transform.SetParent(gorevlendirilmisParent);
            Vector3 oldPos = atanacakCv.transform.position;
            atanacakCv.manager.transform.position = GameManager.instance.allLevels[chosenLevel].managerPlace.position;
            gorevlendirilmisManager.manager.transform.position = oldPos;
            gorevlendirilmisManager = atanacakCv;
            GameManager.instance.allLevels[chosenLevel].manager.gameObject.SetActive(false);
            atanacakManager.gameObject.SetActive(true);
            GameManager.instance.allLevels[chosenLevel].manager = atanacakManager;
        }
        else
        {
            atanacakCv.transform.SetParent(gorevlendirilmisParent);
            atanacakCv.manager.transform.position = GameManager.instance.allLevels[chosenLevel].managerPlace.position;
            atanacakManager.gameObject.SetActive(true);
            GameManager.instance.allLevels[chosenLevel].manager = atanacakManager;
            gorevlendirilmisManager = atanacakCv;
            if(atanacakManager.TryGetComponent(out AttackRateManager attackRateManager))
            {
                attackRateManager._level = chosenLevel;
                attackRateManager.level = GameManager.instance.allLevels[attackRateManager._level];
            }
            // levelManagers.Remove(atanacakManager);
        }
    }
}
