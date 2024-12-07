using UnityEngine;

public class RotateAndMirror : MonoBehaviour
{
    public GameObject originalModel;  // Orijinal model
    public Vector3 rotationSpeed = new Vector3(0, 30, 0);  // Döndürme hýzý (derece/saniye)
    private GameObject mirroredModel; // Simetrik model

    private bool isRotating = false;

    void Update()
    {
        // Eðer döndürme aktifse, nesneyi döndür
        if (isRotating)
        {
            // Nesneyi döndür
            originalModel.transform.Rotate(rotationSpeed * Time.deltaTime);

            // Simetrik modeli de döndür
            if (mirroredModel != null)
            {
                mirroredModel.transform.rotation = originalModel.transform.rotation;
            }
        }
    }

    // Space tuþuna basýldýðýnda döndürme iþlemini baþlatacak fonksiyon
    public void StartRotation()
    {
        isRotating = !isRotating; // Döndürme iþlemi baþlat veya durdur
    }

    // Simetrik modeli oluþtur
    public void CreateSymmetry()
    {
        if (originalModel == null)
        {
            Debug.LogError("Orijinal model atanmadý!");
            return;
        }

        // Simetrik model oluþtur
        mirroredModel = Instantiate(originalModel);

        // X ekseninde ölçeði ters çevirerek yansýtma yap
        Vector3 scale = mirroredModel.transform.localScale;
        scale.x *= -1;
        mirroredModel.transform.localScale = scale;
    }

    // Görev sýfýrlama (isteðe baðlý)
    public void ResetTask()
    {
        isRotating = false;
        originalModel.transform.rotation = Quaternion.identity;
        if (mirroredModel != null)
        {
            mirroredModel.transform.rotation = Quaternion.identity;
        }
    }
}
