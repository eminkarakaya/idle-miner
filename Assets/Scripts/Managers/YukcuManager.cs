using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
public class YukcuManager : Manager, AsansorManager,BankaManager
{
    protected override Sprite _ozellikSprite{get;set;}
    protected override string _ozellikAdi{get;set;}
    public Asansor asansor { get; set; }
    public Banka banka { get; set; }
    void Start()
    {
        asansor = FindObjectOfType<Asansor>();
        banka = FindObjectOfType<Banka>();
    }
    protected override void UseSpecialSkill()
    {
        
    }
}
