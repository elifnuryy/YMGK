using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject objectToRotate;  // D�nd�r�lecek obje
    public float rotationSpeed = 30f;  // D�nd�rme h�z�

    private bool isRotating = false;

    void Update()
    {
        // Space tu�una bas�ld���nda d�nd�rmeyi ba�lat veya durdur
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRotating = !isRotating;
        }

        // E�er d�nd�rme aktifse, objeyi d�nd�r
        if (isRotating)
        {
            objectToRotate.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
