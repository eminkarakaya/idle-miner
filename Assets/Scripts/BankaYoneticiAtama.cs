using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankaYoneticiAtama : MonoBehaviour
{
    public List<Sprite> bankaManagerLevels;
    public GameObject cvPrefab;
    public string ozellikAdi;
    public GameObject managerPrefab;
    public Vector2Int carpanAraligi;
    public Vector2Int beklemeSuresiAraligi;
    public Transform bankaManagerTransform;
    public Cv atanacakCv;
    public Manager atanacakManager;
    public Cv gorevlendirilmisManager;
    Banka banka;
    [SerializeField] Transform parent;
    public Transform gorevlendirilmisParent;
    void Start()
    {
        banka = FindObjectOfType<Banka>();
        if(banka.manager != null)
        {
            gorevlendirilmisManager.manager = banka.manager;
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
            obj.GetComponent<Cv>().manager.sprite = bankaManagerLevels[0];
            obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = bankaManagerLevels[0];
        }
        else if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Idareci)
        {
            obj.GetComponent<Cv>().manager.sprite = bankaManagerLevels[2];
             obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = bankaManagerLevels[2];
        }
        if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Kidemli)
        {
            obj.GetComponent<Cv>().manager.sprite = bankaManagerLevels[1];
             obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = bankaManagerLevels[1];
        }
    }
    public void Gorevlendir()
    {
        if(gorevlendirilmisManager != null)
        {
            gorevlendirilmisManager.transform.SetParent(parent);
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