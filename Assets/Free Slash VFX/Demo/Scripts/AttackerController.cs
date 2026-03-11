using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace MaykerStudio.Demo
{
    public class AttackerController : MonoBehaviour
    {
        public List<GameObject> SlashesProjectile;

        [SerializeField] private int maxAnim = 2;

        [SerializeField] private Animator playerAnim;

        [SerializeField] private Transform slashContainer;

        [Header("UI")] [SerializeField] private TextMeshProUGUI currentPrefabName;

        [SerializeField] private TextMeshProUGUI currentPlayableName;

        [SerializeField] private Slider timeScaleSlider;

        [SerializeField] private TextMeshProUGUI timeScaleValueTxt;

        [SerializeField] private Toggle showLooping;

        [SerializeField] private Transform[] cameras;

        private int currentAnim;

        private int currentCam;

        private GameObject currentSlash;
        private GameObject currentSlashInstance;

        private ElementsShowCase elementsShowCase;

        private Camera mainCamera;
        public static AttackerController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (Application.targetFrameRate != 59)
                Application.targetFrameRate = 59;

            mainCamera = Camera.main;
            mainCamera.transform.SetParent(cameras[currentCam], false);
            mainCamera.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            elementsShowCase = FindAnyObjectByType<ElementsShowCase>();
            currentSlash = SlashesProjectile[0];

            currentPrefabName.text = currentSlash.name;
            currentPlayableName.text = "Sword Attack " + currentAnim;

            timeScaleSlider.value = Time.timeScale;
            timeScaleSlider.onValueChanged.AddListener(value =>
            {
                Time.timeScale = value;
                timeScaleValueTxt.text = value.ToString("0.00");
            });

            showLooping.onValueChanged.AddListener(value =>
            {
                if (!value)
                    elementsShowCase.StopAllParticles();
                else
                    elementsShowCase.StartAllParticles();
            });

            showLooping.isOn = showLooping.isOn;

            SpawnSlash();
        }

        private void Update()
        {
#if ENABLE_INPUT_SYSTEM
            if (Mouse.current.rightButton.wasPressedThisFrame) playerAnim.SetTrigger("Attack");
#else
        if (Input.GetMouseButtonDown(1))
        {
            playerAnim.SetTrigger("Attack");
        }
#endif
        }

        public void NextPrefab()
        {
            var index = SlashesProjectile.IndexOf(currentSlash);
            index++;
            if (index >= SlashesProjectile.Count)
                index = 0;

            currentSlash = SlashesProjectile[index];
            currentPrefabName.text = currentSlash.name;

            SpawnSlash();
        }

        public void PreviousPrefab()
        {
            var index = SlashesProjectile.IndexOf(currentSlash);
            index--;
            if (index < 0)
                index = SlashesProjectile.Count - 1;

            currentSlash = SlashesProjectile[index];
            currentPrefabName.text = currentSlash.name;

            SpawnSlash();
        }

        public void NextPlayable()
        {
            currentAnim++;
            if (currentAnim >= maxAnim)
                currentAnim = 0;

            playerAnim.SetInteger("Current", currentAnim);
            currentPlayableName.text = "Sword Attack " + currentAnim;
        }

        public void PreviousPlayable()
        {
            currentAnim--;
            if (currentAnim < 0)
                currentAnim = maxAnim - 1;

            playerAnim.SetInteger("Current", currentAnim);
            currentPlayableName.text = "Sword Attack " + currentAnim;
        }

        public void NextCam()
        {
            currentCam++;
            if (currentCam >= cameras.Length)
                currentCam = 0;

            mainCamera.transform.SetParent(cameras[currentCam], false);
            mainCamera.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public void PreviousCam()
        {
            currentCam--;
            if (currentCam < 0)
                currentCam = cameras.Length - 1;

            mainCamera.transform.SetParent(cameras[currentCam], false);
            mainCamera.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        private void SpawnSlash(bool destroy = true)
        {
            foreach (Transform child in slashContainer)
                if (child != null && destroy)
                    Destroy(child.gameObject);

            currentSlashInstance = Instantiate(currentSlash, slashContainer);
            currentSlashInstance.transform.localPosition = Vector3.zero;
        }

        public void SpawnProjectile()
        {
            SpawnSlash(false);

            if (!currentSlashInstance || !currentSlashInstance.TryGetComponent(out Projectile projectile))
                return;

            projectile.transform.SetParent(null, true);
            projectile.transform.eulerAngles = Quaternion.Euler(0, 0, slashContainer.eulerAngles.z).eulerAngles;
            projectile.Fire();
        }
    }
}