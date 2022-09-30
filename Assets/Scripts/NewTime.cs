using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
public static class NewTime 
{
    public static _Time GetTime()
    {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create("http://worldtimeapi.org/api/ip");
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<_Time>(json);
    }
}
