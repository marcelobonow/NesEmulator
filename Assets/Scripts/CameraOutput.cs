using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CameraOutput : MonoBehaviour
{

    private Camera mainCamera;
    private Texture2D texture;
    private RawImage rawImage;

    private const int WIDTH = 640;
    private const int HEIGHT = 480;

    private Color32[] buffer;

    private short frames;

    private void Start()
    {
        mainCamera = Camera.main;

        texture = new Texture2D(WIDTH, HEIGHT);
        rawImage = gameObject.AddComponent<RawImage>();
        rawImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        gameObject.GetComponent<RectTransform>().rect.position.Set(0, 0);
        rawImage.texture = texture;

        buffer = new Color32[WIDTH * HEIGHT];

    }

    public void SetPixels(Color32[] colors)
    {
        texture.SetPixels32(colors);
        texture.Apply();
    }


    private Color UIntToColor(uint color)
    {
        byte r = (byte)(color >> 16);
        byte g = (byte)(color >> 8);
        byte b = (byte)(color >> 0);
        return new Color(r / 255f, g / 255f, b / 255f, 255f);
    }

    private void Emulate()
    {

        for (int i = 0; i < WIDTH * HEIGHT; i++)
        {
            //colors[i] = new Color32((byte)(rng.Next() * 255), (byte)(rng.Next() * 255), (byte)(rng.Next() * 255), 255);
            buffer[i] = new Color32((byte)((i + frames + 10f)), (byte)((i + frames + 40f)), (byte)((i + 100f)), 255);
        }
        frames++;
    }

    private void Update()
    {
        Emulate();
        SetPixels(buffer);
    }
}
