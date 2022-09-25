using UnityEngine;
using System.Collections.Generic;
public class AttackRateManager : Manager , LevelManager
{
    protected override Sprite _ozellikSprite{get;set;}
    protected override string _ozellikAdi{get;set;}
    public Level level { get; set; }
    protected override void UseSpecialSkill()
    {
        
    }
}
