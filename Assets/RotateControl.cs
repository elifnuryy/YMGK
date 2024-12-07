using UnityEngine;

public class RotateControl : MonoBehaviour
{
    public RotateAndMirror rotateAndMirror;  // RotateAndMirror scriptini ba�la

    void Start()
    {
        // G�rev ba�latmak i�in simetrik modeli olu�tur
        rotateAndMirror.CreateSymmetry();
    }

    void Update()
    {
        // Space tu�una bas�ld���nda d�nd�rmeyi ba�lat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotateAndMirror.StartRotation();
        }

        // G�rev tamamland���nda �d�l g�ster (iste�e ba�l�)
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotateAndMirror.ResetTask();
            Debug.Log("G�rev s�f�rland�!");
        }
    }
}
