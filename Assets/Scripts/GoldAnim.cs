using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GoldAnim : MonoBehaviour
{
    [SerializeField] GameObject goldAnimPrefab;
    private static GoldAnim _instance;
    public static GoldAnim instance{get =>_instance;}
    [SerializeField] float distanceFactor,radius;
    [SerializeField] GameObject goldPrefab,goldinScene;
    [SerializeField] Ease ease;
    [SerializeField] Text goldText;
    [SerializeField] Transform parent;
    [SerializeField] Transform goldOlusturulcakTransform;
    int gold;
    void Awake()
    {
        _instance = this;
    }
    void SetGold(int count)
    {
        gold += count;
        goldText.text = gold.ToString();
    }
    public void EarnGoldAnim2(float earnedGold , int count , Transform transform)
    {
        var pos = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
        var obj = Instantiate(goldAnimPrefab,transform.position,Quaternion.identity);
        obj.transform.GetChild(0).GetComponent<TextMesh>().text = GameManager.instance.CaclText(earnedGold);
        obj.transform.DOMove(pos,1f);
        Color color = new Color(255,255,255,0);
        obj.GetComponent<SpriteRenderer>().DOColor(color,1);
        obj.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.DOFade(0,1).OnComplete(()=> Destroy(obj));
    }
    public IEnumerator EarnGoldAnim(int earnedGold , int count , Transform transform)
    {
        
        GameManager.instance.idleMoneyCanvas.GetComponent<Canvas>().enabled = false;
        var earnedGold15 =  earnedGold / count;
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(goldPrefab,goldOlusturulcakTransform.position,Quaternion.identity, parent);
            list.Add(obj);
        }
        for (int i = 0; i < count; i++)
        {
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i*radius);
            var y = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i*radius);
            var newPos = new Vector3(x,y,0) + goldOlusturulcakTransform.position;

            list[i].transform.DOMove(newPos,.5f);
        }
        for (int i = 0; i < count; i++)
        {
            list[i].transform.DOMove(goldinScene.transform.position,.5f).SetEase(ease).OnComplete(()=> GameManager.instance.SetGold(earnedGold15));//.OnComplete(()=> goldFlare.Play());
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < count; i++)
        {
            Destroy(list[i]);
        }
        // goldText.transform.DOScale(Vector3.one *1.5f,.4f).OnComplete(()=>goldText.transform.DOScale(Vector3.one,.4f));
    }
}
