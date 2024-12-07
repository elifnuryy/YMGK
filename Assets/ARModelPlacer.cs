using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems; // TrackableType i�in gerekli
using System.Collections.Generic; // List i�in gerekli

public class ARModelPlacer : MonoBehaviour
{
    public GameObject modelPrefab;  // Yerle�tirilecek model
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
            // Kullan�c� dokundu�unda raycast i�lemi yap�lacak
            List<ARRaycastHit> hits = new List<ARRaycastHit>(); // List kullan�m�
            if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon)) // TrackableType ile y�zey tan�mlan�yor
            {
                // Y�zeyde bir nokta bulundu
                Pose hitPose = hits[0].pose;
                if (placedModel == null)
                {
                    placedModel = Instantiate(modelPrefab, hitPose.position, hitPose.rotation);
                    placedModel.AddComponent<RotateObject>(); // D�nd�rme scriptini ekle
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
