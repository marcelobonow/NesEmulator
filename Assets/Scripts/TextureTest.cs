using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TextureTest : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    private Texture2D texture;
    private RawImage rawImage;

    private const int WIDTH = 640;
    private const int HEIGHT = 480;

    private int width;
    private int height;

    private Color32[] colors;

    private Thread emulatorThread;

    private int frames;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();

        texture = new Texture2D(WIDTH, HEIGHT);
        rawImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        rawImage.texture = texture;

        width = texture.width;
        height = texture.height;
        colors = new Color32[width * height];

        emulatorThread = new Thread(() =>
        {
            Emulate();
        });
        emulatorThread.Start();


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
        System.Random rng = new System.Random();
        for (int i = 0; i < width * height; i++)
        {
            colors[i] = new Color32((byte)(rng.Next() * 255), (byte)(rng.Next() * 255), (byte)(rng.Next() * 255), 255);
        }
        while (true)
        {
            for (int i = 0; i < width * height; i++)
            {
                colors[rng.Next() % colors.Length] = new Color32((byte)(rng.Next() * 255), (byte)(rng.Next() * 255), (byte)(rng.Next() * 255), 255);
            }
        }
    }

    private void Update()
    {
        SetPixels(colors);
    }

}
