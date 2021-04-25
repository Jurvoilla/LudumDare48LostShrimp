using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    private float time = 0.3f;

    public void Start()
    {
        StartCoroutine(FadeIn());
    }
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {

        img.gameObject.SetActive(true);
        float t = time;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, t/time);
            yield return 0;
        }
        img.gameObject.SetActive(false);
    }

    IEnumerator FadeOut(string scene)
    {
        img.gameObject.SetActive(true);
        float t = 0f;

        while (t < time)
        {
            t += Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, t/time);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
