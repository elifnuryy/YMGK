using UnityEngine;

public class SymmetryCreator : MonoBehaviour
{
    public GameObject originalModel;  // Orijinal model
    public Vector3 offset;           // Simetri i�in pozisyon ofseti
    public Camera mainCamera;        // Ana kamera
    public string taskMessage = "G�rev: Nesneyi Hareket Ettir ve Simetrisinin Olu�mas�n� Sa�la"; // G�rev mesaj�

    private GameObject mirroredModel; // Simetrik model
    private bool symmetryCreated = false; // Simetrinin olu�turulup olu�turulmad���n� kontrol eden bayrak
    private bool taskCompleted = false; // G�rev tamamland� m�?

    public float cameraSpeed = 100f; // Kameran�n d�n�� h�z�
    private Vector3 cameraRotation; // Kameran�n mevcut d�n�� a��s�

    void Start()
    {
        CreateSymmetry();
        PositionCamera();
    }

    void Update()
    {
        HandleMovement();
        UpdateSymmetry();
        HandleCameraRotation();
        CheckTaskCompletion();
    }

    void CreateSymmetry()
    {
        if (originalModel == null)
        {
            Debug.LogError("Orijinal model atanmad�!");
            return;
        }

        if (!symmetryCreated)
        {
            // Simetrik modeli ilk kez olu�tur
            mirroredModel = Instantiate(originalModel);
            mirroredModel.name = "MirroredModel";
            symmetryCreated = true;
            Debug.Log(taskMessage); // G�rev mesaj�n� ba�lat
        }
    }

    void UpdateSymmetry()
    {
        if (originalModel != null && mirroredModel != null && !taskCompleted)
        {
            // Simetrik modelin pozisyonunu ve d�n���n� orijinale g�re g�ncelle
            mirroredModel.transform.position = originalModel.transform.position + offset;
            mirroredModel.transform.rotation = originalModel.transform.rotation;

            // X ekseninde �l�e�i ters �evirerek yans�tma yap
            Vector3 scale = mirroredModel.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * -1;
            mirroredModel.transform.localScale = scale;
        }
    }

    void PositionCamera()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Kamera atanmad�! Kameray� 'mainCamera' alan�na s�r�kleyip b�rak�n.");
            return;
        }

        // Orijinal modelin merkezine bakacak �ekilde kameray� konumland�r
        Vector3 centerPosition = originalModel.transform.position + (offset / 2);
        Vector3 cameraOffset = new Vector3(0, 2, -5); // Kameray� yukar� ve geriye konumland�rmak i�in ofset
        mainCamera.transform.position = centerPosition + cameraOffset;
        mainCamera.transform.LookAt(centerPosition);

        // Kameran�n ba�lang�� rotasyonunu kaydet
        cameraRotation = mainCamera.transform.eulerAngles;
    }

    void HandleMovement()
    {
        // Klavye girdileriyle orijinal modeli hareket ettirme
        float moveSpeed = 5f;
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        if (originalModel != null)
        {
            originalModel.transform.Translate(new Vector3(moveX, 0, moveZ), Space.World);
        }
    }

    void HandleCameraRotation()
    {
        if (mainCamera == null) return;

        // Fare hareketleriyle kameray� d�nd�rme
        float mouseX = Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSpeed * Time.deltaTime;

        // X ve Y ekseninde kameray� d�nd�r
        cameraRotation.x -= mouseY;
        cameraRotation.y += mouseX;

        // Kameray� s�n�rl� bir a��ya d�nd�rmek i�in (iste�e ba�l�)
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -80f, 80f); // Yaln�zca yukar�/a�a�� hareketi s�n�rla

        // Kameran�n rotasyonunu uygula
        mainCamera.transform.eulerAngles = cameraRotation;

        // Kameray� merkez noktas�na d�nd�rmesini sa�la
        Vector3 centerPosition = originalModel.transform.position + (offset / 2);
        mainCamera.transform.LookAt(centerPosition);
    }

    void CheckTaskCompletion()
    {
        // E�er oyuncu nesneyi hareket ettirirse ve simetriyi olu�turduysa, g�rev tamamlanm�� olur
        if (!taskCompleted && originalModel.transform.position != Vector3.zero)
        {
            taskCompleted = true;
            Debug.Log("G�rev Tamamland�: Simetri ba�ar�yla olu�turuldu!");
        }
    }
}
