// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;

// public class IdleMoney : MonoBehaviour
// {
//     public string url = "www.google.com";
//     private IEnumerator CheckInternet()
//     {
//         using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
//         {
//             yield return webRequest.SendWebRequest();
//             if(!webRequest.isNetworkError)
//             {
//                 Debug.Log("success Internet");
//                 StartCoroutine(CheckDate());
//             }
//         }
//     }
//     // private IEnumerator CheckDate()
//     // {

//     // }
// }
