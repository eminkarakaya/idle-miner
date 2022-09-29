using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MadenIslemeMachine : MonoBehaviour
{
    [SerializeField] Manager manager;
    [SerializeField] int gold;
    [SerializeField] Text goldText;
    public void SetGold(int value)
    {
        gold = value;
        goldText.text = GameManager.instance.CaclText(gold);
    }
    public int GetGold()
    {
        return gold;
    }
}
