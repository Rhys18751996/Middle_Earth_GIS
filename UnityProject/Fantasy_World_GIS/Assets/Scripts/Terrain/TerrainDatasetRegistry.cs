using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace Fantasy_World_GIS.Terrain
{
    public class TerrainDatasetRegistry
    {
        private readonly List<TerrainDataset> datasets = new();

        public IReadOnlyList<TerrainDataset> Datasets =>
            datasets;

        public void Register(
            TerrainDataset dataset)
        {
            datasets.Add(
                dataset);
        }

        public void LoadDatasets()
        {
            datasets.Clear();

            string terrainRoot =
                Path.Combine(
                    Application.dataPath,
                    "Data",
                    "Terrain");

            if (!Directory.Exists(terrainRoot))
            {
                Debug.LogWarning(
                    $"Terrain folder not found: {terrainRoot}");

                return;
            }

            string[] datasetFolders =
                Directory.GetDirectories(
                    terrainRoot);

            foreach (string datasetFolder in datasetFolders)
            {
                string manifestPath =
                    Path.Combine(
                        datasetFolder,
                        "manifest.json");

                if (!File.Exists(manifestPath))
                {
                    Debug.LogWarning(
                        $"Manifest not found: {manifestPath}");

                    continue;
                }

                string json =
                    File.ReadAllText(
                        manifestPath);

                TerrainDatasetManifest manifest =
                    JsonUtility.FromJson<TerrainDatasetManifest>(
                        json);

                TerrainDataset dataset =
                    new TerrainDataset
                    {
                        Manifest = manifest,
                        FolderPath = datasetFolder
                    };

                datasets.Add(
                    dataset);

                Debug.Log(
                    $"Registered Dataset: {manifest.DatasetId}");
            }

            Debug.Log(
                $"Loaded {datasets.Count} terrain datasets");
        }
    }
}