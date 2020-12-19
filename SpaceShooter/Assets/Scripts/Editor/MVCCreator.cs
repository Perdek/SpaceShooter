using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class MVCCreator : EditorWindow {

	#region MEMBERS

	private const string MVC_NAME = "MVC name: ";

	private const string BASE_MENU_TAB_GUI_PREFIX = "BaseMenuTabGUI";
	private const string BASE_POPUP_PREFIX = "PopUp";
	private const string CONTROLLER = "Controller";
	private const string MODEL = "Model";
	private const string VIEW = "View";

	private const int TEMPLATE_FORMAT_INDEX = 3;
	private const int TEMPLATE_MODEL_FORMAT_INDEX = 8;
	private const int TEMPLATE_VIEW_FORMAT_INDEX = 10;

	private static readonly string[] MVC_DEFAULT_TEMPLATE =
	{
		"using System.Collections;",
		"using System.Collections.Generic;",
		"using UnityEngine;\n",
		"public class {0}{2} : {1}{2}",
		"{",
		"\t#region MEMBERS\n",
		"\t#endregion\n",
		"\t#region PROPERTIES\n",
		"\t#endregion\n",
		"\t#region METHODS\n",
		"\t#endregion\n",
		"\t#region CLASS_ENUMS\n ",
		"\t#endregion",
		"}"
	};

	private static readonly string[] MVC_CONTROLLER_TEMPLATE =
	{
		"using System.Collections;",
		"using System.Collections.Generic;",
		"using UnityEngine;\n",
		"[RequireComponent(typeof({0}Model), typeof({0}View))]\npublic class {0}{2} : {1}{2}",
		"{",
		"\t#region MEMBERS\n",
		"\t#endregion\n",
		"\t#region PROPERTIES\n",
		"\tprivate {0}Model Model {1}\n\t\tget => GetModel<{0}Model>();",
		"\t}\n",
		"\tprivate {0}View View {1}\n\t\tget => GetView<{0}View>();",
		"\t}\n",
		"\t#endregion\n",
		"\t#region METHODS\n",
		"\t#endregion\n",
		"\t#region CLASS_ENUMS\n ",
		"\t#endregion",
		"}"
	};

	#endregion
	
	#region PROPERTIES
	
	private string MVCName {
		get;
		set;
	}

	private static MVCCreator Creator {
		get;
		set;
	}

	private static string Path {
		get;
		set;
	}

	#endregion

	#region METHODS

	[MenuItem("Assets/Create/MVC/Create C# MVC Scripts", false, 200)]
	public static void OpenMVCCreatorWindow ()
	{
		if (Creator != null)
		{
			Creator.Close();
		}

		Creator = new MVCCreator();
		Creator.position = new Rect(Vector2.zero, Vector2.zero);
		Creator.Show();

		DesignateCreationPath();
	}

	private static void DesignateCreationPath()
	{
		string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
		
		if(string.IsNullOrEmpty(assetPath) == true)
		{
			return;
		}

		Path = System.IO.Path.GetFullPath(assetPath);
	}

	private void OnGUI ()
	{
		DesignateCreationPath();

		EditorGUILayout.LabelField(string.Format("Current Creation Path: {0}", Path));
		MVCName = EditorGUILayout.TextField(MVC_NAME, MVCName);

		if (GUILayout.Button("Create simple MVC") == true)
		{
			CreateMVCScripts(MVCName);
		}

		if (GUILayout.Button("Create Menu Tab GUI MVC") == true)
		{
			CreateMVCScripts(MVCName, BASE_MENU_TAB_GUI_PREFIX);
		}

		if (GUILayout.Button("Create Pop Up MVC") == true)
		{
			CreateMVCScripts(MVCName + BASE_POPUP_PREFIX, BASE_POPUP_PREFIX);
		}

		if (GUILayout.Button("Close") == true)
		{
			Close();
		}
	}

	private void CreateMVCScripts (string mvcName, string mvcPrefix = "")
	{
		CreateControllerScript(mvcName, mvcPrefix);
		CreateModelScript(mvcName, mvcPrefix);
		CreateViewScript(mvcName, mvcPrefix);

		AssetDatabase.Refresh();
	}

	private void CreateControllerScript (string mvcName, string mvcPrefix)
	{
		CreateScriptFile(MVC_CONTROLLER_TEMPLATE,mvcName, mvcPrefix, CONTROLLER, true);
	}

	private void CreateModelScript (string mvcName, string mvcPrefix)
	{
		CreateScriptFile(MVC_DEFAULT_TEMPLATE, mvcName, mvcPrefix, MODEL);
	}

	private void CreateViewScript (string mvcName, string mvcPrefix)
	{
		CreateScriptFile(MVC_DEFAULT_TEMPLATE, mvcName, mvcPrefix, VIEW);
	}

	private void CreateScriptFile(string[] scriptTemplate , string mvcName, string mvcPrefix, string mvcTypeName, bool useVieModelTemplate = false)
	{
		if (string.IsNullOrEmpty(Path) == true || string.IsNullOrEmpty(mvcName) == true)
		{
			return;
		}

		string fileName = string.Format("{0}{1}.cs", mvcName, mvcTypeName);
		string filePath = string.Format("{0}/{1}", Path, fileName);

		using (FileStream newScriptFileStream = File.Create(filePath))
		{
			using (StreamWriter writer = new StreamWriter(newScriptFileStream))
			{
				for (int i = 0; i < scriptTemplate.Length; i++)
				{
					if (i == TEMPLATE_FORMAT_INDEX)
					{
						writer.WriteLine(string.Format(scriptTemplate[i], mvcName, mvcPrefix, mvcTypeName));
					}
					else if(useVieModelTemplate == true && (i == TEMPLATE_MODEL_FORMAT_INDEX || i == TEMPLATE_VIEW_FORMAT_INDEX))
					{
						string whaaa = scriptTemplate[i];
						writer.WriteLine(string.Format(scriptTemplate[i], mvcName, '{'));
					}
					else
					{
						writer.WriteLine(scriptTemplate[i]);
					}
				}
			}
		}
	}
	
	#endregion
	
	#region CLASS_ENUMS 
	
	#endregion
}
