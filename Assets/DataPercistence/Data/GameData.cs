 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{       
        public List<int> _level;    
        public List<bool> levelAtanmismi;
        public List< float> levelOzellikCarpani;
        public List< Deneyim> levelDeneyim;
        public List< float> levelKullanmaSuresi;
        public List< float> levelBeklemeSuresiTemp;
        public List< Sprite> levelManagerSprite;
        public List< Sprite> levelOzellikSprite;
        public List< float> levelOldAttackRate;

        public List<bool> bankaAtanmismi;
        public List< float> bankaOzellikCarpani;
        public List< Deneyim> bankaDeneyim;
        public List< float> bankaKullanmaSuresi;
        public List< float> bankaBeklemeSuresiTemp;
        public List< Sprite> bankaManagerSprite;
        public List< Sprite> bankaOzellikSprite;
        public List< int> bankaOldKapasite;
    
        public List<bool> asansorAtanmismi;
        public List<float> asansorOzellikCarpani;
        public List<Deneyim> asansorDeneyim;
        public List<float> asansorKullanmaSuresi;
        public List<float> asansorBeklemeSuresiTemp;
        public List<Sprite> asansorManagerSprite;
        public List<Sprite> asansorOzellikSprite;
        public List<float> asansorOldMoveSpeed;
    
    public int bankaManagerDataCount;
    public int level;
    public int nakit;
    public List<int> levelLevel;
    public Manager levelManager;
    public bool isFirst;
    public GameData()
    {
        nakit = 0;
        isFirst = true;
        levelAtanmismi = new List<bool>();
         levelOzellikCarpani = new List<float>();
         levelDeneyim = new List<Deneyim>();
         levelKullanmaSuresi = new List<float>();
         levelBeklemeSuresiTemp = new List<float>();
         levelManagerSprite = new List<Sprite>();
         levelOzellikSprite = new List<Sprite>();
         levelOldAttackRate = new List<float>();

        bankaAtanmismi = new List<bool>();
        bankaOzellikCarpani = new List<float>();
        bankaDeneyim = new List<Deneyim>();
        bankaKullanmaSuresi = new List<float>();
        bankaBeklemeSuresiTemp = new List<float>();
        bankaManagerSprite = new List<Sprite>();
        bankaOzellikSprite = new List<Sprite>();
        bankaOldKapasite = new List<int>();
        asansorAtanmismi = new List<bool>();
        asansorOzellikCarpani = new List<float>();
        asansorDeneyim = new List<Deneyim>();
        asansorKullanmaSuresi = new List<float>();
        asansorBeklemeSuresiTemp = new List<float>();
        asansorManagerSprite = new List<Sprite>();
        asansorOzellikSprite = new List<Sprite>();
        asansorOldMoveSpeed = new List<float>();
        bankaManagerDataCount = 0;
        level = 0;
        levelLevel = new List<int>(){1,1};
        levelManager = null;
        _level = new List<int>();
    }
}
