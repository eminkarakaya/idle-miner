using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Asansor : MonoBehaviour
{
    public const float fullAsansorBolastmaSuresi = 1;
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
    [SerializeField] public float asansorSuresi;
    public float defaultToplamaSuresi;
    [SerializeField] float toplamaSuresi;
    void Start()
    {
        if(manager != null)
            manager.transform.position = asansorManagerTransform.position;
        madenIslemeMachine = FindObjectOfType<MadenIslemeMachine>();
        banka = FindObjectOfType<Banka>();
        StartCoroutine(AsansorMovement());
    }
    public float GetAsansordenZemineAktarimSuresi()
    {
        return fullAsansorBolastmaSuresi / (asansorKapasitesi / asansorDolulugu);
    }
    public float GetToplamaSuresi(Level level)
    {
        return fullAsansorBolastmaSuresi / (asansorKapasitesi / level.GetKasa());
    }
    public void SetAsansorKapasitesi(int value)
    {
        asansorKapasitesi = value;
    }
    public void SetAsansorDolulugu(int value)
    {
        asansorDolulugu = value;
        asansorDoluluguTxt.text = GameManager.instance.CaclText(asansorDolulugu);
    }
    public void SetAsansorDoluluguTopla(int value)
    {
        asansorDolulugu += value;
        asansorDoluluguTxt.text = GameManager.instance.CaclText(asansorDolulugu);
    }
    void AsansordenZemineAktarim()
    {
        transform.DOMoveY(zemin.position.y,asansorSuresi).OnComplete(()=> transform.DOMoveY(zemin.position.y,GetAsansordenZemineAktarimSuresi())).OnComplete(()=>
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
            transform.DOMoveY(activeLevels[i].transform.position.y+1,asansorSuresi).
            OnComplete(()=>transform.DOMoveY(activeLevels[i].transform.position.y,GetToplamaSuresi(activeLevels[i]))).
            OnComplete(()=>{
                if(activeLevels[i].GetKasa() + asansorDolulugu >= asansorKapasitesi)
                {
                    // activelevel[i].setkasa(asansorkapasitesi - asansordolulugu) asansorkapasitesi - asansordolulugu  
                    activeLevels[i].SetKasa(activeLevels[i].GetKasa() - (asansorKapasitesi - asansorDolulugu));
                    SetAsansorDolulugu(asansorKapasitesi);
                    i = activeLevels.Count;
                }
                else
                {
                    SetAsansorDoluluguTopla(activeLevels[i].GetKasa());
                    activeLevels[i].SetKasa(0);
                }
                });
            yield return new WaitForSeconds(asansorSuresi); 
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
    public float AsansorBirSaniyedeToplananMaden()
    {
        return asansorKapasitesi / ((activeLevels.Count * asansorSuresi) + (fullAsansorBolastmaSuresi*2));
    }
}
