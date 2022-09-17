using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;


public class ClockView : MonoBehaviour
{
    public GameObject timeTextObject;
    string goldenGateUrl = "http://worldtimeapi.org/api/timezone/America/Los_Angeles";


    // Start is called before the first frame update
    void Start()
    { 
    InvokeRepeating("GetDataFromWeb", 0f, 10f);    
    }

    void GetDataFromWeb()
   {

       StartCoroutine(UpdateTime(goldenGateUrl));
   }

    
   IEnumerator UpdateTime(string uri)
    {
        //Regualt 12hr clock 
        if(timeTextObject.name == "Regular Time"){
                    using (UnityWebRequest sfWebRequest = UnityWebRequest.Get(uri)){
                    // Request and wait for the desired page.
                    yield return sfWebRequest.SendWebRequest();


                    if (sfWebRequest.result ==  UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.Log(": Error: " + sfWebRequest.error);
                    }
                    else
                    {
                        Debug.Log("Time Zone received: " + sfWebRequest.downloadHandler.text );
                        int startTime = sfWebRequest.downloadHandler.text.IndexOf("datetime",0);
                        int endTime = sfWebRequest.downloadHandler.text.IndexOf(",",startTime);
                        string time = sfWebRequest.downloadHandler.text.Substring(startTime + 22, endTime - startTime - 39);
                        //double tempTime = float.Parse(sfWebRequest.downloadHandler.text.Substring(startTime+12, (endTime-startTime-5)));
                        // int time = Mathf.RoundToInt((float)tempTime);
                        Debug.Log("substring is: " + sfWebRequest.downloadHandler.text.Substring(startTime + 22, endTime - startTime - 39));
                        timeTextObject.GetComponent<TextMeshPro>().text = time;

                    }


            }
            Debug.Log("Regular Time after: " + timeTextObject.name);

        }
        //Military 24hr clock
        else{
            timeTextObject.GetComponent<TextMeshPro>().text = System.DateTime.Now.ToString(" HH:mm");
            Debug.Log("Military Time after: " + timeTextObject.name);
        }

    }
}
