using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class ShareScreenshot : MonoBehaviour {

    private string filePath;

    public void CaptureAndShareScreenshot()
	{
        filePath = Application.persistentDataPath + "/temp.jpg";
        CaptureScreenshot();
        ShareScreenshotImage ();
    }

	private void CaptureScreenshot()
	{
		// Salvar a tela dentro de uma texture
		Texture2D tex = new Texture2D (Screen.width, Screen.height);
		tex.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();
        // Salva a texture em um arquivo JPG
        byte[] bytes = tex.EncodeToJPG();
        Object.Destroy(tex);


        if (File.Exists (filePath)) {
			File.Delete (filePath);
		}
		File.WriteAllBytes (filePath, bytes); // salva a imagem

	}


    private void ShareScreenshotImage()
	{
		AndroidJavaClass intentClass 
			= new AndroidJavaClass ("android.content.Intent");

		AndroidJavaObject intentObject 
			= new AndroidJavaObject ("android.content.Intent");

		intentObject.Call<AndroidJavaObject> ("setAction",
			intentClass.GetStatic<string>("ACTION_SEND"));
		intentObject.Call<AndroidJavaObject>("setType", "image/*");

		AndroidJavaClass uriClass 
			= new AndroidJavaClass ("android.net.Uri");
		AndroidJavaObject fileObject 
			= new AndroidJavaObject ("java.io.File", filePath);

		AndroidJavaObject uriObject  
			= uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);
		intentObject.Call<AndroidJavaObject> ("putExtra", 
			intentClass.GetStatic<string>("EXTRA_STREAM"),
			uriObject);

		AndroidJavaObject curActivity = GetCurrentActivity ();
		curActivity.Call ("startActivity", intentObject);
	}

    private void NativeShareScreenshot()
	{
		AndroidJavaClass jc 
			= new AndroidJavaClass (
				"br.edu.posgames.flappybird.NativeScreenshot"
		);

		jc.CallStatic ("ShareScreenshot", GetCurrentActivity());
	}

	private AndroidJavaObject GetCurrentActivity()
	{
		AndroidJavaClass jc 
			= new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		return jc.GetStatic<AndroidJavaObject> ("currentActivity");
	}

	private string GetAndroidExternalStoragePath()
	{
		string path = " ";

		AndroidJavaClass jc 
			= new AndroidJavaClass ("android.os.Environment");

		path =
		jc.CallStatic<AndroidJavaObject> ("getExternalStorageDirectory")
			.Call<string> ("getAbsolutePath");

		return path;
	}

}
