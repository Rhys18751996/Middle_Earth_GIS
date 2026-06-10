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
    public class GisCoreIntegrationTest
        : MonoBehaviour
    {
        private void Start()
        {
            SettlementFeature hobbiton =
                new()
                {
                    FeatureId = "HOBBITON",
                    FeatureType = FeatureType.Settlement,
                    Geometry =
                        new PointGeometry
                        {
                            Coordinate =
                                new WorldCoordinate(
                                    100,
                                    100)
                        }
                };

            hobbiton.Attributes.Add(
                new GisAttribute
                {
                    Name = "Population",
                    Value = "1200"
                });

            hobbiton.Attributes.Add(
                new GisAttribute
                {
                    Name = "Owner",
                    Value = "Shire"
                });

            SettlementFeature bywater =
                new()
                {
                    FeatureId = "BYWATER",
                    FeatureType = FeatureType.Settlement,
                    Geometry =
                        new PointGeometry
                        {
                            Coordinate =
                                new WorldCoordinate(
                                    400,
                                    300)
                        }
                };

            SettlementFeature michelDelving =
                new()
                {
                    FeatureId = "MICHEL_DELVING",
                    FeatureType = FeatureType.Settlement,
                    Geometry =
                        new PointGeometry
                        {
                            Coordinate =
                                new WorldCoordinate(
                                    800,
                                    900)
                        }
                };

            SettlementFeature bree =
                new()
                {
                    FeatureId = "BREE",
                    FeatureType = FeatureType.Settlement,
                    Geometry =
                        new PointGeometry
                        {
                            Coordinate =
                                new WorldCoordinate(
                                    1500,
                                    1200)
                        }
                };

            SettlementDataset dataset =
                new()
                {
                    DatasetId = "SETTLEMENTS",
                    Name = "Middle-earth Settlements"
                };

            dataset.Features.Add(hobbiton);
            dataset.Features.Add(bywater);
            dataset.Features.Add(michelDelving);
            dataset.Features.Add(bree);

            DatasetRegistry registry =
                new();

            registry.Register(dataset);

            FeatureIdValidator validator =
                new();

            foreach (GisFeature feature in dataset.Features)
            {
                foreach (ValidationResult result in validator.Validate(feature))
                {
                    Debug.Log(
                        $"{result.Severity}: {result.Message}");
                }
            }

            GisBounds shireBounds =
                new(
                    0,
                    0,
                    1000,
                    1000);

            var settlementsInBounds =
                DatasetQueryService
                    .GetFeaturesInBounds(
                        dataset,
                        shireBounds)
                    .ToList();

            Debug.Log(
                "=== Settlements In Bounds ===");

            foreach (GisFeature settlement in settlementsInBounds)
            {
                Debug.Log(
                    settlement.FeatureId);
            }

            WorldCoordinate searchPoint =
                new(
                    100,
                    100);

            var nearbySettlements =
                DatasetQueryService
                    .GetFeaturesNearPoint(
                        dataset,
                        searchPoint,
                        500)
                    .ToList();

            Debug.Log(
                "=== Nearby Settlements ===");

            foreach (GisFeature settlement in nearbySettlements)
            {
                Debug.Log(
                    settlement.FeatureId);
            }

            bool found =
                registry.TryGetDataset(
                    "SETTLEMENTS",
                    out GisDataset registeredDataset);

            Debug.Log(
                "=== GIS Core Integration Test ===");

            Debug.Log(
                $"Dataset Registered: {found}");

            Debug.Log(
                $"Dataset Features: {dataset.Features.Count}");

            Debug.Log(
                $"Bounds Query Results: {settlementsInBounds.Count}");

            Debug.Log(
                $"Near Point Query Results: {nearbySettlements.Count}");
        }
    }
}