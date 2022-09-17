using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class WeatherAPIScript : MonoBehaviour
{
    public GameObject WeatherTextObject;

        // add your personal API key after APPID= and before &units=
       string chicagoUrl = "https://api.openweathermap.org/data/2.5/weather?&q=chicago&appid=7b65d5572c61f467ba2a115e4fcbcfb1&units=imperial";
       string goldenGateUrl = "https://api.openweathermap.org/data/2.5/weather?&q=san francisco&appid=7b65d5572c61f467ba2a115e4fcbcfb1&units=metric";

   
    void Start()
    {

    // wait a couple seconds to start and then refresh every 900 seconds

       InvokeRepeating("GetDataFromWeb", 2f, 900f);
   }

   void GetDataFromWeb()
   {

       StartCoroutine(GetRequest(chicagoUrl, goldenGateUrl));
   }

    IEnumerator GetRequest(string uri, string uri2)
    {
        using (UnityWebRequest chicagoWebRequest = UnityWebRequest.Get(uri))
        {
                // Request and wait for the desired page.
                yield return chicagoWebRequest.SendWebRequest();


                if (chicagoWebRequest.result ==  UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(": Error: " + chicagoWebRequest.error);
                }
                else
                {
                    // print out the weather data to make sure it makes sense
                    Debug.Log(":\nChicago Received: " + chicagoWebRequest.downloadHandler.text);

                    // this code will NOT fail gracefully, so make sure you have
                    // your API key before running or you will get an error

                    // grab the current temperature and simplify it if needed
                    int startTemp = chicagoWebRequest.downloadHandler.text.IndexOf("temp",0);
                    int endTemp = chicagoWebRequest.downloadHandler.text.IndexOf(",",startTemp);
                    double tempF = float.Parse(chicagoWebRequest.downloadHandler.text.Substring(startTemp+6, (endTemp-startTemp-6)));
                    int easyTempF = Mathf.RoundToInt((float)tempF);
                    Debug.Log ("integer temperature is: " + easyTempF.ToString());
                    Debug.Log ("\nStartTemp: " + startTemp.ToString());
                    Debug.Log ("\nEndTemp: " + endTemp.ToString());
                    Debug.Log ("\nTempF: " + tempF.ToString());
                    Debug.Log ("\nEasyTempF: " + easyTempF.ToString());

                    int startConditions = chicagoWebRequest.downloadHandler.text.IndexOf("main",0);
                    int endConditions = chicagoWebRequest.downloadHandler.text.IndexOf(",",startConditions);
                    string conditions = chicagoWebRequest.downloadHandler.text.Substring(startConditions+7, (endConditions-startConditions-8));
                    Debug.Log(conditions);
                    Debug.Log("Name of the weather object: " + WeatherTextObject.name);


                    WeatherTextObject.GetComponent<TextMeshPro>().text = "" + easyTempF.ToString() + "°F\n" + conditions;
                }
        }

        if(WeatherTextObject.name == "Golden Gate Weather"){
            using (UnityWebRequest sfWebRequest = UnityWebRequest.Get(uri2)){
                    // Request and wait for the desired page.
                    yield return sfWebRequest.SendWebRequest();


                    if (sfWebRequest.result ==  UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.Log(": Error: " + sfWebRequest.error);
                    }
                    else
                    {
                    int startTemp = sfWebRequest.downloadHandler.text.IndexOf("temp",0);
                    int endTemp = sfWebRequest.downloadHandler.text.IndexOf(",",startTemp);
                    double tempF = float.Parse(sfWebRequest.downloadHandler.text.Substring(startTemp+6, (endTemp-startTemp-6)));
                    int easyTempF = Mathf.RoundToInt((float)tempF);
                    Debug.Log ("integer temperature is: " + easyTempF.ToString());
                    Debug.Log ("\nStartTemp: " + startTemp.ToString());
                    Debug.Log ("\nEndTemp: " + endTemp.ToString());
                    Debug.Log ("\nTempF: " + tempF.ToString());
                    Debug.Log ("\nEasyTempF: " + easyTempF.ToString());

                    int startConditions = sfWebRequest.downloadHandler.text.IndexOf("main",0);
                    int endConditions = sfWebRequest.downloadHandler.text.IndexOf(",",startConditions);
                    string conditions = sfWebRequest.downloadHandler.text.Substring(startConditions+7, (endConditions-startConditions-8));
                    Debug.Log(conditions);
                    Debug.Log("Name of the weather object: " + WeatherTextObject.name); 
                    WeatherTextObject.GetComponent<TextMeshPro>().text = "" + easyTempF.ToString() + "°C\n" + conditions;
                       
                        // Debug.Log(":\nSan Francisco Received: " + sfWebRequest.downloadHandler.text);
                        // WeatherTextObject.GetComponent<TextMeshPro>().text = "Worked MF!!!";
                    }

            }
        }

    }
}

