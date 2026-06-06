# TerrainArchitecture.md

## Purpose

Define the authoritative terrain architecture used by Middle_Earth_GIS.

Terrain is represented as GIS raster datasets rather than engine-specific terrain objects.

Terrain data must remain independent of:

- Unity
- Rendering systems
- Runtime systems
- Editors
- Storage implementations

Terrain architecture is designed to support progressive refinement of a world over many years.

---

# Core Principle

Terrain is not a Unity terrain.

Terrain is a collection of GIS datasets.

Examples:

```text
MiddleEarth_256m
MiddleEarth_128m
MiddleEarth_64m
MiddleEarth_32m
Shire_16m
Hobbiton_4m
BagEnd_1m
```

The world is assembled from terrain datasets.

---

# Terrain Dataset Model

Terrain is stored as independent datasets.

Each dataset contains:

- Dataset metadata
- Coverage bounds
- Resolution
- Tile metadata
- Tile references

Example:

```json
{
  "DatasetId": "MiddleEarth_128m",
  "DatasetType": "Terrain",
  "CellSize": 128.0,
  "CoverageBounds": {},
  "Priority": 1,
  "Version": 1
}
```

---

# Terrain Resolution Hierarchy

Terrain datasets use powers-of-two resolutions.

```text
256m
128m
64m
32m
16m
8m
4m
2m
1m
```

This hierarchy is a core architectural standard.

Future datasets should follow this hierarchy whenever possible.

---

# Multi-Resolution Terrain

Terrain datasets may overlap.

Example:

```text
MiddleEarth_256m
covers entire world

MiddleEarth_128m
covers entire world

MiddleEarth_64m
covers entire world

Shire_16m
covers The Shire

Hobbiton_4m
covers Hobbiton

BagEnd_1m
covers Bag End
```

Overlapping datasets are expected.

Overlapping datasets are not duplicates.

Each dataset provides a different level of detail.

---

# Runtime Terrain Selection

For any coordinate:

1. Find all terrain datasets covering the location.
2. Select the highest resolution available.

Example:

Available:

```text
256m
128m
64m
16m
```

Result:

```text
16m
```

Runtime always uses:

```text
Highest Resolution Available
```

---

# Authoritative Truth Model

The highest resolution available dataset is authoritative.

Example:

```text
BagEnd_1m
```

is authoritative.

Not:

```text
MiddleEarth_64m
```

Local improvements become the source of truth.

This allows terrain quality to increase incrementally over time.

---

# Downsampling Architecture

Higher-resolution datasets generate lower-resolution datasets.

Example:

```text
BagEnd_1m
    ↓
Generate
    ↓
Hobbiton_2m
    ↓
Generate
    ↓
Hobbiton_4m
    ↓
Generate
    ↓
Shire_8m
    ↓
Generate
    ↓
Shire_16m
    ↓
Generate
    ↓
MiddleEarth_32m
    ↓
Generate
    ↓
MiddleEarth_64m
    ↓
Generate
    ↓
MiddleEarth_128m
    ↓
Generate
    ↓
MiddleEarth_256m
```

Lower-resolution datasets are generated products.

Higher-resolution datasets are authoritative source data.

---

# Terrain Tiles

Terrain datasets are divided into raster tiles.

Tiles are storage units.

Tiles are not chunks.

Example:

```text
Dataset
    └── Tiles
            └── Height Samples
```

A tile stores:

- Tile coordinates
- Sample counts
- Cell size
- Height data reference

Example:

```json
{
  "TileId": "Tile_0042_0017",
  "TileX": 42,
  "TileY": 17,
  "CellSize": 128.0,
  "SampleCountX": 256,
  "SampleCountY": 256
}
```

---

# Terrain Tile Coverage

Tile dimensions remain constant.

Example:

```text
256 × 256 samples
```

Coverage varies by dataset resolution.

Examples:

```text
256m Dataset
65.536 km × 65.536 km

128m Dataset
32.768 km × 32.768 km

64m Dataset
16.384 km × 16.384 km

32m Dataset
8.192 km × 8.192 km

16m Dataset
4.096 km × 4.096 km

8m Dataset
2.048 km × 2.048 km

4m Dataset
1.024 km × 1.024 km

2m Dataset
512 m × 512 m

1m Dataset
256 m × 256 m
```

---

# Chunk Relationship

Chunk Grid:

```text
256m × 256m
```

Terrain Resolution:

```text
256m
128m
64m
32m
16m
8m
4m
2m
1m
```

These are independent concepts.

Core Principle:

```text
Chunk Grid
=
World Indexing System

Terrain Datasets
=
Resolution Layers
```

Chunks locate terrain.

Chunks do not define terrain resolution.

---

# Terrain Source Files

Source files are import/export formats.

Examples:

- GeoTIFF
- TIFF
- RAW
- PNG Heightmaps

Workflow:

```text
Source File
    ↓
Import
    ↓
Terrain Dataset
    ↓
Editing
    ↓
Export
    ↓
Source File
```

Terrain datasets are authoritative.

Import formats are not.

---

# Storage Independence

Terrain architecture must remain independent of storage technology.

Examples:

- Local disk
- SQLite
- Object storage
- Cloud storage

Storage implementations may change.

Terrain architecture must not.

---

# Distribution Strategy

Base Package:

```text
MiddleEarth_256m
```

Optional Packages:

```text
MiddleEarth_128m
MiddleEarth_64m
MiddleEarth_32m
Shire_HD
Moria_HD
MinasTirith_HD
```

Users download only the datasets they require.

---

# Future Support

The architecture must support:

- New terrain datasets
- Higher-resolution regions
- Community-created worlds
- Multiple worlds
- Multiple world layers
- Distributed storage
- Cloud streaming

Without redesigning the terrain system.

---

# Rules

1. Terrain is stored as GIS datasets.
2. Terrain datasets may overlap.
3. Higher-resolution datasets are authoritative.
4. Lower-resolution datasets are generated products.
5. Runtime uses the highest resolution available.
6. Terrain resolutions should use powers-of-two spacing.
7. Terrain tiles are storage units.
8. Chunks are indexing units.
9. Terrain datasets remain independent of game engines.
10. Terrain datasets remain independent of storage implementations.

---

# Key Mental Model

Do not think:

```text
Terrain
=
Unity Terrain
```

Think:

```text
Terrain
=
GIS Raster Datasets
```

and

```text
Highest Resolution Available
=
Authoritative Truth
```