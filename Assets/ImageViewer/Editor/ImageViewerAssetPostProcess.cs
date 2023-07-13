using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class ImageViewerAssetPostProcess : AssetPostprocessor {
	void OnPreprocessTexture() {
		if(assetPath.Contains("/Images/")) {
			TextureImporter importer = assetImporter as TextureImporter;
			importer.textureType = TextureImporterType.GUI;
		}
	}
}
