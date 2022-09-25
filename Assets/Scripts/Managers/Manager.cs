using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Manager : MonoBehaviour
{
    // public Vector2Int carpanAraligi;
    public Vector2Int kullanmaSuresiAraligi;
    public Vector2Int beklemeSuresiAraligi;
    public string isim;
    protected abstract string _ozellikAdi{get;set;}
    public string ozellikAdi{get=>_ozellikAdi; set{}}
    protected abstract Sprite _ozellikSprite{get;set;}
    public Sprite ozellikSprite{get=>_ozellikSprite; set{}}
    public int ozellikCarpani;
    public Sprite sprite;
    public Deneyim deneyim;
    [SerializeField] Button activeBtn;
    [SerializeField] Button btn;
    [SerializeField] Text text;
    public bool isActive;
    public float kullanmaSuresi;
    protected float kullanmaSuresiTemp;
    protected float beklemeSuresi;
    [SerializeField] protected float beklemeSuresiTemp;
    void Start()
    {
        kullanmaSuresiTemp = kullanmaSuresi;
        text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        btn = transform.GetChild(0).GetChild(1).GetComponent<Button>();
        activeBtn = transform.GetChild(0).GetChild(2).GetComponent<Button>();
    }
    public void SetOzellikCarpani(int value)
    {
        ozellikCarpani = value;
        if(value < 5)
        {
            deneyim = Deneyim.Caylak;
        }
        else if(value <= 7 && value > 4)
        {
            deneyim = Deneyim.Kidemli;
        }
        else
        {
            deneyim = Deneyim.Idareci;
        }
        beklemeSuresiTemp = Random.Range(beklemeSuresiAraligi.x,beklemeSuresiAraligi.y);
        kullanmaSuresi = Random.Range(beklemeSuresiAraligi.x,beklemeSuresiAraligi.y);
    }
    protected abstract void UseSpecialSkill();
    IEnumerator KullanmaSuresi()
    {
        activeBtn.gameObject.SetActive(true);
        text.text = kullanmaSuresi.ToString();
        while(kullanmaSuresi > 0 && isActive)
        {
            yield return new WaitForSeconds(1);
            kullanmaSuresi--;
            text.text = kullanmaSuresi.ToString();
        }
        kullanmaSuresi = kullanmaSuresiTemp;
        btn.interactable = false;
        isActive = false;
        StartCoroutine(BeklemeSuresi());
    }
    IEnumerator BeklemeSuresi()
    {
        beklemeSuresi = beklemeSuresiTemp;
        activeBtn.gameObject.SetActive(false);
        btn.gameObject.SetActive(true);
        text.text = beklemeSuresi.ToString();
        while(beklemeSuresi > 0 && !isActive)
        {
            yield return new WaitForSeconds(1);
            beklemeSuresi --;
            text.text = beklemeSuresi.ToString();
        }
        text.gameObject.SetActive(false);
        btn.GetComponent<Image>().enabled = true;
    }
    public void OzelHareketBtn()
    {
        if(!isActive && beklemeSuresi <= 0)
        {
            text.gameObject.SetActive(true);
            isActive = true;
            UseSpecialSkill();
            btn.GetComponent<Image>().enabled = false;
            activeBtn.GetComponent<Image>().enabled = true;
            StartCoroutine(KullanmaSuresi());
        }
    }
}
