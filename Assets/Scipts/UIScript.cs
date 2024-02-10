using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public Text cashText;
    public Text manPowerText;
    public GameObject playerBase;
    public BaseScript playerBaseScript;
    public GameObject enemyBase;
    public BaseScript enemyBaseScript;
    public Image pauseBox;
    private bool gameEnded = false;
    [SerializeField] private Slider recruitmentBar;
    [SerializeField] private Slider musicVolume;

    private void Start() {
        playerBaseScript = playerBase.GetComponent<BaseScript>();
        enemyBaseScript = enemyBase.GetComponent<BaseScript>();
    }
    void Update()
    {
        cashText.text = playerBaseScript.money.ToString("0"); // Update Cash
        manPowerText.text = playerBaseScript.manPower.ToString("0"); // Update Cash
        if (Input.GetKeyDown(KeyCode.Escape))ActivateBox(pauseBox);
        if (playerBaseScript.health <= 0 && !gameEnded) {
            gameEnded = true;
            transform.Find("Lose").gameObject.SetActive(true);
        }
        if (enemyBaseScript.health <= 0) {
            gameEnded = true;
            transform.Find("Win").gameObject.SetActive(true);
        }
        if (playerBaseScript.recruitingUnit) {
            recruitmentBar.value = playerBaseScript.recruitmentPercentage;
        }
        else recruitmentBar.value = 0;
        if (pauseBox.IsActive()) {
            transform.GetComponent<AudioSource>().volume = musicVolume.value;
        }

    }
    public void ActivateBox(Image boxName) {
        boxName.gameObject.SetActive(!boxName.IsActive());

    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame() {
        Application.Quit();
    }

}
