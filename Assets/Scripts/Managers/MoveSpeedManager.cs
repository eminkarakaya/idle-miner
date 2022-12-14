using UnityEngine;
using System.Collections.Generic;
public class MoveSpeedManager : Manager , AsansorManager,BankaManager,LevelManager
{
    protected override Sprite _ozellikSprite{get;set;}
    protected override string _ozellikAdi{get;set;}
    public Asansor asansor { get; set; }
    public Banka banka { get; set; }
    public Level level { get; set; }
    public float oldMove;
    void Start()
    {
        asansor = FindObjectOfType<Asansor>();
        banka = FindObjectOfType<Banka>();
    }
    protected override void UseSpecialSkill()
    {
        oldMove = asansor.asansorSuresi;
        
        asansor.asansorSuresi = asansor.asansorSuresi - (asansor.asansorSuresi *(ozellikCarpani/100));
        Debug.Log(ozellikCarpani);
        Debug.Log(asansor.asansorSuresi);
    }
    protected override void UseSpecialSkillTersi()
    {
        asansor.asansorSuresi = oldMove;   
    }
}
