using Mapbox.Unity.Map;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Mapbox.Utils;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cam, cineCam;

    PhotonView PV;

    bool location = false;

    [SerializeField] TextMeshProUGUI debugText;
    float maxTravelDist = 0.5f;

    public TextMeshProUGUI clueText;
    public TextMeshProUGUI taskText;

    public JsonBreakdown breakdown;

    int loc = 0;
    
    
    // Start is called before the first frame update
    async void Start()
    {
        PV = GetComponent<PhotonView>(); 

        if (!PV.IsMine) {
            Destroy(cam); 
            Destroy(cineCam);
            return; 
        }

        StartCoroutine(Ping());
        //Input.location.lastData.latitude
    }


    IEnumerator GetRequest(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {

            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("ngrok-skip-browser-warning", "69420");

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError) {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            } else {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                var courses = webRequest.downloadHandler.text;
                JsonBreakdown breakdown = JsonBreakdown.CreateFromJSON(courses);
                print(breakdown.players.Count);
                this.breakdown = breakdown;
            }
        }
    }

    [System.Serializable]
    public class JsonBreakdown {
        public List<Location> players;

        public static JsonBreakdown CreateFromJSON(string jsonString) {
            return JsonUtility.FromJson<JsonBreakdown>(jsonString);
        }

        [System.Serializable]
        public class Location {
            public string _id;
            public string address;
            public float lat;
            public float longg;
            public string clueType;
            public string clueText;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(breakdown == null) {
            clueText.text = "Loading...";
        } else {
            //clueText.text = "OK!...";
            clueText.text = breakdown.players[loc].clueText;

        }
    }

    private void FixedUpdate() {
        if (!PV.IsMine) return; 

        if (location) {
            // Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp); 

           // debugText.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp; 

            AbstractMap abstractMap = GameObject.Find("CitySimulatorMap").GetComponent<AbstractMap>();
            Vector3 target = abstractMap.GeoToWorldPosition(new Vector2d(Input.location.lastData.latitude, Input.location.lastData.longitude));

           debugText.text = "A: " + (target - transform.position).magnitude + " \n B: " + maxTravelDist;


            if ((target - transform.position).magnitude <= maxTravelDist || (target - transform.position).magnitude >= 10  * maxTravelDist) {
                transform.position = target;
            } else {

                transform.position += (target - transform.position).normalized * maxTravelDist * Time.deltaTime;
            }
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

            Coroutine c = StartCoroutine(GetRequest("https://0358-192-54-222-153.ngrok-free.app/locations/coordinates?lat=" + Input.location.lastData.latitude + "&long=" + Input.location.lastData.longitude + "&number=4"));
        }




        // Stops the location service if there is no need to query location updates continuously.
        // Input.location.Stop();
    }
}
