using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AsansorManagerPanel : MonoBehaviour , IDataPersistence
{
    public List<Manager> asansorManagers;
    public List<Sprite> asansorManagerLevels;
    public GameObject cvPrefab;
    public Vector2Int carpanAraligi;
    public GameObject managerPrefab;
    string ozellikAdi;
    public Transform asansorManagerTransform;
    public Cv atanacakCv;
    public Manager atanacakManager;
    public Cv gorevlendirilmisManager;
    Asansor asansor;
    [SerializeField] Transform parent;
    public Transform gorevlendirilmisParent;
    public void LoadData(GameData data)
    {    
        asansor = FindObjectOfType<Asansor>();
        // if(data.isFirst)
        //     return;
        for (int i = 0; i < data.asansorBeklemeSuresiTemp.Count; i++)
        {
            var cv = Instantiate(cvPrefab,transform.position,Quaternion.identity,parent);
            var manager = Instantiate(managerPrefab,new Vector3(-10,10,0),Quaternion.identity);
            asansorManagers.Add(manager.GetComponent<MoveSpeedManager>());
            // bankaManagers[i] = data.bankaManagerDatas[i].manager.GetComponent<YukcuManager>();
            manager.GetComponent<MoveSpeedManager>().beklemeSuresiTemp = data.asansorBeklemeSuresiTemp[i];
            manager.GetComponent<MoveSpeedManager>().deneyim = data.asansorDeneyim[i];
            manager.GetComponent<MoveSpeedManager>().atanmismi = data.asansorAtanmismi[i];
            if(manager.GetComponent<MoveSpeedManager>().atanmismi)
            {
                asansor.manager = manager.GetComponent<MoveSpeedManager>();
                asansor.manager.transform.position = asansorManagerTransform.position;
                cv.transform.SetParent(gorevlendirilmisParent);
                gorevlendirilmisManager = cv.GetComponent<Cv>();
                cv.transform.localScale = Vector3.one;
            }

            // _manager.isim = data.bankaManagerDatas[i].;
            manager.GetComponent<MoveSpeedManager>().kullanmaSuresi = data.asansorKullanmaSuresi[i];
            manager.GetComponent<MoveSpeedManager>().managerSprite = data.asansorManagerSprite[i];
            manager.GetComponent<MoveSpeedManager>().ozellikCarpani = data.asansorOzellikCarpani[i];
            manager.GetComponent<MoveSpeedManager>().oldMove = data.asansorOldMoveSpeed[i];
            manager.GetComponent<MoveSpeedManager>().ozellikSprite = data.asansorOzellikSprite[i];
            cv.GetComponent<Cv>().manager = manager.GetComponent<Manager>();
            cv.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = data.asansorManagerSprite[i];
        }
        // Debug.Log(data.bankaManagerDatas[0].manager);
    }
    public void SaveData(ref GameData data)
    {
        // if(asansor.manager == null)
        //     return;
        data.asansorAtanmismi.Clear();
        data.asansorBeklemeSuresiTemp.Clear();
        data.asansorDeneyim.Clear();
        data.asansorKullanmaSuresi.Clear();
        data.asansorManagerSprite.Clear();
        data.asansorOldMoveSpeed.Clear();
        data.asansorOzellikCarpani.Clear();
        data.asansorOzellikSprite.Clear();
        for (int i = 0; i < asansorManagers.Count; i++)
        { 
            Debug.Log(i);
            // newDatas.Add(newData);
            // Debug.Log(data.bankaManagerDatas[i].beklemeSuresiTemp);
            data.asansorAtanmismi.Add(asansorManagers[i].atanmismi);
            data.asansorBeklemeSuresiTemp.Add(asansorManagers[i].kullanmaSuresi);
            data.asansorDeneyim.Add(asansorManagers[i].deneyim);
            data.asansorKullanmaSuresi.Add(asansorManagers[i].kullanmaSuresi);
            data.asansorManagerSprite.Add(asansorManagers[i].managerSprite);
            data.asansorOldMoveSpeed.Add(asansorManagers[i].GetComponent<MoveSpeedManager>().oldMove);
            data.asansorOzellikCarpani.Add(asansorManagers[i].ozellikCarpani);
            data.asansorOzellikSprite.Add(asansorManagers[i].ozellikSprite);
        }
    }
    public void YoneticiIseAl()
    {
        int _carpanAraligi = Random.Range(carpanAraligi.x,carpanAraligi.y);
        var obj = Instantiate(cvPrefab,Vector3.zero,Quaternion.identity,parent);
        var manager = Instantiate(managerPrefab,new Vector3(-10,10,0),Quaternion.identity);
        obj.GetComponent<Cv>().manager = manager.GetComponent<Manager>();
        obj.GetComponent<Cv>().manager.SetOzellikCarpani(_carpanAraligi);
        obj.GetComponent<Cv>().manager.ozellikAdi = _carpanAraligi+"x AsansorHizi";
        // obj.GetComponent<Cv>().manager.ozellikSprite = ozellikAdi;
        if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Caylak)
        {
            obj.GetComponent<Cv>().manager.managerSprite = asansorManagerLevels[0];
            obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = asansorManagerLevels[0];
        }
        else if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Idareci)
        {
            obj.GetComponent<Cv>().manager.managerSprite = asansorManagerLevels[2];
            obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = asansorManagerLevels[2];
        }
        else if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Kidemli)
        {
            obj.GetComponent<Cv>().manager.managerSprite = asansorManagerLevels[1];
            obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = asansorManagerLevels[1];
        }
        asansorManagers.Add(obj.GetComponent<Cv>().manager);
    }
    public void Gorevlendir()
    {
        if(asansor.manager != null)
        {
            gorevlendirilmisManager.transform.SetParent(parent);
            gorevlendirilmisManager.manager.atanmismi = false;
            atanacakManager.atanmismi = true;
            atanacakCv.transform.SetParent(gorevlendirilmisParent);
            Vector3 oldPos = atanacakCv.transform.position;
            atanacakCv.manager.transform.position = asansorManagerTransform.position;
            gorevlendirilmisManager.manager.transform.position = oldPos;
            gorevlendirilmisManager = atanacakCv;
            asansor.manager.gameObject.SetActive(false);
            atanacakManager.gameObject.SetActive(true);
            asansor.manager = atanacakManager;
        }
        else
        {
            atanacakManager.atanmismi = true;
            atanacakCv.transform.SetParent(gorevlendirilmisParent);
            atanacakCv.manager.transform.position = asansorManagerTransform.position;
            atanacakManager.gameObject.SetActive(true);
            asansor.manager = atanacakManager;
            gorevlendirilmisManager = atanacakCv;
        }
    }
}
