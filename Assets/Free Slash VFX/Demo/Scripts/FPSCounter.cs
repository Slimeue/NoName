using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MaykerStudio.Demo
{
    public class FPSCounter : MonoBehaviour
    {
        // Add this to the class variables
        [Range(1, 100)] public int smoothingFrames = 60;
        private readonly Queue<float> frameTimes = new();
        private TextMeshProUGUI textFPS;

        private void Start()
        {
            textFPS = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            frameTimes.Enqueue(Time.deltaTime);
            if (frameTimes.Count > smoothingFrames) frameTimes.Dequeue();

            float averageDelta = 0;
            foreach (var t in frameTimes) averageDelta += t;
            averageDelta /= frameTimes.Count;

            textFPS.text = $"FPS: {Mathf.RoundToInt(1f / averageDelta)}";
        }
    }
}