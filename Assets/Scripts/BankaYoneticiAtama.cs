using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankaYoneticiAtama : MonoBehaviour, IDataPersistence
{
    public int bankaManagerInt;
    public List<YukcuManager> bankaManagers;
    public List<Sprite> bankaManagerLevels;
    public GameObject cvPrefab;
    public string ozellikAdi;
    public GameObject managerPrefab;
    public Vector2Int carpanAraligi;
    public Vector2Int beklemeSuresiAraligi;
    public Transform bankaManagerTransform;
    public Cv atanacakCv;
    public YukcuManager atanacakManager;
    public Cv gorevlendirilmisManager;
    Banka banka;
    [SerializeField] Transform parent;
    public Transform gorevlendirilmisParent;
    void Start()
    {

    }
    public void LoadData(GameData data)
    {    
        banka = FindObjectOfType<Banka>();
        // if(data.isFirst)
        //     return;
        for (int i = 0; i < data.bankaBeklemeSuresiTemp.Count; i++)
        {
            var cv = Instantiate(cvPrefab,transform.position,Quaternion.identity,parent);
            var manager = Instantiate(managerPrefab,new Vector3(-10,10,0),Quaternion.identity);
            bankaManagers.Add(manager.GetComponent<YukcuManager>());
            // bankaManagers[i] = data.bankaManagerDatas[i].manager.GetComponent<YukcuManager>();
            manager.GetComponent<YukcuManager>().beklemeSuresiTemp = data.bankaBeklemeSuresiTemp[i];
            manager.GetComponent<YukcuManager>().deneyim = data.bankaDeneyim[i];
            manager.GetComponent<YukcuManager>().atanmismi = data.bankaAtanmismi[i];
            if(manager.GetComponent<YukcuManager>().atanmismi)
            {
                banka.manager = manager.GetComponent<YukcuManager>();
                banka.manager.transform.position = bankaManagerTransform.position;
                cv.transform.SetParent(gorevlendirilmisParent);
                gorevlendirilmisManager = cv.GetComponent<Cv>();
                cv.transform.localScale = Vector3.one;
            }

            // _manager.isim = data.bankaManagerDatas[i].;
            manager.GetComponent<YukcuManager>().kullanmaSuresi = data.bankaKullanmaSuresi[i];
            manager.GetComponent<YukcuManager>().managerSprite = data.bankaManagerSprite[i];
            manager.GetComponent<YukcuManager>().ozellikCarpani = data.bankaOzellikCarpani[i];
            manager.GetComponent<YukcuManager>().oldKapasite = data.bankaOldKapasite[i];
            manager.GetComponent<YukcuManager>().ozellikSprite = data.bankaOzellikSprite[i];
            cv.GetComponent<Cv>().manager = manager.GetComponent<Manager>();
            cv.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = data.bankaManagerSprite[i];
        }
        // Debug.Log(data.bankaManagerDatas[0].manager);
    }
    public void SaveData(ref GameData data)
    {
        data.bankaManagerDataCount = bankaManagers.Count;
        data.bankaBeklemeSuresiTemp.Clear();
        data.bankaDeneyim.Clear();
        data.bankaKullanmaSuresi.Clear();
        data.bankaManagerSprite.Clear();
        data.bankaOldKapasite.Clear();
        data.bankaOzellikCarpani.Clear();
        data.bankaOzellikSprite.Clear();
        data.bankaAtanmismi.Clear();
        for (int i = 0; i < bankaManagers.Count; i++)
        { 
            // newDatas.Add(newData);
            // Debug.Log(data.bankaManagerDatas[i].beklemeSuresiTemp);
            data.bankaAtanmismi.Add(bankaManagers[i].atanmismi);
            data.bankaBeklemeSuresiTemp.Add(bankaManagers[i].kullanmaSuresi);
            data.bankaDeneyim.Add(bankaManagers[i].deneyim);
            data.bankaKullanmaSuresi.Add(bankaManagers[i].kullanmaSuresi);
            data.bankaManagerSprite.Add(bankaManagers[i].managerSprite);
            data.bankaOldKapasite.Add(bankaManagers[i].oldKapasite);
            data.bankaOzellikCarpani.Add(bankaManagers[i].ozellikCarpani);
            data.bankaOzellikSprite.Add(bankaManagers[i].ozellikSprite);
        }
    }
    public void YoneticiIseAl()
    {
        int _carpanAraligi = Random.Range(carpanAraligi.x,carpanAraligi.y);
        var obj = Instantiate(cvPrefab,Vector3.zero,Quaternion.identity,parent);
        var manager = Instantiate(managerPrefab,new Vector3(-10,10,0),Quaternion.identity);
        obj.GetComponent<Cv>().manager = manager.GetComponent<Manager>();
        obj.GetComponent<Cv>().manager.SetOzellikCarpani(_carpanAraligi);
        obj.GetComponent<Cv>().manager.ozellikAdi = _carpanAraligi+"x Yuk Kapasitesi";
        if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Caylak)
        {
            obj.GetComponent<Cv>().manager.managerSprite = bankaManagerLevels[0];
            obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = bankaManagerLevels[0];
            obj.GetComponent<Cv>().manager.managerSprite = bankaManagerLevels[0];
        }
        else if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Idareci)
        {
            obj.GetComponent<Cv>().manager.managerSprite = bankaManagerLevels[2];
             obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = bankaManagerLevels[2];
             obj.GetComponent<Cv>().manager.managerSprite = bankaManagerLevels[2];
        }
        if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Kidemli)
        {
            obj.GetComponent<Cv>().manager.managerSprite = bankaManagerLevels[1];
            obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = bankaManagerLevels[1];
            obj.GetComponent<Cv>().manager.managerSprite = bankaManagerLevels[1];
        }
        bankaManagers.Add(obj.GetComponent<Cv>().manager.GetComponent<YukcuManager>());
    }
    public void Gorevlendir()
    {
        if(banka.manager != null)
        {
            gorevlendirilmisManager.transform.SetParent(parent);
            gorevlendirilmisManager.manager.atanmismi = false;
            atanacakManager.atanmismi = true;
            atanacakCv.transform.SetParent(gorevlendirilmisParent);
            Vector3 oldPos = new Vector3(-10,10,0);
            atanacakCv.manager.transform.position = bankaManagerTransform.position;
            gorevlendirilmisManager.manager.transform.position = oldPos;
            gorevlendirilmisManager = atanacakCv;
            banka.manager.gameObject.SetActive(false);
            atanacakManager.gameObject.SetActive(true);
            banka.manager = atanacakManager;
        }
        else
        {
            atanacakManager.atanmismi = true;
            atanacakCv.transform.SetParent(gorevlendirilmisParent);
            atanacakCv.manager.transform.position = bankaManagerTransform.position;
            atanacakManager.gameObject.SetActive(true);
            banka.manager = atanacakManager;
        }
    }
    public void GorevdenAl()
    {
        
    }
}