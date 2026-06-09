# TerrainRepresentation.md

## Purpose

Define the Phase 2 authoritative internal representation for terrain data.

Terrain data is stored as engine-independent GIS raster datasets. Unity runtime meshes, colliders, materials, and scene objects are consumers of this data and must never become the authoritative terrain source.

---

## Architectural Position

Phase 2 builds on the Phase 0 chunk grid and Phase 1 GIS dataset principles:

- **Chunk Grid**: fixed 256m × 256m indexing, streaming, caching, and lookup system.
- **Terrain Dataset**: an authoritative raster resolution layer such as `MiddleEarth_1m_Test`, `MiddleEarth_250m`, or `Shire_25m`.
- **Terrain Tile**: a raster storage unit inside a terrain dataset.
- **Unity Mesh**: a generated runtime visualization of one loaded terrain tile.

Chunks do not own terrain. Terrain tiles reference world-space bounds and can be discovered through the chunk grid.

---

## Terrain Dataset Manifest

Every terrain dataset has a manifest that describes the resolution layer as a whole.

Required fields:

```json
{
  "DatasetId": "MiddleEarth_1m_Test",
  "DatasetType": "Terrain",
  "CellSize": 1.0,
  "Priority": 1,
  "Version": 1,
  "CoverageBounds": {
    "MinX": 0.0,
    "MinY": 0.0,
    "MaxX": 2560.0,
    "MaxY": 2560.0
  },
  "TileSize": 257,
  "HeightFormat": "UInt16",
  "MinElevation": 0.0,
  "MaxElevation": 200.0,
  "Attributes": {
    "Source": "Procedural Perlin test terrain",
    "HistoricalPeriod": "Synthetic Test Data",
    "Author": "Middle_Earth_GIS",
    "Notes": "Generated test dataset manifest for Phase 2 terrain representation."
  }
}
```

### Manifest Fields

| Field | Meaning |
| --- | --- |
| `DatasetId` | Stable dataset identifier referenced by terrain tiles. |
| `DatasetType` | Must be `Terrain`. |
| `CellSize` | Horizontal spacing, in meters, between adjacent height samples. |
| `Priority` | Resolution-selection priority when terrain datasets overlap. |
| `Version` | Dataset schema/content version. |
| `CoverageBounds` | World-space horizontal bounds in the global coordinate system. |
| `TileSize` | Standard sample count per tile axis. |
| `HeightFormat` | Binary height encoding. Phase 2 currently supports `UInt16`. |
| `MinElevation` / `MaxElevation` | Elevation range represented by the dataset. |
| `Attributes` | Explicit metadata fields that are serializable in Unity and portable to other tools. |

---

## Terrain Tile Schema

Terrain tiles store metadata in JSON and sample values in a referenced binary heightmap.

```json
{
  "DatasetId": "MiddleEarth_1m_Test",
  "DatasetType": "Terrain",
  "TileId": "Tile_P000_P000",
  "TileX": 0,
  "TileY": 0,
  "ChunkX": 0,
  "ChunkY": 0,
  "SampleCountX": 257,
  "SampleCountY": 257,
  "CellSize": 1.0,
  "Bounds": {
    "MinX": 0.0,
    "MinY": 0.0,
    "MaxX": 256.0,
    "MaxY": 256.0
  },
  "HeightFormat": "UInt16",
  "MinElevation": 0.0,
  "MaxElevation": 200.0,
  "Version": 1,
  "HeightMapFile": "terrain_P000_P000.bin",
  "Attributes": {
    "Source": "Procedural Perlin test terrain",
    "HistoricalPeriod": "Synthetic Test Data",
    "Author": "Middle_Earth_GIS",
    "Notes": "Representative Phase 2 sample terrain tile."
  },
  "Heights": [],
  "ChunkId": "Tile_P000_P000"
}
```

### Compatibility Fields

`ChunkX`, `ChunkY`, and `ChunkId` are retained only as Phase 0 compatibility aliases for existing streaming and rendering code. New terrain systems should use `DatasetId`, `TileId`, `TileX`, `TileY`, and `Bounds`.

---

## Height Storage Format

Phase 2 stores height samples in external binary files referenced by `HeightMapFile`.

Current format:

```text
UInt16 little-endian samples
Sample order: row-major, south-to-north rows, west-to-east columns
Expected sample count: SampleCountX × SampleCountY
```

The loader must reject files whose sample count does not match the tile metadata.

---

## Bounds and Coordinate Mapping

All tile bounds use the global GIS coordinate system:

```text
MinX = west edge in meters
MinY = south edge in meters
MaxX = east edge in meters
MaxY = north edge in meters
```

A tile sample maps to world space as:

```text
WorldX = Bounds.MinX + SampleX × CellSize
WorldY = Bounds.MinY + SampleY × CellSize
Elevation = decoded height sample
```

Unity converts GIS horizontal `Y` to Unity world `Z` only at render time:

```text
UnityPosition = (WorldX, Elevation, WorldY)
```

---

## Mesh Generation Requirements

Runtime mesh generation must:

- Use loaded terrain tile metadata as input.
- Use `SampleCountX`, `SampleCountY`, and `CellSize` for vertex spacing.
- Name generated meshes with the chunk identifier.
- Position generated chunk objects from world-space `Bounds.MinX` and `Bounds.MinY`.
- Keep generated meshes disposable and non-authoritative.

---

## Validation Requirements

A valid Phase 2 terrain tile must satisfy:

- JSON metadata file exists.
- Referenced heightmap file exists.
- `DatasetType` is `Terrain`.
- `HeightFormat` is supported.
- Sample dimensions are greater than 1.
- Binary height sample count equals `SampleCountX × SampleCountY`.
- Bounds align with the global coordinate system.
- Dataset and tile identifiers are stable.

---

## Current Implementation

The Unity implementation now exposes terrain representation classes under:

```text
UnityProject/Fantasy_World_GIS/Assets/Scripts/Terrain/
```

Key files:

- `TerrainDatasetManifest.cs`
- `TerrainChunkData.cs`
- `TerrainBounds.cs`
- `TerrainAttributes.cs`
- `TerrainConstants.cs`
- `TerrainChunkLoader.cs`
- `TerrainChunkRenderer.cs`
- `GenerateTestTerrain.cs`

The sample dataset is stored under:

```text
UnityProject/Fantasy_World_GIS/Assets/Data/Terrain/
```

with `MiddleEarth_1m_Test.manifest.json` and `Chunk_*.json` tile metadata files.
