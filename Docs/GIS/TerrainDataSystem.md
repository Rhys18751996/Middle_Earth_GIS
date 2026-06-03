# Terrain Data System

## Purpose

Define the authoritative terrain data architecture used by Middle_Earth_GIS.
Terrain data must remain independent of rendering engines, game logic, and editor implementations.
The terrain data system defines how terrain is stored, loaded, saved, streamed, validated, imported, and exported.
Terrain data is the authoritative representation of world elevation.

---

# Design Goals

- Engine independent
- GIS inspired
- Streamable
- Editable
- Versionable
- Efficient storage
- Efficient loading
- Efficient saving
- Suitable for large worlds
- Compatible with future terrain editing workflows

---

# Authoritative Terrain Representation

Terrain data is stored as chunked terrain datasets.
Each terrain chunk represents a fixed-size section of world space as defined by the Chunk System specification.
Terrain chunks are the authoritative terrain representation during development.
Source files such as GeoTIFF, TIFF, RAW, and PNG are considered import/export formats only.

Workflow:

```text
Source Terrain
    ↓
Import
    ↓
Terrain Chunks
    ↓
Editing
    ↓
Export
    ↓
Updated Source Terrain
```

---

# Terrain File Structure

Each terrain chunk consists of two files:

```text
terrain_0100_0050.json
terrain_0100_0050.bin
```

Purpose:

```text
JSON = Metadata
BIN  = Height Data
```

This keeps metadata human-readable while keeping terrain loading efficient.

---

# Directory Structure

Example:

```text
Data/
└── Terrain/
    ├── terrain_0100_0050.json
    ├── terrain_0100_0050.bin
    ├── terrain_0101_0050.json
    └── terrain_0101_0050.bin
```

---

# Height Storage Format

Height values are stored as:

```text
UInt16
```

Range:

```text
0 → 65535
```

Benefits:

- Small file size
- Fast loading
- Fast streaming
- Industry-standard terrain representation
- Sufficient precision for large worlds

---

# Elevation Range

Default elevation mapping:

```text
0      = -1000m
65535  = +9000m
```
or
```text
{
  "MinElevation": -1000,
  "MaxElevation": 9000
}
```

Resulting in:

```text
10,000m total elevation range
```

This range supports:

- Sea floors
- Middle-earth terrain
- Real-world terrain
- Future fantasy worlds

---

# Height Conversion

Stored values are converted into world elevation using:

```text
Elevation =
MinElevation +
((HeightValue / 65535.0) * ElevationRange)
```

Example:

```text
HeightValue = 32767
```

Produces approximately:

```text
4000m elevation
```

---

# Terrain Resolution

Terrain resolution is defined by the Chunk System.

Default:

```text
Chunk Size:
256m × 256m

Height Samples:
257 × 257

Cell Size:
1m
```

A 256m × 256m chunk contains 256 × 256 terrain cells.

257 × 257 samples are used so neighbouring chunks share border vertices seamlessly.

---

# Terrain Chunk Schema

Example:

```json
{
  "SchemaVersion": 1,
  "FeatureType": "TerrainChunk",
  "ChunkId": "TERRAIN_0123_0456",

  "ChunkX": 123,
  "ChunkY": 456,

  "Bounds": {
    "MinX": 31488,
    "MinY": 116736,
    "MaxX": 31744,
    "MaxY": 116992
  },

  "ChunkSize": 256,

  "SampleCountX": 257,
  "SampleCountY": 257,

  "CellSize": 1.0,

  "HeightFormat": "UInt16",

  "MinElevation": -1000,
  "MaxElevation": 9000,

  "HeightMapFile": "terrain_0123_0456.bin",

  "Attributes": {
    "Source": "Generated",
    "Biome": "Grassland"
  }
}
```

---

# Schema Versioning

All terrain chunks must contain:

```json
{
  "SchemaVersion": 1
}
```

Purpose:

- Backward compatibility
- Future schema evolution
- Safe migrations
- Dataset validation

Future versions may introduce additional fields without invalidating older datasets.

---

# Terrain Attributes

The Attributes section stores optional metadata.

Examples:

```json
{
  "Attributes": {
    "Source": "Imported",
    "Biome": "Grassland",
    "Region": "The Shire"
  }
}
```

Attributes must never contain data required to reconstruct terrain geometry.

Terrain geometry must remain fully recoverable from the heightmap and schema fields.

---

# Validation Requirements

Terrain chunks must satisfy:

- Valid SchemaVersion
- Valid ChunkId
- Valid Chunk Coordinates
- Valid Bounds
- Valid HeightMapFile reference
- Valid sample dimensions
- Valid elevation range
- Existing heightmap file
- Correct UInt16 height count

Critical validation failures must prevent terrain loading.
Non-critical validation failures should generate warnings.

---

# Loading Requirements

Terrain loaders must:

- Read JSON metadata
- Validate schema
- Load binary height data
- Validate sample count
- Convert heights to elevations
- Generate runtime terrain representation

Runtime representations must never become the authoritative terrain source.

---

# Saving Requirements

Terrain save systems must:

- Preserve schema compatibility
- Save modified height data
- Save updated metadata
- Preserve chunk identifiers
- Preserve coordinate integrity

---

# Streaming Requirements

Terrain chunks must support:

- Independent loading
- Independent unloading
- Independent saving
- Independent validation

Neighbouring chunks must not be required to load a chunk.

---

# Rules

1. Terrain chunks are the authoritative terrain representation.
2. Source terrain files are import/export formats only.
3. Height data must be stored using UInt16 values.
4. Terrain metadata must be stored in JSON.
5. All terrain chunks must contain a SchemaVersion.
6. Terrain geometry must be recoverable from chunk data alone.
7. Runtime representations must never become authoritative.
8. Terrain chunks must remain compatible with the Chunk System specification.
9. Validation failures must prevent terrain loading.
10. Future systems must remain compatible with this specification.
