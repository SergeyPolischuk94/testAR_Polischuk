using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class NativeToolController : MonoBehaviour
{
    public Texture2D CameraShotTexture
    {
        get;
        set;
    }

    public void CameraShot()
    {
        StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();
        string album = "TestAR";
        string name = "image" + DateTime.Now.Millisecond % 1000 + ".jpg";
        NativeGallery.SaveImageToGallery(texture, album, name);
        CameraShotTexture = texture;
    }

    public void OnShareBtnClick()
    {
        StartCoroutine(Share());
    }

    private IEnumerator Share()
    {
        yield return new WaitForEndOfFrame();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared1 img.png");
        File.WriteAllBytes(filePath, CameraShotTexture.EncodeToPNG());

        new NativeShare().AddFile(filePath).SetSubject("testAR").SetText("Look at my woolf!").Share();
    }

    public void Support()
    {
        string email = "vb@marevo.vision";
        string subject = "Test My APP";
        string body = "";
        OpenLink("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    private void OpenLink(string link)
    {
        bool googleSearch = link.Contains("google.com/search");
        string newLink = link.Replace(" ", googleSearch ? "+" : "%20");
        Application.OpenURL(link);
    }
}
