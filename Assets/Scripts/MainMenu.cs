using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject previousButton;
    [SerializeField] GameObject[] availableMenus, pages;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource bgm, ui;
    [SerializeField] AudioClip music, ingameMusic, click;
    [SerializeField] Slider uiValue, instrumentValue, musicValue;
    [SerializeField] TextMeshProUGUI uiPercent, instrumentPercent, musicPercent;

    private bool isPlaying;
    private int currentPage = 0;
    private readonly WaitForSeconds startupVolumeDelay = new WaitForSeconds(0.5f);

    void Update()
    {
        if (uiPercent != null && uiPercent.gameObject.activeInHierarchy)
        {
            uiPercent.text = uiValue.value + 80 + "%";
            
            if (instrumentPercent != null)
                instrumentPercent.text = instrumentValue.value + 80 + "%";

            if (musicPercent != null)
                musicPercent.text = musicValue.value + 80 + "%";
        }
    }

    void Start()
    {
        bgm.volume = 0f;
        StartCoroutine(StartVolume());
    }

    private IEnumerator StartVolume()
    {
        while (bgm.volume < 0.054f)
        {
            bgm.volume += 0.002f;
            yield return startupVolumeDelay;
        }
    }

    public void ShowMenu(GameObject menuObject)
    {
        foreach (GameObject obj in availableMenus)
        {
            if (obj != menuObject)
                obj.SetActive(false);
        }

        menuObject.SetActive(true);

        if (menuObject.name == "How2PlayMenu")
        {
            currentPage = 0;
            UpdatePages(currentPage);
        }

        if (menuObject.name == "GameGUI")
        {
            bgm.volume = 0.083f;
            bgm.Stop();
            bgm.clip = ingameMusic;
            bgm.Play();
            isPlaying = true;
        }
        else if (isPlaying)
        {
            isPlaying = false;
            bgm.volume = 0f;
            StartCoroutine(StartVolume());
            bgm.Stop();
            bgm.clip = music;
            bgm.Play();
        }
    }

    public void NextPage()
    {
        if (currentPage + 1 >= pages.Length)
        {
            ShowMenu(availableMenus[0]);
            currentPage = 0;
        }
        else
        {
            currentPage++;
            UpdatePages(currentPage);
        }
    }

    public void PreviousPage()
    {
        if (currentPage < pages.Length)
        {
            currentPage--;
            UpdatePages(currentPage);
        }
    }

    /// <summary>
    /// Play the click sound.
    /// </summary>
    public void ClickSound()
    {
        //0.37 - 1.18
        //ui.pitch = Random.Range(0.37f, 1.19f);
        ui.pitch = Mathf.Clamp(Time.time - (int) Time.time + 0.15f, 0.37f, 1.35f);
        ui.PlayOneShot(click);
    }

    /// <summary>
    /// Used inside Unity to update "UI" volume.
    /// </summary>
    /// <param name="value"></param>
    public void UpdateUIVolume(float value)
    {
        mixer.SetFloat("uiVolume", value);
    }

    /// <summary>
    /// Used inside Unity to update "instruments" volume.
    /// </summary>
    /// <param name="value"></param>
    public void UpdateInstrumentsVolume(float value)
    {
        mixer.SetFloat("instrumentVolume", value);
    }

    /// <summary>
    /// Used inside Unity to update "music" volume.
    /// </summary>
    /// <param name="value"></param>
    public void UpdateMusicVolume(float value)
    {
        mixer.SetFloat("musicVolume", value);
    }

    public void Exit() => Application.Quit();

    private void UpdatePages(int page)
    {
        foreach (GameObject obj in pages)
        {
            obj.SetActive(false);
        }

        pages[page].SetActive(true);
        previousButton.SetActive(currentPage != 0);
    }
}