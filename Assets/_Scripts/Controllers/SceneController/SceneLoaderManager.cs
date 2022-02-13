using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    [SerializeField] private Text dayCounter;
    [SerializeField] private float transitionTime;
    [SerializeField] private int dayNumber = 1;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        dayCounter.text = "Day: " + dayNumber.ToString();
    }

    private void OnEnable()
    {
        SceneLoaderTrigger.loadScene += LoadNextLevel;
    }

    private void OnDisable()
    {
        SceneLoaderTrigger.loadScene -= LoadNextLevel;
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        dayNumber++;
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetTrigger("Fade");
        dayCounter.text = "Day: " + dayNumber.ToString();
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
