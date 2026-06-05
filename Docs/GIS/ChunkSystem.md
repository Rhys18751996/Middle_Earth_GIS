# ChunkSystem.md

## Purpose

Define the authoritative chunk architecture used by Middle_Earth_GIS.

Chunks are the fundamental spatial indexing, streaming, caching, and lookup mechanism used throughout the platform.

Chunks are not terrain datasets.

Chunks are not terrain resolution.

Chunks are not political regions.

Chunks are not gameplay zones.

Chunks are fixed-size square areas of world space used to organize and locate GIS data efficiently.

---

# Design Goals

- Simple coordinate calculations
- Efficient streaming
- Efficient spatial indexing
- Engine independent
- GIS inspired
- Scalable to continent-sized worlds
- Compatible with multiple terrain resolutions
- Suitable for future multi-world support
- Suitable for future underground and vertical layers

---

# Core Architectural Principle

Chunk Grid
=
World Indexing System

Terrain Datasets
=
Resolution Layers

Chunks and terrain resolution are intentionally independent concepts.

This separation is a foundational architectural rule.

---

# Chunk Dimensions

All chunks use a fixed size:

```text
256m × 256m
```

Each chunk covers:

```text
Width: 256 meters
Height: 256 meters
Area: 65,536 square meters
```

Chunk size never changes regardless of terrain dataset resolution.

Examples:

```text
MiddleEarth_500m
MiddleEarth_250m
MiddleEarth_100m
Shire_25m
Hobbiton_5m
BagEnd_1m
```

All use the same chunk grid.

---

# Why A Fixed Chunk Grid?

The chunk grid provides a stable spatial reference system.

Benefits:

- Fast spatial lookups
- Fast streaming calculations
- Dataset-independent indexing
- Consistent caching
- Efficient runtime queries
- Engine-independent architecture

Future datasets can use the same chunk grid without modifying the chunk system.

---

# Chunk Coordinate System

Chunks are indexed using integer coordinates.

Example:

```text
Chunk_100_050
```

Where:

```text
ChunkX = 100
ChunkY = 50
```

Chunk coordinates refer to the south-west corner of the chunk cell.

---

# Chunk Naming Convention

Format:

```text
Chunk_<ChunkX>_<ChunkY>
```

Examples:

```text
Chunk_000_000
Chunk_001_000
Chunk_100_050
Chunk_512_128
```

---

# Chunk Bounds Calculation

Given:

```text
ChunkSize = 256
```

Bounds:

```text
MinX = ChunkX * ChunkSize
MinY = ChunkY * ChunkSize

MaxX = MinX + ChunkSize
MaxY = MinY + ChunkSize
```

Example:

```text
Chunk_100_050
```

Produces:

```text
MinX = 25600
MinY = 12800

MaxX = 25856
MaxY = 13056
```

---

# World To Chunk Conversion

World coordinates can be converted into chunk coordinates:

```text
ChunkX = floor(WorldX / 256)
ChunkY = floor(WorldY / 256)
```

Example:

```text
World Position

X = 25701
Y = 12825
```

Produces:

```text
ChunkX = 100
ChunkY = 50
```

Result:

```text
Chunk_100_050
```

---

# Chunk To World Conversion

The south-west corner of a chunk:

```text
WorldX = ChunkX * 256
WorldY = ChunkY * 256
```

Example:

```text
Chunk_100_050
```

Produces:

```text
X = 25600
Y = 12800
```

---

# Chunk Responsibilities

Chunks are responsible for:

- Spatial indexing
- Runtime streaming
- Runtime caching
- Dataset discovery
- Spatial queries
- Loading prioritization
- Validation support

Chunks are NOT responsible for:

- Terrain fidelity
- Terrain authoring
- Terrain resolution
- Political boundaries
- World ownership

---

# Terrain Dataset Relationship

A chunk may overlap multiple terrain datasets simultaneously.

Example:

```text
MiddleEarth_500m
MiddleEarth_250m
MiddleEarth_100m
Shire_25m
```

For a given coordinate:

Runtime systems determine:

```text
Highest Resolution Available
```

The chunk system simply helps locate datasets.

---

# Dataset Usage

Any GIS dataset may use chunk indexing.

Examples:

- Terrain
- Roads
- Rivers
- Settlements
- Structures
- Regions
- Vegetation
- World Objects

Chunk boundaries must never modify dataset geometry.

Example:

A road crossing ten chunks remains one road.

A river crossing one hundred chunks remains one river.

---

# Future Layer Support

Multiple world layers may use identical chunk coordinates.

Example:

```text
Surface Layer
Chunk_100_050

Moria Layer
Chunk_100_050

BagEndInterior Layer
Chunk_100_050
```

Layer identifiers distinguish overlapping worlds.

Chunk coordinates remain unchanged.

---

# Example Chunk Record

```json
{
  "ChunkId": "Chunk_100_050",
  "ChunkX": 100,
  "ChunkY": 50,
  "Bounds": {
    "MinX": 25600,
    "MinY": 12800,
    "MaxX": 25856,
    "MaxY": 13056
  }
}
```

---

# Rules

1. Chunk size is fixed at 256m × 256m.
2. Chunk coordinates must be integers.
3. Chunk naming must follow the documented convention.
4. Chunk grid and terrain resolution are independent concepts.
5. Chunk boundaries must never become political boundaries.
6. Chunk boundaries must never become gameplay boundaries.
7. Chunk boundaries must never alter GIS geometry.
8. Persistent world data must always be convertible between world coordinates and chunk coordinates.
9. Multiple datasets may overlap the same chunk.
10. Future systems must remain compatible with this specification.

---

# Key Mental Model

Do not think:

```text
Chunk
    =
Terrain
```

Think:

```text
Chunk
    =
World Index Cell
```

and

```text
Terrain Dataset
    =
Resolution Layer
```

This separation enables large-scale GIS architecture, progressive terrain refinement, and long-term platform scalability.
