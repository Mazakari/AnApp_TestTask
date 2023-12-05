using System.Collections;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] private CanvasGroup _curtain;
    private float _fadeInStep = 0.03f;

    private void Awake() => 
        DontDestroyOnLoad(this);

    public void Show()
    {
        gameObject.SetActive(true);
        _curtain.alpha = 1f;
    }

    public void Hide() => 
        StartCoroutine(FadeIn());

    private IEnumerator FadeIn()
    {
        while (_curtain.alpha > 0)
        {
            _curtain.alpha -= _fadeInStep;
            yield return new WaitForSeconds(_fadeInStep);
        }

        gameObject.SetActive(false);
    }
}