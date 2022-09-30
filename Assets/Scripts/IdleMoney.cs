using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class IdleMoney : MonoBehaviour , IDataPersistence
{
    public string url = "http://worldtimeapi.org/api/ip";

    public string sDate;
    void Start()
    {
        // StartCoroutine(CheckInternet());
       
    }
    // private IEnumerator CheckInternet()
    // {
    //     using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
    //     {
    //         yield return webRequest.SendWebRequest();
    //         if(webRequest.result != UnityWebRequest.Result.ConnectionError)
    //         {
    //             Debug.Log("success Internet");
    //             StartCoroutine(CheckDate());
    //         }
    //     }
    // }
    public void LoadData(GameData data)
    {
        sDate = data.sonGirisTarihi;
        GecenSureyiHesapla();
    }
    public void SaveData(ref GameData data)
    {
        _Time time = NewTime.GetTime();
        data.sonGirisTarihi =  time.datetime; //.ToString("yyyy-MM-dd-HH-mm-ss");
    }
    // private IEnumerator CheckDate()
    // {
    //     using(UnityWebRequest webRequest = UnityWebRequest.Get(url))
    //     {
    //         yield return webRequest.SendWebRequest();
    //         if(webRequest.result != UnityWebRequest.Result.ConnectionError)
    //         {
                
    //         }
    //     }
    // }
    public int GecenSureyiHesapla()
    {
        string dateOld = sDate;
        if(string.IsNullOrEmpty(dateOld))
        {
            Debug.Log("firstgame");
            // PlayerPrefs.SetString("PlayDateOld",sDate);
        }
        else
        {
            _Time time = NewTime.GetTime();
            DateTime _dateNow = Convert.ToDateTime(time.datetime);
            DateTime _dateOld = Convert.ToDateTime(sDate);
            TimeSpan diff = _dateNow.Subtract(_dateOld);
            return diff.Seconds;
        }
        return 0;
    }
}
