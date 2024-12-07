using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject objectToRotate;  // Döndürülecek obje
    public float rotationSpeed = 30f;  // Döndürme hýzý

    private bool isRotating = false;

    void Update()
    {
        // Space tuþuna basýldýðýnda döndürmeyi baþlat veya durdur
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRotating = !isRotating;
        }

        // Eðer döndürme aktifse, objeyi döndür
        if (isRotating)
        {
            objectToRotate.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
