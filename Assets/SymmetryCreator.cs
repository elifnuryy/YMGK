using UnityEngine;

public class SymmetryCreator : MonoBehaviour
{
    public GameObject originalModel;  // Orijinal model
    public Vector3 offset;           // Simetri için pozisyon ofseti
    public Camera mainCamera;        // Ana kamera
    public string taskMessage = "Görev: Nesneyi Hareket Ettir ve Simetrisinin Oluþmasýný Saðla"; // Görev mesajý

    private GameObject mirroredModel; // Simetrik model
    private bool symmetryCreated = false; // Simetrinin oluþturulup oluþturulmadýðýný kontrol eden bayrak
    private bool taskCompleted = false; // Görev tamamlandý mý?

    public float cameraSpeed = 100f; // Kameranýn dönüþ hýzý
    private Vector3 cameraRotation; // Kameranýn mevcut dönüþ açýsý

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
            Debug.LogError("Orijinal model atanmadý!");
            return;
        }

        if (!symmetryCreated)
        {
            // Simetrik modeli ilk kez oluþtur
            mirroredModel = Instantiate(originalModel);
            mirroredModel.name = "MirroredModel";
            symmetryCreated = true;
            Debug.Log(taskMessage); // Görev mesajýný baþlat
        }
    }

    void UpdateSymmetry()
    {
        if (originalModel != null && mirroredModel != null && !taskCompleted)
        {
            // Simetrik modelin pozisyonunu ve dönüþünü orijinale göre güncelle
            mirroredModel.transform.position = originalModel.transform.position + offset;
            mirroredModel.transform.rotation = originalModel.transform.rotation;

            // X ekseninde ölçeði ters çevirerek yansýtma yap
            Vector3 scale = mirroredModel.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * -1;
            mirroredModel.transform.localScale = scale;
        }
    }

    void PositionCamera()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Kamera atanmadý! Kamerayý 'mainCamera' alanýna sürükleyip býrakýn.");
            return;
        }

        // Orijinal modelin merkezine bakacak þekilde kamerayý konumlandýr
        Vector3 centerPosition = originalModel.transform.position + (offset / 2);
        Vector3 cameraOffset = new Vector3(0, 2, -5); // Kamerayý yukarý ve geriye konumlandýrmak için ofset
        mainCamera.transform.position = centerPosition + cameraOffset;
        mainCamera.transform.LookAt(centerPosition);

        // Kameranýn baþlangýç rotasyonunu kaydet
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

        // Fare hareketleriyle kamerayý döndürme
        float mouseX = Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSpeed * Time.deltaTime;

        // X ve Y ekseninde kamerayý döndür
        cameraRotation.x -= mouseY;
        cameraRotation.y += mouseX;

        // Kamerayý sýnýrlý bir açýya döndürmek için (isteðe baðlý)
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -80f, 80f); // Yalnýzca yukarý/aþaðý hareketi sýnýrla

        // Kameranýn rotasyonunu uygula
        mainCamera.transform.eulerAngles = cameraRotation;

        // Kamerayý merkez noktasýna döndürmesini saðla
        Vector3 centerPosition = originalModel.transform.position + (offset / 2);
        mainCamera.transform.LookAt(centerPosition);
    }

    void CheckTaskCompletion()
    {
        // Eðer oyuncu nesneyi hareket ettirirse ve simetriyi oluþturduysa, görev tamamlanmýþ olur
        if (!taskCompleted && originalModel.transform.position != Vector3.zero)
        {
            taskCompleted = true;
            Debug.Log("Görev Tamamlandý: Simetri baþarýyla oluþturuldu!");
        }
    }
}
