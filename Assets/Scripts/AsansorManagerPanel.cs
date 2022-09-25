using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AsansorManagerPanel : MonoBehaviour
{
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
    void Start()
    {
        asansor = FindObjectOfType<Asansor>();
        if(asansor.asansorManager != null)
        {
            gorevlendirilmisManager.manager = asansor.manager;
        }
    }
    public void YoneticiIseAl()
    {
        int _carpanAraligi = Random.Range(carpanAraligi.x,carpanAraligi.y);
        var obj = Instantiate(GameManager.instance.cvPrefab,Vector3.zero,Quaternion.identity,parent);
        var manager = Instantiate(managerPrefab,new Vector3(-10,10,0),Quaternion.identity);
        obj.GetComponent<Cv>().manager = manager.GetComponent<Manager>();
        obj.GetComponent<Cv>().manager.SetOzellikCarpani(_carpanAraligi);
        obj.GetComponent<Cv>().manager.ozellikAdi = _carpanAraligi+"x AsansorHizi";
        // obj.GetComponent<Cv>().manager.ozellikSprite = ozellikAdi;
    }
    public void Gorevlendir()
    {
        gorevlendirilmisManager.transform.SetParent(parent);
        atanacakCv.transform.SetParent(gorevlendirilmisParent);
        Vector3 oldPos = atanacakCv.transform.position;
        atanacakCv.manager.transform.position = asansorManagerTransform.position;
        gorevlendirilmisManager.manager.transform.position = oldPos;
        gorevlendirilmisManager = atanacakCv;
        asansor.manager.gameObject.SetActive(false);
        atanacakManager.gameObject.SetActive(true);
        asansor.manager = atanacakManager;
    }
}
