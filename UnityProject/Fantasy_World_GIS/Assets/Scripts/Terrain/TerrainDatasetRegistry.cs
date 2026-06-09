using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fantasy_World_GIS.Terrain
{
    /// <summary>
    /// Serializable description of a terrain dataset.
    ///
    /// The manifest defines dataset-wide properties such as
    /// resolution, priority, coverage bounds, height format
    /// and elevation limits.
    ///
    /// Manifests are loaded at startup and used to construct
    /// TerrainDataset instances.
    /// </summary>
    public class TerrainDatasetRegistry
    {
        private readonly List<TerrainDataset> datasets =
            new();

        public IReadOnlyList<TerrainDataset> Datasets =>
            datasets;

        public void Register(
            TerrainDataset dataset)
        {
            if (dataset == null)
            {
                return;
            }

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

            if (!Directory.Exists(
                    terrainRoot))
            {
                Debug.LogWarning(
                    $"Terrain folder not found: {terrainRoot}");

                return;
            }

            string[] datasetFolders =
                Directory.GetDirectories(
                    terrainRoot);

            // for debugging, log the found dataset folders
            Debug.Log($"Found {datasetFolders.Length} dataset folders");

            foreach (string folder in datasetFolders)
            {
                Debug.Log(
                    $"Dataset Folder: {folder}");
            }

            foreach (string datasetFolder in datasetFolders)
            {
                string manifestPath =
                    Path.Combine(
                        datasetFolder,
                        "manifest.json");

                if (!File.Exists(
                        manifestPath))
                {
                    Debug.LogWarning(
                        $"Manifest not found: {manifestPath}");

                    continue;
                }

                try
                {
                    string json =
                        File.ReadAllText(
                            manifestPath);

                    TerrainDatasetManifest manifest =
                        JsonUtility.FromJson<TerrainDatasetManifest>(
                            json);

                    if (manifest == null)
                    {
                        Debug.LogWarning(
                            $"Failed to parse manifest: {manifestPath}");

                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(
                            manifest.DatasetId))
                    {
                        Debug.LogWarning(
                            $"DatasetId missing in: {manifestPath}");

                        continue;
                    }

                    TerrainDataset dataset = new TerrainDataset{Manifest = manifest, FolderPath = datasetFolder};
                    BuildChunkRegistry(dataset);
                    datasets.Add(dataset);

                    Debug.Log($"Registered Dataset: {manifest.DatasetId} " + $"({manifest.ResolutionMeters}m)");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(
                        $"Failed to load dataset manifest '{manifestPath}'\n{ex}");
                }
            }

            datasets.Sort(
                (a, b) => b.Priority.CompareTo(a.Priority));

            Debug.Log(
                $"Loaded {datasets.Count} terrain datasets");
        }

        private static void BuildChunkRegistry(TerrainDataset dataset)
        {
            foreach (string filePath in Directory.GetFiles(
                        dataset.FolderPath,
                        "Chunk_*.json"))
            {
                string fileName =
                    Path.GetFileNameWithoutExtension(
                        filePath);

                if (!ChunkFileNaming.TryParseCoordinates(
                        fileName,
                        out int x,
                        out int y))
                {
                    continue;
                }

                dataset.AvailableChunks.Add(
                    (x, y));
            }

            Debug.Log(
                $"{dataset.DatasetId}: {dataset.AvailableChunks.Count} chunks registered");
        }
    }
}