# Chunk System

## Purpose

Define the authoritative chunk architecture used by Middle_Earth_GIS.
Chunks are the fundamental storage, streaming, loading, saving, and editing units for terrain data.
Chunks are not political regions, terrain files, or gameplay zones.
Chunks are fixed-size square areas of world space.

---

# Design Goals

- Simple coordinate calculations
- Efficient streaming
- Engine independent
- GIS inspired
- Scalable to very large worlds
- Suitable for future multi-world support
- Suitable for future underground and vertical layers

---

# Chunk Dimensions

All terrain chunks use a fixed size:

```text
256m x 256m
```

Each chunk covers:

```text
Width: 256 meters
Height: 256 meters
Area: 65,536 square meters
```

---

# Heightmap Resolution

Default terrain resolution:

```text
257 x 257 samples
```

Resulting in:

```text
256 x 256 terrain cells
1 meter per cell
```

This provides shared border vertices between neighbouring chunks and eliminates terrain seams.

---

# Why 257 × 257 Samples?

A 256m × 256m terrain chunk contains 256 terrain cells.

Because terrain cells are formed between vertices:

```text
256 cells require 257 vertices
```

Benefits:

- Shared border vertices between neighbouring chunks
- No visible cracks between chunks
- No stitching algorithms required
- Easier terrain editing
- Easier LOD generation
- Compatible with common terrain engine practices

Additional storage cost is negligible while significantly simplifying future development.

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

Chunk coordinates always refer to the south-west corner of the chunk grid cell.

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

Terrain identifiers may use:

```text
TERRAIN_0100_0050
```

for dataset-specific references.

---

# Chunk Bounds Calculation

Given:

```text
ChunkSize = 256
```

Bounds are calculated as:

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

World coordinates can be converted into chunk coordinates using:

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

The south-west corner of a chunk is calculated as:

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
World Position

X = 25600
Y = 12800
```

---

# Chunk Responsibilities

Chunks are responsible for:

- Terrain storage
- Terrain streaming
- Terrain loading
- Terrain saving
- Terrain editing
- Terrain validation

---

# Chunk Independence

Chunks must remain independent from:

- Political regions
- Kingdom boundaries
- Roads
- Rivers
- Settlements
- Vegetation
- Runtime gameplay systems

Chunks represent storage units, not logical world regions.

---

# Future Dataset Usage

Other datasets may optionally use chunk-based spatial indexing for performance.

Examples:

- Vegetation
- Roads
- Rivers
- Structures
- World Objects

However, chunk boundaries must never modify dataset geometry.

---

# Future Layer Support

Multiple world layers may use identical chunk coordinates.

Examples:

```text
Surface Layer
Chunk_100_050

Moria Layer
Chunk_100_050
```

Layer identifiers distinguish overlapping worlds.

Chunk coordinates remain unchanged.

---

# Example Chunk

```json
{
  "ChunkId": "TERRAIN_0100_0050",
  "ChunkX": 100,
  "ChunkY": 50,
  "Bounds": {
    "MinX": 25600,
    "MinY": 12800,
    "MaxX": 25856,
    "MaxY": 13056
  },
  "Resolution": 1.0
}
```

---

# Rules

1. All terrain data must belong to exactly one chunk.
2. Chunk size is fixed at 256m × 256m.
3. Heightmaps use 257 × 257 samples.
4. Chunk coordinates must be integers.
5. Chunk naming must follow the documented convention.
6. Chunk boundaries must never be used as political or gameplay boundaries.
7. Persistent world data must always be convertible between world coordinates and chunk coordinates.
8. Future systems must remain compatible with this specification.
