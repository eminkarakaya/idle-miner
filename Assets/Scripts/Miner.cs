using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum State{
    mining,
    goToKasa,
    goToMine,
}

public class Miner : MonoBehaviour
{
    public Level level;
    bool isMining;
    public int tekVurustaToplananMaden;
    [SerializeField] int cantaDolulugu;
    public float bagCapaity;
    [SerializeField] float attackRate;
    float attackRateTemp;
    [SerializeField] Transform kasaPoint , minePoint;
    public float madencilikHizi;
    void Update()
    {
        attackRateTemp -= Time.deltaTime;
        if(attackRateTemp < 0 && isMining)
        {
            
            attackRateTemp = attackRate;
        }
    }
    void Start()
    {
        level = transform.parent.GetComponent<Level>();
        GotoMine();
    }
   
    void ParayiKasayaKoy(int value)
    {
        level.SetKasa(value + level.GetKasa());
        cantaDolulugu = 0;
    }
    void GotoKasa()
    {
        transform.DOMoveX(kasaPoint.position.x,2f).OnComplete(()=>
        {
            ParayiKasayaKoy(cantaDolulugu);
            GotoMine();
        });

    }
    void GotoMine()
    {
        transform.DOMoveX(minePoint.position.x,2f).OnComplete(()=>Mining());
    }
    void Mining()
    {
        isMining = true;
        IEnumerator HitMine()
        {
            while(cantaDolulugu < bagCapaity)
            {
                yield return new WaitForSeconds(attackRate);
                cantaDolulugu += tekVurustaToplananMaden;
            }
            GotoKasa();
            isMining = false;
        }
        StartCoroutine(HitMine());
    }

}
