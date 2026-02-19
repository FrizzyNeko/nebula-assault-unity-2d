using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score = 0;
    public float Score => score;

    public void ModifyScore(int scoreToAdd)
    {
        score += scoreToAdd;
        score = Mathf.Clamp(score, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        score = 0;
    }
}
