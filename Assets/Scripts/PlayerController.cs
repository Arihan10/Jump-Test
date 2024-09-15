using Mapbox.Unity.Map;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Mapbox.Utils; 

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cam, cineCam;

    PhotonView PV;

    bool location = false;

    [SerializeField] TextMeshProUGUI debugText; 
    
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>(); 

        if (!PV.IsMine) {
            Destroy(cam); 
            Destroy(cineCam);
            return; 
        }

        StartCoroutine(Ping()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (!PV.IsMine) return; 

        if (location) {
            // Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp); 

            debugText.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp; 

            AbstractMap abstractMap = GameObject.Find("CitySimulationMap").GetComponent<AbstractMap>();
            Vector3 newPos = abstractMap.GeoToWorldPosition(new Vector2d(Input.location.lastData.latitude, Input.location.lastData.longitude));

            transform.position = newPos; 
        }
    }

    IEnumerator Ping() {
        if (!Input.location.isEnabledByUser)
            Debug.Log("Location not enabled on device or app does not have permission to access location");

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1) {
            Debug.Log("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed) {
            Debug.LogError("Unable to determine device location");
            yield break;
        } else {
            // Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp); 
            location = true; 
        }

        // Stops the location service if there is no need to query location updates continuously.
        // Input.location.Stop();
    }
}
