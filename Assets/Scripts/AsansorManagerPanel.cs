using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AsansorManagerPanel : MonoBehaviour
{
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
        var obj = Instantiate(cvPrefab,Vector3.zero,Quaternion.identity,parent);
        var manager = Instantiate(managerPrefab,new Vector3(-10,10,0),Quaternion.identity);
        obj.GetComponent<Cv>().manager = manager.GetComponent<Manager>();
        obj.GetComponent<Cv>().manager.SetOzellikCarpani(_carpanAraligi);
        obj.GetComponent<Cv>().manager.ozellikAdi = _carpanAraligi+"x AsansorHizi";
        // obj.GetComponent<Cv>().manager.ozellikSprite = ozellikAdi;
        if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Caylak)
        {
            Debug.Log("caylak");
            obj.GetComponent<Cv>().manager.sprite = asansorManagerLevels[0];
            obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = asansorManagerLevels[0];
        }
        else if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Idareci)
        {
            Debug.Log("idaredi");
            obj.GetComponent<Cv>().manager.sprite = asansorManagerLevels[2];
             obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = asansorManagerLevels[2];
        }
        if(obj.GetComponent<Cv>().manager.deneyim == Deneyim.Kidemli)
        {
            Debug.Log("kidemli");
            obj.GetComponent<Cv>().manager.sprite = asansorManagerLevels[1];
             obj.GetComponent<Cv>().manager.GetComponent<SpriteRenderer>().sprite = asansorManagerLevels[1];
        }
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
