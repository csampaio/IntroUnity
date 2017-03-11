using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ShareScreenShot : MonoBehaviour {

	public void CaptureAndShareScreenshot()
    {
        CaptureScreenShot();
        //NativeShareScreenshot();
        ShareScreenshotImage();
    }

    public void CaptureScreenShot()
    {
        Texture2D tex = new Texture2D(Screen.width, Screen.height);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        byte[] bytes = tex.EncodeToJPG();
        string filepath = GetAndroidExternalStoragePath() + "/temporary_file.jpg";

        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }
        File.WriteAllBytes(filepath, bytes);

    }

    void NativeShareScreenshot()
    {
        AndroidJavaClass jc = new AndroidJavaClass("br.edu.posgames.flappybird.NativeScreenshot");
        jc.CallStatic("ShareScreenshot", GetCurrentActivity());
    }

    public AndroidJavaObject GetCurrentActivity()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnitPlayer");
        return jc.GetStatic<AndroidJavaObject>("currentActivity");
    }

    public string GetAndroidExternalStoragePath()
    {
        string path = "";
        AndroidJavaClass jc = new AndroidJavaClass("android.os.Environment");
        jc.CallStatic<AndroidJavaObject>("getExternalStorageDirectory")
            .Call<string>("getAbsolutePath");
        return path;
    }

    public void ShareScreenshotImage()
    {
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

        intentObject.Call<AndroidJavaObject>("setAction",
            intentClass.GetStatic<string>("SEND_ACTION"));

        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", 
            GetAndroidExternalStoragePath() + "/temporary_file.jpg");
        AndroidJavaObject uriObject = 
           uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);

        intentObject.Call<AndroidJavaObject>("putExtra", 
            intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

        AndroidJavaObject curActivity = GetCurrentActivity();
        curActivity.Call("startActivity", intentObject);
    }
}
