using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
public class YukcuManager : Manager, AsansorManager,BankaManager
{
    protected override Sprite _ozellikSprite{get;set;}
    protected override string _ozellikAdi{get;set;}
    public Asansor asansor { get; set; }
    public Banka banka { get; set; }
    public int oldKapasite;
    void Start()
    {
        asansor = FindObjectOfType<Asansor>();
        banka = FindObjectOfType<Banka>();
    }
    protected override void UseSpecialSkill()
    {
        oldKapasite = banka.paraTasiyicilar[0].kapasite;
        for (int i = 0; i < banka.paraTasiyicilar.Count; i++)
        {
            banka.paraTasiyicilar[i].kapasite *= (int)ozellikCarpani;
        }
    }
    protected override void UseSpecialSkillTersi()
    {
        for (int i = 0; i < banka.paraTasiyicilar.Count; i++)
        {
            banka.paraTasiyicilar[i].kapasite = oldKapasite;
        }
    }
}
