using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance{
        get; private set;
    }
    public int nakit;
    public Text nakitText;
    public int superNakit;
    public Text superNakitText;
    public int bosNakit;
    public Text bosNakitText;
    [SerializeField] int earnedGold;
    [SerializeField] float distanceFactor,radius;
    [SerializeField] GameObject goldPrefab,goldinScene;
    [SerializeField] Ease ease;
    [SerializeField] Text goldText;
    [SerializeField] Transform parent;
    void Awake()
    {
        _instance = this;
    }
    void SetGold(int count)
    {
        nakit += count;
        nakitText.text = nakit.ToString();
    }
    public IEnumerator EarnGoldAnim(int gold)
    {
        var count = 15;
        var earnedGold15 = earnedGold / count;
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(goldPrefab,parent.transform.position,Quaternion.identity, parent);
            list.Add(obj);
        }
        for (int i = 0; i < count; i++)
        {
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i*radius);
            var y = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i*radius);
            var newPos = new Vector3(x,y,0) + parent.transform.position;
            list[i].transform.DOMove(newPos,.2f);
        }
        for (int i = 0; i < count; i++)
        {
            list[i].transform.DOMove(goldinScene.transform.position,.7f).SetEase(ease).OnComplete(()=> SetGold(gold));//.OnComplete(()=> goldFlare.Play());
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(.7f);
        for (int i = 0; i < count; i++)
        {
            Destroy(list[i]);
        }
        nakitText.transform.DOScale(Vector3.one *1.5f,.4f).OnComplete(()=>nakitText.transform.DOScale(Vector3.one,.4f));
    }
}
