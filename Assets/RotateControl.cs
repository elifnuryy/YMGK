using UnityEngine;

public class RotateControl : MonoBehaviour
{
    public RotateAndMirror rotateAndMirror;  // RotateAndMirror scriptini baðla

    void Start()
    {
        // Görev baþlatmak için simetrik modeli oluþtur
        rotateAndMirror.CreateSymmetry();
    }

    void Update()
    {
        // Space tuþuna basýldýðýnda döndürmeyi baþlat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotateAndMirror.StartRotation();
        }

        // Görev tamamlandýðýnda ödül göster (isteðe baðlý)
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotateAndMirror.ResetTask();
            Debug.Log("Görev sýfýrlandý!");
        }
    }
}
