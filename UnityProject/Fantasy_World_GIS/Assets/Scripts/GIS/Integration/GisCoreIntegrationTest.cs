using System.Linq;

using Fantasy_World_GIS.GIS.Coordinates;
using Fantasy_World_GIS.GIS.Core;
using Fantasy_World_GIS.GIS.Geometry;
using Fantasy_World_GIS.GIS.Queries;
using Fantasy_World_GIS.GIS.Registries;
using Fantasy_World_GIS.GIS.Validation;

using UnityEngine;

namespace Fantasy_World_GIS.GIS.Integration
{
    public class GisCoreIntegrationTest : MonoBehaviour
    {
        private void Start()
        {
            SettlementFeature settlement = new();

            settlement.FeatureId = "HOBBITON";
            settlement.FeatureType = FeatureType.Settlement;

            settlement.Geometry = new PointGeometry{ Coordinate = new WorldCoordinate(1200, 800) };

            settlement.Attributes.Add(
                new GisAttribute
                {
                    Name = "Population",
                    Value = "1200"
                });

            settlement.Attributes.Add(
                new GisAttribute
                {
                    Name = "Owner",
                    Value = "Shire"
                });

            SettlementDataset dataset =
                new()
                {
                    DatasetId = "SHIRE_SETTLEMENTS",
                    Name = "Shire Settlements"
                };

            dataset.Features.Add(settlement);

            DatasetRegistry registry = new();

            registry.Register(dataset);

                var results =
                DatasetQueryService
                    .GetFeaturesByType(
                        dataset,
                        FeatureType.Settlement);

            FeatureIdValidator validator = new();

            foreach (var feature in results)
            {
                foreach (var validationResult in validator.Validate(feature))
                {
                    Debug.Log(validationResult.Message);
                }
            }

            bool found = registry.TryGetDataset("SHIRE_SETTLEMENTS", out GisDataset registeredDataset);

            Debug.Log($"Dataset Registered: {found}");
            Debug.Log("=== GIS Core Integration Test ===");
            Debug.Log($"Created Feature: {settlement.FeatureId}");
            Debug.Log($"Dataset Features: {dataset.Features.Count}");
            Debug.Log($"Query Results: {results.Count()}");
        }
    }
}