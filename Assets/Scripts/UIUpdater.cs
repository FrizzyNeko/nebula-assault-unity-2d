using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        healthSlider = FindFirstObjectByType<Slider>();
    }

    private void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth;
    }

    private void Update()
    {
        scoreText.text = scoreKeeper.Score.ToString("000000000");
        healthSlider.value = playerHealth.GetHealth;
    }




}
