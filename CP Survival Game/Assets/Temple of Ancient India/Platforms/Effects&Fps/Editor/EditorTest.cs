using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class NewEditorTest {

	[Test]
	public void EditorTest()
	{
		string[] projectContent = new string[] {"Assets/Temple of Ancient India", "ProjectSettings/QualitySettings.asset"};  
		            AssetDatabase.ExportPackage(projectContent, "UltimateTemplate.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);  
		           Debug.Log("Project Exported");
	}
}
