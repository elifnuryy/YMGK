using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems; // TrackableType için gerekli
using System.Collections.Generic; // List için gerekli

public class ARModelPlacer : MonoBehaviour
{
    public GameObject modelPrefab;  // Yerleþtirilecek model
    private ARRaycastManager raycastManager;
    private GameObject placedModel;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Kullanýcý dokunduðunda raycast iþlemi yapýlacak
            List<ARRaycastHit> hits = new List<ARRaycastHit>(); // List kullanýmý
            if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon)) // TrackableType ile yüzey tanýmlanýyor
            {
                // Yüzeyde bir nokta bulundu
                Pose hitPose = hits[0].pose;
                if (placedModel == null)
                {
                    placedModel = Instantiate(modelPrefab, hitPose.position, hitPose.rotation);
                    placedModel.AddComponent<RotateObject>(); // Döndürme scriptini ekle
                }
                else
                {
                    placedModel.transform.position = hitPose.position;
                    placedModel.transform.rotation = hitPose.rotation;
                }
            }
        }
    }
}
