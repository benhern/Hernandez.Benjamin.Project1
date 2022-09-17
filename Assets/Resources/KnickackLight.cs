using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KnickackLight : MonoBehaviour
{
    public GameObject KnickBaseObject;
    public GameObject LightObject;
    int counter = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

        // Update is called once per frame
    void Update()
    {
        double bottom = Math.Round(KnickBaseObject.transform.rotation.z, 2);
        double rotation = Math.Abs(bottom);


        Debug.Log(
        "Knick is: " + KnickBaseObject.name +
        "\nRotation z is: " + rotation + 
        "\nCounter is" + counter +
        "\nHere is Light's intensity before: " + LightObject.GetComponent<Light>().intensity +
        "\nHere is Light's Name: " + LightObject.GetComponent<Light>().name);

        

        if(rotation > 0.90){

            if(counter == 1){
                counter = 0;
                LightObject.GetComponent<Light>().intensity = 0f;
            }            
            else if(LightObject.GetComponent<Light>().name == "GSW Light"){
                counter = 1;
                LightObject.GetComponent<Light>().intensity = 0.5f;
            }
            else if(LightObject.GetComponent<Light>().name == "Chicago Light"){
                counter = 1;
                LightObject.GetComponent<Light>().intensity = 1.5f;
            }

        }

            Debug.Log("Checking.." +"\nCounter is: " + counter);  
            Debug.Log("Here is Light's intensity after condition: " + LightObject.GetComponent<Light>().intensity);

        }
    
}
