using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IyilestirPanel : MonoBehaviour
{
    [SerializeField] GameObject iyilestirPanelObj;
    [SerializeField] Text yurumeHiziText,yurumeHiziTextsonraki
        ,attacRateText,attacRateTextSonraki,cantaKapasitesiText
        ,cantaKapasitesiTextSonraki,madenciSayisiText,madenciSayisiTextSonraki,iyilestirCostTexti,titleText;
    [SerializeField] Slider slider;
    public Level level;
    public void ClickIyilestirBtn()
    {
        level.levelLevel++;
        for (int i = 0; i < level.levelMiners.Count; i++)
        {
            level.levelMiners[i].bagCapaity += level.levelIyilestirDatas[level.levelLevel].cantaKapasitesi;
            level.levelMiners[i].attackRate += level.levelIyilestirDatas[level.levelLevel].attackRate;
            level.levelMiners[i].moveTime -= level.levelIyilestirDatas[level.levelLevel].yurumeHizi;
            level.iyilestirCost += level.levelIyilestirDatas[level.levelLevel].iyilestirCost;
        }
        LoadIyılestirDatas();
    }
    public void CarpiBtn()
    {
        GetComponent<Canvas>().enabled = false;
        level = null;
    }
    public void LoadIyılestirDatas()
    {
        yurumeHiziText.text = level.levelMiners[0].moveTime.ToString();
        yurumeHiziTextsonraki.text = level.levelIyilestirDatas[level.levelLevel+1].yurumeHizi.ToString();

        attacRateText.text = level.levelMiners[0].attackRate.ToString();
        attacRateTextSonraki.text = level.levelIyilestirDatas[level.levelLevel+1].attackRate.ToString();

        madenciSayisiText.text = level.isciKapasitesi.ToString();
        madenciSayisiTextSonraki.text = level.levelIyilestirDatas[level.levelLevel+1].madenciSayisi.ToString();

        cantaKapasitesiText.text = level.levelMiners[0].bagCapaity.ToString();
        cantaKapasitesiTextSonraki.text = level.levelIyilestirDatas[level.levelLevel+1].cantaKapasitesi.ToString();

        titleText.text = "Maden Kuyusu " + level + " " + level.levelLevel+". Seviye";
        iyilestirCostTexti.text = level.levelIyilestirDatas[level.levelLevel].iyilestirCost.ToString();
    }
    public void LevelUpButton()
    {
        gameObject.GetComponent<Canvas>().enabled = true;
        LoadIyılestirDatas();
    }
}
