using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [SerializeField] private Transform coinsParent;
    [SerializeField] private Text winnerText;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject ResetButton;
    [SerializeField] private GameObject PauseMenu;

    void OnEnable()
    {
        pauseButton.SetActive(false);
        ResetButton.SetActive(false);
        PauseMenu.SetActive(false);
    }
    public void Initialize(int playerInt, Sprite graphic)
    {
        gameObject.SetActive(true);
        board.SetActive(false);

        if(playerInt == -10)
        {
            coinsParent.gameObject.SetActive(false);
            winnerText.text = "It’s a Draw";
        }

        else
        {
            playerInt++;
            winnerText.text = "Player " + playerInt + " Wins!";
            foreach (Transform child in coinsParent)
                child.GetComponent<Image>().sprite = graphic;
        }
    }
}
