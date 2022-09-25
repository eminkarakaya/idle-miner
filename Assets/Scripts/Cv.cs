using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Deneyim{
    Caylak,
    Kidemli,
    Idareci
}
public class Cv : MonoBehaviour
{
    AsansorManagerPanel asansorManagerPanel;
    BankaYoneticiAtama bankaYoneticiAtama;
    [SerializeField] Button gorevlendirBtn;
    public Manager manager;
    public string _name;
    [SerializeField] Text nameText;
    [SerializeField] Text deneyimText;
    [SerializeField] Text efektSuresiText;
    [SerializeField] Text ozellikText;
    [SerializeField] Image ozellikSprite;
    [SerializeField] Image managerSprite;
    void Start()
    {
        bankaYoneticiAtama = FindObjectOfType<BankaYoneticiAtama>();
        asansorManagerPanel = FindObjectOfType<AsansorManagerPanel>();
        deneyimText.text = manager.deneyim.ToString();
        efektSuresiText.text = manager.kullanmaSuresi.ToString();
        ozellikText.text = manager.kullanmaSuresi.ToString();
        ozellikText.text = manager.ozellikCarpani.ToString();
        nameText.text = manager.isim;
        ozellikSprite.sprite = manager.ozellikSprite;
        managerSprite.sprite = manager.sprite;
    }
    public void AsansorGorevlendir()
    {
        asansorManagerPanel.atanacakManager = manager;
        asansorManagerPanel.atanacakCv = this;
    }
    public void BankaGorevlendir()
    {
        bankaYoneticiAtama.atanacakManager = manager;
        bankaYoneticiAtama.atanacakCv = this;
    }
}
