using UnityEngine;
using TMPro;

/// <summary>
/// Handles the GameOver input for the name of the player.
/// This is entered in the highscores menu if applicable.
/// </summary>
public class NameInputUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] letters = null;
    [SerializeField] private PlayerString playerName = null;

    private char[] alphaNum = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
    private int[] positions;

    void OnEnable()
    {
        positions = new int[letters.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = 0;
        }
    }

    void Update()
    {
        playerName.value = "";
        for (int i = 0; i < letters.Length; i++)
        {

            playerName.value += letters[i].text;
        }
    }

    /// <summary>
    /// Cycles through the letters in a forward manner.
    /// </summary>
    /// <param name="index"></param>
    public void NextLetter(int index)
    {
        positions[index]++;
        if (positions[index] == alphaNum.Length) positions[index] = 0;
        letters[index].text = alphaNum[positions[index]].ToString();
    }

    /// <summary>
    /// Cycles through the letters in a backward manner,
    /// </summary>
    /// <param name="index"></param>
    public void PrevLetter(int index)
    {
        positions[index]--;
        if (positions[index] < 0) positions[index] = alphaNum.Length - 1;
        letters[index].text = alphaNum[positions[index]].ToString();
    }
}
