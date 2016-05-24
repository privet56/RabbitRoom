using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
//using System.Runtime.InteropServices;
//using UnityEditor;		//->EditorUtility: only valid within the editor
//using System.Linq;
using UnityEngine;
using System.Collections;

public class screenshotbutton : MonoBehaviour
{
	public GameObject[] canvasesToDeactivate = new GameObject[0];

	//[DllImport("user32.dll")]
	//private static extern void SaveFileDialog(); //or: OpenFileDialog

	void Start ()
	{
		{
			Animator myAnimator = this.GetComponent<Animator>();
			if(myAnimator)
			{
				#if !MOBILE_INPUT
				myAnimator.applyRootMotion = false;
				#else
				myAnimator.applyRootMotion = true;
				#endif
			}
		}
	}
	
	void Update ()
	{
	
	}

	public void onButtonClick()
	{
		//Application.OpenURL("http://

		//TODO: if(mobile) -> send email instead of saving locally?
		StartCoroutine(startFileSelector());
	}

	void GotFile(FileSelector.Status status, string path)
	{
		if(status != FileSelector.Status.Successful)
			return;

		bool[] canvasesIsDeactivated = new bool[canvasesToDeactivate.Length];
		for(int i=0;i<canvasesToDeactivate.Length;i++)
		{
			canvasesIsDeactivated[i] = canvasesToDeactivate[i].activeSelf;
			canvasesToDeactivate[i].SetActive(false);
		}

		Application.CaptureScreenshot(path);

		{	//workaround for Coroutine couldn't be started because the the game object is inactive!
			UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController rigidbodyFirstPersonController = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
			rigidbodyFirstPersonController.StartCoroutine(rigidbodyFirstPersonController.ActivateCanvases(canvasesToDeactivate, canvasesIsDeactivated, "Screenshot is saved"));
		}
	}

	IEnumerator startFileSelector()
	{
		yield return new WaitForSeconds(0.1f);
		FileSelector.GetFile(GotFile, ".png");
	}

}
