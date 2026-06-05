# TerrainDataSystem.md

## Purpose

Define the authoritative terrain architecture used by Middle_Earth_GIS.

Terrain data must remain independent of rendering engines, runtime systems, editors, and gameplay logic.

The terrain system defines how terrain is:

- Stored
- Streamed
- Indexed
- Loaded
- Saved
- Imported
- Exported
- Validated
- Distributed

Terrain is represented as GIS datasets.

---

# Design Goals

- Engine Independent
- GIS Inspired
- Multi-Resolution
- Streamable
- Editable
- Versionable
- Scalable
- Community Friendly
- Suitable for Continent-Sized Worlds
- Suitable for Future Multi-World Support

---

# Core Architectural Principle

Terrain is not a Unity Terrain.

Terrain is not a collection of meshes.

Terrain is not a collection of chunks.

Terrain is a collection of GIS datasets.

Examples:

- MiddleEarth_500m
- MiddleEarth_250m
- MiddleEarth_100m
- Shire_25m
- Hobbiton_5m
- BagEnd_1m
- BagEndInterior_0.25m

Unity is simply a consumer of terrain datasets.

---

# Authoritative Terrain Model

Highest Resolution Available
=
Authoritative Truth

Lower Resolution Datasets
=
Generated Products

Example:

BagEnd_1m

is authoritative.

MiddleEarth_100m is not.

MiddleEarth_250m is not.

MiddleEarth_500m is not.

---

# Terrain Dataset Architecture

Terrain is organized into independent datasets.

Example:

MiddleEarth_500m
covers entire world

MiddleEarth_250m
covers entire world

MiddleEarth_100m
covers entire world

Shire_25m
covers The Shire

Hobbiton_5m
covers Hobbiton

BagEnd_1m
covers Bag End

Datasets may overlap.

This is expected.

---

# Terrain Dataset Schema

Each terrain dataset contains:

- DatasetId
- CellSize
- CoverageBounds
- Priority
- Version
- Tile Metadata
- Tile References

Example:

```json
{
  "DatasetId": "MiddleEarth_500m",
  "CellSize": 500.0,
  "Priority": 1,
  "Version": 1
}
```

---

# Terrain Tiles

Terrain datasets are stored using GIS raster tiles.

Tiles are storage units.

Tiles are not world regions.

Tiles are not gameplay zones.

Tiles are not chunk grid cells.

---

# Tile Dimensions

Tile dimensions remain roughly constant.

Recommended:

```text
256 × 256 samples
```

Coverage changes according to dataset resolution.

Examples:

500m Dataset

```text
128 km × 128 km
```

250m Dataset

```text
64 km × 64 km
```

100m Dataset

```text
25.6 km × 25.6 km
```

25m Dataset

```text
6.4 km × 6.4 km
```

5m Dataset

```text
1.28 km × 1.28 km
```

1m Dataset

```text
256 m × 256 m
```

---

# Relationship To Chunk System

Chunks and terrain datasets are separate concepts.

Chunk Grid:

- Streaming
- Indexing
- Caching
- Dataset lookup

Terrain Datasets:

- Terrain fidelity
- Terrain authoring
- Terrain storage

Core Rule:

Chunk Grid ≠ Terrain Resolution

---

# Runtime Dataset Selection

When terrain is requested for a coordinate:

1. Find all datasets covering the coordinate.
2. Compare resolutions.
3. Select the highest resolution dataset.
4. Use that dataset as runtime truth.

Example:

Available:

- 500m
- 250m
- 100m
- 25m

Result:

25m

---

# Terrain Resolution Roadmap

Initial world creation:

```text
MiddleEarth_500m
```

Future refinement:

```text
MiddleEarth_500m
    ↓
MiddleEarth_250m
    ↓
MiddleEarth_100m
    ↓
Regional HD Datasets
    ↓
Local HD Datasets
```

The architecture must support progressive improvement over many years.

---

# Downsampling Pipeline

Lower resolution datasets are generated from higher resolution datasets.

Example:

```text
BagEnd_1m
    ↓
Generate
    ↓
Hobbiton_5m
    ↓
Generate
    ↓
Shire_25m
    ↓
Generate
    ↓
MiddleEarth_100m
    ↓
Generate
    ↓
MiddleEarth_250m
    ↓
Generate
    ↓
MiddleEarth_500m
```

Higher resolution datasets are authoritative.

Lower resolution datasets are derived.

---

# Import Architecture

Import formats are not authoritative.

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
Updated Source File
```

---

# Height Storage

Recommended:

```text
UInt16
```

Range:

```text
0 → 65535
```

Benefits:

- Compact storage
- Fast streaming
- GIS compatibility
- Industry standard

Alternative formats may be supported in future versions.

---

# Dataset Distribution

Base Package:

```text
MiddleEarth_500m
```

Optional Packages:

```text
MiddleEarth_250m
MiddleEarth_100m
Shire_HD
Moria_HD
MinasTirith_HD
```

Users should only download datasets they require.

---

# Object Storage

Long-term storage should support:

- Cloudflare R2
- Amazon S3
- Self-hosted object storage

Datasets must be independently versioned and distributable.

---

# Validation Requirements

Terrain datasets must validate:

- Dataset identifiers
- Coverage bounds
- Tile references
- Resolution metadata
- Elevation ranges
- Height data integrity
- Dataset version compatibility

Critical failures must prevent loading.

---

# Runtime Requirements

Runtime systems must:

- Discover overlapping datasets
- Select highest resolution dataset
- Stream required tiles
- Cache loaded data
- Remain independent of authoring tools

Runtime systems must never become authoritative.

---

# Rules

1. Terrain is represented as GIS datasets.
2. Highest resolution available is authoritative.
3. Lower resolutions are generated products.
4. Datasets may overlap.
5. Chunk grid and terrain resolution are independent concepts.
6. Runtime systems must select the highest resolution available.
7. Import formats are not authoritative.
8. Runtime representations are not authoritative.
9. Terrain datasets must remain independently distributable.
10. Future systems must remain compatible with this specification.
