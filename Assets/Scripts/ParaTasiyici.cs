using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ParaTasiyici : MonoBehaviour
{
    [SerializeField] Transform bankaPos;
    [SerializeField] Transform madenIslemeMachinePos;
    Banka banka;
    MadenIslemeMachine madenIslemeMachine;
    [SerializeField] Text dolulukText;
    [SerializeField] int kapasite;
    [SerializeField] int doluluk;
    [Range(1,10)] [SerializeField] float moveSpeed;
    [Range(1,10)] [SerializeField]  float dolumSuresi;
    [Range(1,10)] [SerializeField]  float bosaltimSuresi;
    void Start()
    {
        madenIslemeMachine = FindObjectOfType<MadenIslemeMachine>();
        banka = FindObjectOfType<Banka>();
        Debug.Log(GameManager.instance);
        MadenAl();
    }
    public void SetGold(int value){
        doluluk = value;
        dolulukText.text = doluluk.ToString();  
    }
    void BankayaGotur()
    {
        transform.DOMoveX(bankaPos.position.x,moveSpeed).OnComplete(()=>transform.DOMoveX(bankaPos.position.x,bosaltimSuresi)).OnComplete(()=>
        {
            GameManager.instance.SetGold(doluluk);
            SetGold(0);
            MadenAl();
        });
    }
    void MadenAl()
    {
        transform.DOMoveX(madenIslemeMachinePos.position.x,moveSpeed).OnComplete(()=>transform.DOMoveX(madenIslemeMachinePos.position.x,dolumSuresi)).OnComplete(()=>
        {
            if(madenIslemeMachine.GetGold() >= kapasite)
            {
                madenIslemeMachine.SetGold(madenIslemeMachine.GetGold()-kapasite);
                SetGold(kapasite);
            }
            else
            {
                SetGold( madenIslemeMachine.GetGold());
                madenIslemeMachine.SetGold(0);
            }
            BankayaGotur();
        });
    }
}
