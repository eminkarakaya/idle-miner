using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Asansor : MonoBehaviour
{
    public Transform asansorManagerTransform;
    [SerializeField] Text asansorManagerCanvasTitleText;
    public GameObject asansorManagerCanvas;
    public Banka banka;
    public Manager manager;
    public AsansorManager asansorManager;
    MadenIslemeMachine madenIslemeMachine;
    [SerializeField] float asansorBosaltmaSuresi;
    public int temp = 1;
    [SerializeField] Transform zemin;
    public List<Level> activeLevels;
    [SerializeField] Text asansorDoluluguTxt;
    public int asansorKapasitesi;
    public int asansorDolulugu;
    [SerializeField] float asansorHizi;
    public float defaultToplamaSuresi;
    [SerializeField] float toplamaSuresi;
    void Start()
    {
        manager.transform.position = asansorManagerTransform.position;
        madenIslemeMachine = FindObjectOfType<MadenIslemeMachine>();
        banka = FindObjectOfType<Banka>();
        StartCoroutine(AsansorMovement());
    }
    public void SetAsansorKapasitesi(int value)
    {
        asansorKapasitesi = value;
    }
    public void SetAsansorDolulugu(int value)
    {
        asansorDolulugu = value;
        asansorDoluluguTxt.text = asansorDolulugu.ToString();
    }
    void AsansordenZemineAktarim()
    {
        transform.DOMoveY(zemin.position.y,asansorHizi).OnComplete(()=> transform.DOMoveY(zemin.position.y,asansorBosaltmaSuresi)).OnComplete(()=>
        {
            madenIslemeMachine.SetGold(asansorDolulugu + madenIslemeMachine.GetGold());
            SetAsansorDolulugu(0);
            StartCoroutine(AsansorMovement());
        });
    }
    IEnumerator AsansorMovement()
    {
        for (int i = 0; i < activeLevels.Count; i++)
        {
            if(asansorDolulugu >= asansorKapasitesi)
            {
                AsansordenZemineAktarim();
            }
            transform.DOMoveY(activeLevels[i].transform.position.y+1,asansorHizi).
            OnComplete(()=>transform.DOMoveY(activeLevels[i].transform.position.y,toplamaSuresi)).
            OnComplete(()=>{
                if(activeLevels[i].GetKasa() + asansorDolulugu >= asansorKapasitesi)
                {
                    // activelevel[i].setkasa(asansorkapasitesi - asansordolulugu) asansorkapasitesi - asansordolulugu  
                    activeLevels[i].SetKasa(activeLevels[i].GetKasa() - asansorKapasitesi - asansorDolulugu);
                    SetAsansorDolulugu(asansorKapasitesi);
                    i = activeLevels.Count;
                }
                else
                {
                    SetAsansorDolulugu(activeLevels[i].GetKasa() + asansorDolulugu);
                    activeLevels[i].SetKasa(0);
                }
                });
            yield return new WaitForSeconds(asansorHizi); 
        }
        AsansordenZemineAktarim();
    }
    public void AsansorManagerCanvas()
    {
        asansorManagerCanvas.GetComponent<Canvas>().enabled = true;
        asansorManagerCanvasTitleText.text = "Asansor Yoneticisi";
    }
    public void Carpi()
    {
        asansorManagerCanvas.GetComponent<Canvas>().enabled = false;
    }
}
