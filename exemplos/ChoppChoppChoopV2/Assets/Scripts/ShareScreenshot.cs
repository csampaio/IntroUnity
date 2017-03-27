using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class ShareScreenshot : MonoBehaviour {

	public void CaptureAndShareScreenshot()
	{
		CaptureScreenshot ();
		ShareScreenshotImage ();
	}

	public void CaptureScreenshot()
	{
		// Salvar a tela dentro de uma texture
		Texture2D tex = new Texture2D (Screen.width, Screen.height);
		tex.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0);
		// Salva a texture em um arquivo JPG
		byte[] bytes = tex.EncodeToJPG();

		string filepath = GetAndroidExternalStoragePath () + 
									"/temporary_file.jpg";

		if (File.Exists (filepath)) {
			File.Delete (filepath);
		}
		File.WriteAllBytes (filepath, bytes); // salva a imagem

	}

	public void ShareScreenshotImage()
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
			= new AndroidJavaObject ("java.io.File", 
				GetAndroidExternalStoragePath() + "/temporary_file.jpg");

		AndroidJavaObject uriObject  
			= uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);
		intentObject.Call<AndroidJavaObject> ("putExtra", 
			intentClass.GetStatic<string>("EXTRA_STREAM"),
			uriObject);

		AndroidJavaObject curActivity = GetCurrentActivity ();
		curActivity.Call ("startActivity", intentObject);
	}

	public void NativeShareScreenshot()
	{
		AndroidJavaClass jc 
			= new AndroidJavaClass (
				"br.edu.posgames.flappybird.NativeScreenshot"
		);

		jc.CallStatic ("ShareScreenshot", GetCurrentActivity());
	}

	public AndroidJavaObject GetCurrentActivity()
	{
		AndroidJavaClass jc 
			= new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		return jc.GetStatic<AndroidJavaObject> ("currentActivity");
	}

	public string GetAndroidExternalStoragePath()
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
