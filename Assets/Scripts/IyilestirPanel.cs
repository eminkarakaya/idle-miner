using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class IyilestirPanel : MonoBehaviour 
{
    public List<int> ekstraElemanLevelleri;
    Asansor asansor;
    float madenCikarmaArtisYuzdesi = 15;
    float moveSpeedArtisYuzdesi = 1;
    float cantaKapasitesiArtisYuzdesi = 20;
    long costArtisYuzdesi = 25;
    [SerializeField] GameObject iyilestirPanelObj;
    [SerializeField] Text yurumeHiziText,yurumeHiziTextsonraki
        ,attacRateText,attacRateTextSonraki,cantaKapasitesiText
        ,cantaKapasitesiTextSonraki,madenciSayisiText,madenciSayisiTextSonraki,iyilestirCostTexti,titleText;
    [SerializeField] Slider slider;
    public Level level;
    void Start()
    {
        asansor = FindObjectOfType<Asansor>();
        for (int j = 0; j < asansor.activeLevels.Count; j++)
        {
            for (int i = 0; i < asansor.activeLevels[j].levelMiners.Count; i++)
            {
                for (int k = 0; k < asansor.activeLevels[j].levelLevel; k++)
                {
                    asansor.activeLevels[j].levelMiners[i].bagCapaity += ((int) (asansor.activeLevels[j].levelMiners[i].bagCapaity * (cantaKapasitesiArtisYuzdesi/100)));
                    asansor.activeLevels[j].levelMiners[i].attackRate += ((asansor.activeLevels[j].levelMiners[i].attackRate * (madenCikarmaArtisYuzdesi/100))); 
                    asansor.activeLevels[j].levelMiners[i].moveTime -= ((asansor.activeLevels[j].levelMiners[i].moveTime * (moveSpeedArtisYuzdesi/100)));
                }
            }
            asansor.activeLevels[j].iyilestirCost = (int) (asansor.activeLevels[j].iyilestirCost + (asansor.activeLevels[j].iyilestirCost * (costArtisYuzdesi/100)));
        }
            
        LoadIyılestirDatas();
    }
    public void ClickIyilestirBtn()
    {
        level.levelLevel++;
        for (int i = 0; i < ekstraElemanLevelleri.Count; i++)
        {
            if(level.levelLevel == ekstraElemanLevelleri[i])
            {
                var obj = Instantiate(level.levelMiners[0].gameObject, level.minerSpawnPoint.position ,Quaternion.identity,level.levelMiners[0].transform.parent);
                level.levelMiners.Add(obj.GetComponent<Miner>());
            }
        }
        for (int i = 0; i < level.levelMiners.Count; i++)
        {
            level.levelMiners[i].bagCapaity += (level.levelMiners[i].bagCapaity * (cantaKapasitesiArtisYuzdesi/100));
            level.levelMiners[i].attackRate += level.levelMiners[i].attackRate * (madenCikarmaArtisYuzdesi/100); 
            level.levelMiners[i].moveTime -= level.levelMiners[i].moveTime * (moveSpeedArtisYuzdesi/100);
        }
        level.iyilestirCost = (int) (level.iyilestirCost + (level.iyilestirCost * (costArtisYuzdesi/100)));
        LoadIyılestirDatas();
    }
    public void CarpiBtn()
    {
        GetComponent<Canvas>().enabled = false;
        level = null;
    }
    public void LoadIyılestirDatas()
    {
        yurumeHiziText.text =String.Format("{0:0.00}", level.levelMiners[0].moveTime) + "s";
        yurumeHiziTextsonraki.text =  "-" + String.Format("{0:0.00}",level.levelMiners[0].moveTime * (moveSpeedArtisYuzdesi/100))+ "s";

        attacRateText.text = String.Format("{0:0.0}", level.levelMiners[0].attackRate) + "/s";
        attacRateTextSonraki.text =  "+" + String.Format("{0:0.0}", level.levelMiners[0].attackRate * (madenCikarmaArtisYuzdesi/100)) + "/s";

        madenciSayisiText.text = level.isciKapasitesi.ToString();
        madenciSayisiTextSonraki.text = 0.ToString();

        cantaKapasitesiText.text = GameManager.instance.CaclText(level.levelMiners[0].bagCapaity);
        cantaKapasitesiTextSonraki.text =  "+" + GameManager.instance.CaclText ((long) (level.levelMiners[0].bagCapaity * (cantaKapasitesiArtisYuzdesi/100)));

        titleText.text = "Maden Kuyusu " + level + " " + level.levelLevel+". Seviye";
        iyilestirCostTexti.text = "+" + GameManager.instance.CaclText(level.iyilestirCost);
    }
    public void LevelUpButton()
    {
        gameObject.GetComponent<Canvas>().enabled = true;
        LoadIyılestirDatas();
    }
}
