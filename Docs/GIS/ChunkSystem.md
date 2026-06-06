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

Chunk size never changes.

---

# Chunk Coordinate System

Chunks are identified using integer coordinates.

Example:

```text
Chunk_P100_P050
```

Where:

```text
ChunkX = 100
ChunkY = 50
```

Chunk coordinates reference the south-west corner of the chunk cell.

---

# Chunk Naming Convention

Format:

```text
Chunk_P<ChunkX>_P<ChunkY>
```

Examples:

```text
Chunk_P000_P000
Chunk_P001_P000
Chunk_P100_P050
Chunk_P512_P128
```

---

# Chunk Bounds

Given:

```text
ChunkSize = 256
```

Bounds:

```text
MinX = ChunkX × ChunkSize
MinY = ChunkY × ChunkSize

MaxX = MinX + ChunkSize
MaxY = MinY + ChunkSize
```

Example:

```text
Chunk_P100_P050
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

Convert world coordinates to chunk coordinates:

```text
ChunkX = floor(WorldX / 256)
ChunkY = floor(WorldY / 256)
```

Example:

```text
WorldX = 25701
WorldY = 12825
```

Produces:

```text
ChunkX = 100
ChunkY = 50
```

Result:

```text
Chunk_P100_P050
```

---

# Chunk To World Conversion

Convert chunk coordinates to world coordinates:

```text
WorldX = ChunkX × 256
WorldY = ChunkY × 256
```

Example:

```text
Chunk_P100_P050
```

Produces:

```text
WorldX = 25600
WorldY = 12800
```

---

# Chunk Responsibilities

Chunks are responsible for:

- Spatial indexing
- Dataset discovery
- Runtime streaming
- Runtime caching
- Spatial queries
- Runtime lookup
- Loading prioritization

---

# Chunk Non-Responsibilities

Chunks are NOT responsible for:

- Terrain fidelity
- Terrain resolution
- Terrain authoring
- Political boundaries
- Gameplay boundaries
- World ownership
- GIS geometry

---

# Dataset Relationship

Multiple datasets may overlap the same chunk.

Examples:

```text
MiddleEarth_256m
MiddleEarth_128m
MiddleEarth_64m
Shire_16m
Hobbiton_4m
BagEnd_1m
```

A chunk does not select which dataset is authoritative.

Runtime systems determine:

```text
Highest Resolution Available
```

The chunk system only assists in locating datasets.

---

# Geometry Rules

Chunk boundaries must never alter GIS geometry.

Examples:

- A road crossing 10 chunks remains one road.
- A river crossing 100 chunks remains one river.
- A region spanning 500 chunks remains one region.

Chunks index geometry.

Chunks do not own geometry.

---

# Future Layer Support

Multiple world layers may use identical chunk coordinates.

Example:

```text
Surface Layer
Chunk_P100_P050

Moria Layer
Chunk_P100_P050

BagEndInterior Layer
Chunk_P100_P050
```

Layer identifiers distinguish overlapping worlds.

Chunk coordinates remain unchanged.

---

# Rules

1. Chunk size is fixed at 256m × 256m.
2. Chunk coordinates must be integers.
3. Chunk naming must follow the documented convention.
4. Chunk grid and terrain resolution are independent concepts.
5. Chunk boundaries must never become political boundaries.
6. Chunk boundaries must never become gameplay boundaries.
7. Chunk boundaries must never alter GIS geometry.
8. Multiple datasets may overlap the same chunk.
9. Chunk coordinates must always be derivable from world coordinates.
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