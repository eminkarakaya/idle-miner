using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Manager : MonoBehaviour
{
    public bool atanmismi;
    public string isim;
    protected abstract string _ozellikAdi{get;set;}
    public string ozellikAdi{get=>_ozellikAdi; set{}}
    protected abstract Sprite _ozellikSprite{get;set;}
    public Sprite ozellikSprite{get=>_ozellikSprite; set{}}
    public float ozellikCarpani;
    public Sprite managerSprite;
    public Deneyim deneyim;
    [SerializeField] Button activeBtn;
    [SerializeField] Button btn;
    [SerializeField] Text text;
    public bool isActive;
    public float kullanmaSuresi;
    protected float kullanmaSuresiTemp;
    protected float beklemeSuresi;
    [SerializeField] public float beklemeSuresiTemp;
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
            // deneyimImage.color = caylakColor;
        }
        else if(value <= 7 && value > 4)
        {
            deneyim = Deneyim.Kidemli;
            // deneyimImage.color = kidemliColor;
        }
        else
        {
            deneyim = Deneyim.Idareci;
            // deneyimImage.color = yoneticiColor;
        }
        
    }
    protected abstract void UseSpecialSkill();
    protected abstract void UseSpecialSkillTersi();
    IEnumerator KullanmaSuresi()
    {
        activeBtn.enabled = true;
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
        UseSpecialSkillTersi(); 
        StartCoroutine(BeklemeSuresi());
    }
    IEnumerator BeklemeSuresi()
    {
        beklemeSuresi = beklemeSuresiTemp;
        activeBtn.enabled = true;
        btn.enabled = true;
        text.text = beklemeSuresi.ToString();
        while(beklemeSuresi > 0 && !isActive)
        {
            yield return new WaitForSeconds(1);
            beklemeSuresi --;
            text.text = beklemeSuresi.ToString();
        }
        text.enabled = false;
        btn.GetComponent<Image>().enabled = true;
    }
    public void OzelHareketBtn()
    {
        if(!isActive && beklemeSuresi <= 0)
        {
            text.enabled = true;
            isActive = true;
            UseSpecialSkill();
            btn.GetComponent<Image>().enabled = false;
            activeBtn.GetComponent<Image>().enabled = true;
            StartCoroutine(KullanmaSuresi());
        }
    }
}
