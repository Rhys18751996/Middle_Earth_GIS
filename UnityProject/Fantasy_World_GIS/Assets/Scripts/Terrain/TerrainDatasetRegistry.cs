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
        private readonly List<TerrainDataset> datasets = new();

        public IReadOnlyList<TerrainDataset> Datasets => datasets;

        public void Register(TerrainDataset dataset)
        {
            if (dataset == null)
            {
                return;
            }

            datasets.Add(dataset);
        }

        /// <summary>
        /// Discovers and loads all terrain datasets from the terrain data folder.
        ///
        /// Each dataset folder must contain a manifest.json file describing
        /// the dataset. During loading, a chunk registry is built by scanning
        /// the dataset folder and recording all available chunk coordinates
        /// in memory.
        ///
        /// Datasets are sorted by priority so higher-detail datasets are
        /// evaluated first during chunk resolution.
        /// </summary>
        public void LoadDatasets()
        {
            // Remove any previously loaded datasets.
            datasets.Clear();

            string terrainRoot = Path.Combine(Application.dataPath, "Data", "Terrain");

            if (!Directory.Exists(terrainRoot))
            {
                Debug.LogWarning($"Terrain folder not found: {terrainRoot}");
                return;
            }

            // Each subfolder represents a terrain dataset.
            string[] datasetFolders = Directory.GetDirectories(terrainRoot);

            Debug.Log($"Found {datasetFolders.Length} dataset folders");

            foreach (string folder in datasetFolders)
            {
                Debug.Log($"Dataset Folder: {folder}");
            }

            foreach (string datasetFolder in datasetFolders)
            {
                string manifestPath =Path.Combine(datasetFolder, "manifest.json");

                // Skip folders that do not contain a manifest.
                if (!File.Exists(manifestPath))
                {
                    Debug.LogWarning($"Manifest not found: {manifestPath}");
                    continue;
                }

                try
                {
                    string json = File.ReadAllText(manifestPath);

                    TerrainDatasetManifest manifest = JsonUtility.FromJson<TerrainDatasetManifest>(json);

                    if (manifest == null)
                    {
                        Debug.LogWarning($"Failed to parse manifest: {manifestPath}");
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(manifest.DatasetId))
                    {
                        Debug.LogWarning($"DatasetId missing in: {manifestPath}");

                        continue;
                    }

                    TerrainDataset dataset = new TerrainDataset
                        {
                            Manifest = manifest,
                            FolderPath = datasetFolder
                        };

                    // Build an in-memory registry of available chunks.
                    // This avoids expensive filesystem lookups during
                    // runtime chunk resolution.
                    BuildChunkRegistry(dataset);

                    datasets.Add(dataset);

                    Debug.Log($"Registered Dataset: {manifest.DatasetId} " + $"({manifest.ResolutionMeters}m)");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Failed to load dataset manifest '{manifestPath}'\n{ex}");
                }
            }

            // Higher-priority datasets are evaluated first by the resolver.
            datasets.Sort((a, b) => b.Priority.CompareTo(a.Priority));

            Debug.Log($"Loaded {datasets.Count} terrain datasets");
        }

        private static void BuildChunkRegistry(TerrainDataset dataset)
        {
            foreach (string filePath in Directory.GetFiles(dataset.FolderPath, "Chunk_*.json"))
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);

                if (!ChunkFileNaming.TryParseCoordinates(fileName, out int x, out int y))
                {
                    continue;
                }

                dataset.AvailableChunks.Add((x, y));
            }

            Debug.Log($"{dataset.DatasetId}: {dataset.AvailableChunks.Count} chunks registered");
        }
    }
}