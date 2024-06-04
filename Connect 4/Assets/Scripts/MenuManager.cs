using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MoonActive.Connect4;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Transform SpawnContainersParent;
    [SerializeField] private Transform SpawnColidersParent;
    [SerializeField] private Sprite soundOnTexture;
    [SerializeField] private Sprite soundOffTexture;
    [SerializeField] private Slider[] volumeSliders;
    [SerializeField] private Image[] volumeIcons;
    [SerializeField] private AudioSource clickSound;

    private int numberOfPlayers;
    private int numberOfComps;

    void Start()
    {
        ChangeVolumeSliders();
    }
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PauseGame()
    {
        foreach (Transform SpawnPoint in SpawnContainersParent)
            SpawnPoint.gameObject.GetComponent<Button>().interactable = false;
    }
    public void ResumeGame()
    {
        foreach (Transform SpawnPoint in SpawnContainersParent)
            SpawnPoint.gameObject.GetComponent<Button>().interactable = true;
    }

    public void SetNumberOfPlayers(int i)
    {
        numberOfPlayers = i;
    }
    public void SetNumberOfComps(int i)
    {
        numberOfComps = i;
    }
    public void SendGameParameters()
    {
        gridManager.SetGameParameters(numberOfPlayers, numberOfComps);
    }
    public void ButtonSound()
    {
        clickSound.Stop();
        clickSound.Play();
    }
    public void ChangeVolume(System.Single volume)
    {
        AudioListener.volume = volume;
        ChangeVolumeSliders();
    }
    public void DeleteDisks()
    {
        foreach(Transform SpawnPoint in SpawnContainersParent)
        {
            foreach(Transform child in SpawnPoint)
            {
                if (child.gameObject.GetComponent<Disk>())
                    Destroy(child.gameObject);
            }
        }
        foreach (Transform child in SpawnColidersParent)
        {
            Collider2D collider = child.gameObject.GetComponent<Collider2D>();
            if (collider != null)
                collider.enabled = false;
        }
    }
    public void ChangeVolumeSliders()
    {
        int i = 0;
        while(i < volumeSliders.Length)
        {
            if (AudioListener.volume == 0)
                volumeIcons[i].sprite = soundOffTexture;
            else
                volumeIcons[i].sprite = soundOnTexture;

            volumeSliders[i].value = AudioListener.volume;
            i++;
        }
    }
}
