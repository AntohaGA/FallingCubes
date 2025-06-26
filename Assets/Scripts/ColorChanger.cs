using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    const string ColorProperty = "_Color";
    const int FadeTime = 3;

    private Color _defaultCubeColor = Color.red;
    private Color _defaultBombColor = Color.black;

    public void SetRandomColor(Figure instance)
    {
        instance.Renderer.material.SetColor(ColorProperty, Random.ColorHSV());
    }

    public void SetDefaultColor(Figure instance)
    {
        if (instance is Cube)
        {
            instance.Renderer.material.SetColor(ColorProperty, _defaultCubeColor);
        }
        else if (instance is Bomb)
        {
            instance.Renderer.material.SetColor(ColorProperty, _defaultBombColor);
        }
    }

    public void Fade(Figure figure)
    {
        StartCoroutine(MakeTransparent(figure, FadeTime));
    }

    public IEnumerator MakeTransparent(Figure figure, float timeDuration)
    {
        Renderer renderer = figure.GetComponent<Renderer>();
        Material material = renderer.material;
        Color color = material.color;

        float time = 0;
        float targetTime = 1;
        float startAlpha = color.a;

        while (time < targetTime)
        {
            time += Time.deltaTime / timeDuration;
            color.a = Mathf.Lerp(startAlpha, 0, time);
            material.color = color;
            yield return null;
        }
    }
}