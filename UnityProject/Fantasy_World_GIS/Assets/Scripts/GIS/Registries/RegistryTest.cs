using UnityEngine;
using Fantasy_World_GIS.GIS.Core;
using Fantasy_World_GIS.GIS.Queries;
using System;

namespace Fantasy_World_GIS.GIS.Registries
{
    public class RegistryTest : MonoBehaviour
    {
        private void Start()
        {
            TestDatasetRegistry();
            TestAssetRegistry();
            TestDuplicateRegistration();
            TestAttributes();
        }

        private void TestDatasetRegistry()
        {
            Debug.Log(
                "=== Dataset Registry Test ===");

            DatasetRegistry registry =
                new();

            RiverDataset dataset =
                new()
                {
                    DatasetId =
                        "RIVERS_TEST",

                    Name =
                        "Test Rivers"
                };

            registry.Register(
                dataset);

            bool found =
                registry.TryGetDataset(
                    "RIVERS_TEST",
                    out GisDataset result);

            Debug.Log(
                $"Found Dataset: {found}");

            if (found)
            {
                Debug.Log(
                    $"Dataset Name: {result.Name}");
            }
        }

        private void TestAssetRegistry()
        {
            Debug.Log(
                "=== Asset Registry Test ===");

            AssetRegistry registry =
                new();

            GisAssetDefinition asset =
                new()
                {
                    AssetId =
                        "TREE_OAK_SMALL",

                    Name =
                        "Small Oak Tree",

                    Category =
                        "Vegetation"
                };

            registry.Register(
                asset);

            bool found =
                registry.TryGetAsset(
                    "TREE_OAK_SMALL",
                    out GisAssetDefinition result);

            Debug.Log(
                $"Found Asset: {found}");

            if (found)
            {
                Debug.Log(
                    $"Asset Name: {result.Name}");

                Debug.Log(
                    $"Category: {result.Category}");
            }

            bool missingFound =
                registry.TryGetAsset(
                    "DOES_NOT_EXIST",
                    out _);

            Debug.Log(
                $"Missing Asset Found: {missingFound}");
        }

        private void TestDuplicateRegistration()
        {
            Debug.Log("=== Duplicate Registration Test ===");

            AssetRegistry registry = new();

            GisAssetDefinition asset = new()
            {
                AssetId = "TREE_OAK_SMALL",
                Name = "Small Oak Tree",
                Category = "Vegetation"
            };

            registry.Register(asset);

            try
            {
                registry.Register(asset);

                Debug.LogError("Duplicate registration should have failed.");
            }
            catch (System.InvalidOperationException)
            {
                Debug.Log("Duplicate registration correctly rejected.");
            }
            catch (Exception ex)
            {
                Debug.Log($"{ex}");
            }
        }

        private void TestAttributes()
        {
            Debug.Log("=== Attribute Framework Test ===");

            SettlementFeature settlement = new();

            settlement.FeatureId = "HOBBITON";

            settlement.Attributes.Add(
                new GisAttribute
                {
                    Name = "Population",

                    Value = "1200"
                });

            settlement.Attributes.Add(
                new GisAttribute
                {
                    Name =
                        "Owner",

                    Value =
                        "Shire"
                });

            Debug.Log(
                $"Feature: {settlement.FeatureId}");

            foreach (GisAttribute attribute
                    in settlement.Attributes)
            {
                Debug.Log(
                    $"{attribute.Name} = " +
                    $"{attribute.Value}");
            }

            Debug.Log(
                $"Attribute Count: " +
                $"{settlement.Attributes.Count}");
        }
    
        private void TestQueries()
        {
            Debug.Log(
                "=== Query Test ===");

            SettlementFeature settlement =
                new();

            settlement.FeatureId =
                "HOBBITON";

            settlement.FeatureType =
                FeatureType.Settlement;

            RiverDataset dataset =
                new()
                {
                    DatasetId =
                        "TEST"
                };

            dataset.Features.Add(
                settlement);

            var results =
                DatasetQueryService
                    .GetFeaturesByType(
                        dataset,
                        FeatureType.Settlement);

            foreach (var feature in results)
            {
                Debug.Log(
                    $"Found: {feature.FeatureId}");
            }
        }
    }
}