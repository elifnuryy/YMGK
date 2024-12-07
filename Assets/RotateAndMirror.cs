using UnityEngine;

public class RotateAndMirror : MonoBehaviour
{
    public GameObject originalModel;  // Orijinal model
    public Vector3 rotationSpeed = new Vector3(0, 30, 0);  // D�nd�rme h�z� (derece/saniye)
    private GameObject mirroredModel; // Simetrik model

    private bool isRotating = false;

    void Update()
    {
        // E�er d�nd�rme aktifse, nesneyi d�nd�r
        if (isRotating)
        {
            // Nesneyi d�nd�r
            originalModel.transform.Rotate(rotationSpeed * Time.deltaTime);

            // Simetrik modeli de d�nd�r
            if (mirroredModel != null)
            {
                mirroredModel.transform.rotation = originalModel.transform.rotation;
            }
        }
    }

    // Space tu�una bas�ld���nda d�nd�rme i�lemini ba�latacak fonksiyon
    public void StartRotation()
    {
        isRotating = !isRotating; // D�nd�rme i�lemi ba�lat veya durdur
    }

    // Simetrik modeli olu�tur
    public void CreateSymmetry()
    {
        if (originalModel == null)
        {
            Debug.LogError("Orijinal model atanmad�!");
            return;
        }

        // Simetrik model olu�tur
        mirroredModel = Instantiate(originalModel);

        // X ekseninde �l�e�i ters �evirerek yans�tma yap
        Vector3 scale = mirroredModel.transform.localScale;
        scale.x *= -1;
        mirroredModel.transform.localScale = scale;
    }

    // G�rev s�f�rlama (iste�e ba�l�)
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
