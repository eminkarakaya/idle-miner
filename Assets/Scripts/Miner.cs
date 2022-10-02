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
    // public Miner()
    // {
    //     cantaDolulugu = 0;
    // }
    public const float oneSec = 1;
    public float moveTime;
    public Level level;
    bool isMining;
    public int tekVurustaToplananMaden;
    [SerializeField] int cantaDolulugu;
    public float bagCapaity;
    [SerializeField] public float attackRate;
    float attackRateTemp;
    [SerializeField] public Transform kasaPoint , minePoint;
    void Update()
    {
        // attackRateTemp -= Time.deltaTime;
        // if(attackRateTemp < 0 && isMining)
        // {
        //     attackRateTemp = attackRate;
        // }
    }
    void Start()
    {
        level = transform.parent.GetComponent<Level>();
        GotoMine();
    }
   
    void ParayiKasayaKoy(int value)
    {
        level.SetKasaTopla(value);
        cantaDolulugu = 0;
    }
    void GotoKasa()
    {
        transform.DOMoveX(kasaPoint.position.x,moveTime).OnComplete(()=>
        {
            ParayiKasayaKoy(cantaDolulugu);
            GotoMine();
        });

    }
    void GotoMine()
    {
        transform.DOMoveX(minePoint.position.x,moveTime).OnComplete(()=>Mining());
    }
    void Mining()
    {
        isMining = true;
        IEnumerator HitMine()
        {
            while(cantaDolulugu < bagCapaity)
            {
                yield return new WaitForSeconds(oneSec/attackRate);
                cantaDolulugu += tekVurustaToplananMaden;
            }
            GotoKasa();
            isMining = false;
        }
        StartCoroutine(HitMine());
    }
    public float GetBirSaniyedeToplananMaden()
    {
        return  bagCapaity/((bagCapaity/tekVurustaToplananMaden) * (oneSec/attackRate)) + moveTime;
    }

}
