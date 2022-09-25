using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filtrele : MonoBehaviour
{
    public List<Transform> allParentCvs;
    public Transform yuklemeParent,ucuzParent,attackParent,moveParent,agirlikParent;
    AsansorManagerPanel asansorManagerPanel;
    void Start()
    {
        allParentCvs.Add(yuklemeParent);
        allParentCvs.Add(ucuzParent);
        allParentCvs.Add(attackParent);
        allParentCvs.Add(moveParent);
        allParentCvs.Add(agirlikParent);
        asansorManagerPanel = FindObjectOfType<AsansorManagerPanel>();
    }
    public void MoveSpeedManagerFiltre(Manager manager)
    {
        for (int i = 0; i < allParentCvs.Count; i++)
        {
            allParentCvs[i].gameObject.SetActive(false);
            if(allParentCvs[i] == moveParent)
            {
                moveParent.gameObject.SetActive(true);
            }
        }
    }
    public void AttackFiltre()
    {
        for (int i = 0; i < allParentCvs.Count; i++)
        {
            allParentCvs[i].gameObject.SetActive(false);
            if(allParentCvs[i] == attackParent)
            {
                attackParent.gameObject.SetActive(true);
            }
        }
    }
    public void UcuzlatFiltre()
    {
        for (int i = 0; i < allParentCvs.Count; i++)
        {
            allParentCvs[i].gameObject.SetActive(false);
            if(allParentCvs[i] == ucuzParent)
            {
                ucuzParent.gameObject.SetActive(true);
            }
        }
    }
}
