using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GameLogic : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 100f;

    void Awake() {
        //AirConsole.instance.onMessage += OnMessage;    
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f){
            controller.Move(direction * speed * Time.deltaTime);
        }
    }

    void OnMessage(int fromDeviceID, JToken data){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        Debug.Log("message from" + fromDeviceID + ", data: " + data);
        if (data ["action"] != null && data ["action"].ToString ().Equals ("interact1")){
            Camera.main.backgroundColor = new Color (Random.Range (0f, 1f), Random.Range(0f,1f), Random.Range(0f,1f));
            
            transform.position += Vector3.forward  * 20.0f;

        }else if(data ["action"] != null && data ["action"].ToString ().Equals ("interact2")){
            Camera.main.backgroundColor = Color.red;
            transform.position += Vector3.forward  * -20.0f;

        }else if(data ["action"] != null && data ["action"].ToString ().Equals ("interact3")){
            Camera.main.backgroundColor = Color.blue;
            transform.position += Vector3.left * 20.0f;

        }else if(data ["action"] != null && data ["action"].ToString ().Equals ("interact4")){
            Camera.main.backgroundColor = Color.yellow;
            transform.position += Vector3.right * 20.0f;
        }
    }

    /*
       void OnDestroy() {
        if(AirConsole.instance != null){
            AirConsole.instance.onMessage -= OnMessage;
        }    
    }
    */
}
