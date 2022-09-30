using UnityEngine;
using System.Collections.Generic;
public class AttackRateManager : Manager , LevelManager 
{
    protected override Sprite _ozellikSprite{get;set;}
    protected override string _ozellikAdi{get;set;}
    [SerializeField] public Level level { get; set; }
    [SerializeField] public int _level = -1;
    public float oldAttckrate;
    void Start()
    {
            GetComponent<SpriteRenderer>().sprite = managerSprite;
    }
    protected override void UseSpecialSkill()
    {
        Debug.Log(level.levelMiners.Count);
        Debug.Log(level);
        oldAttckrate = level.levelMiners[0].attackRate;
        for (int i = 0; i < level.levelMiners.Count; i++)
        {
            level.levelMiners[i].attackRate = level.levelMiners[i].attackRate - level.levelMiners[i].attackRate  * (1/ozellikCarpani);
        }

    }
    protected override void UseSpecialSkillTersi()
    {
        for (int i = 0; i < level.levelMiners.Count; i++)
        {
            level.levelMiners[i].attackRate = oldAttckrate;
        }
    }
}
