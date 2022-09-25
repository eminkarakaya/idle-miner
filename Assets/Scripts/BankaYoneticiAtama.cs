using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankaYoneticiAtama : MonoBehaviour
{
    public string ozellikAdi;
    public GameObject managerPrefab;
    public Vector2Int carpanAraligi;
    public Vector2Int kullanmaSuresiAraligi;
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
        var obj = Instantiate(GameManager.instance.cvPrefab,Vector3.zero,Quaternion.identity,parent);
        var manager = Instantiate(managerPrefab,new Vector3(-10,10,0),Quaternion.identity);
        obj.GetComponent<Cv>().manager = manager.GetComponent<Manager>();
        obj.GetComponent<Cv>().manager.SetOzellikCarpani(_carpanAraligi);
        obj.GetComponent<Cv>().manager.ozellikAdi = _carpanAraligi+"x Yuk Kapasitesi";
        // obj.GetComponent<Cv>().manager.ozellikSprite = ozellikAdi;
    }
    public void Gorevlendir()
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
}
