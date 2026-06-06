# CoordinateSystem.md

## Purpose

Define the authoritative world coordinate system used by Middle_Earth_GIS.

All datasets, runtime systems, editors, validation tools, and future extensions must use this coordinate system.

A shared coordinate system is required to allow independent GIS datasets to interact correctly.

---

# Core Principle

All world data exists within a single global coordinate system.

Every dataset references the same world space.

Examples:

- Terrain
- Roads
- Rivers
- Settlements
- Regions
- Structures
- Vegetation
- World Objects
- Future Datasets

No dataset owns its own persistent coordinate space.

---

# World Origin

The world origin is located at the south-west corner of the world.

```text
Origin = (0,0)
```

World coordinates increase:

```text
+X = East
+Y = North
```

Example:

```text
          North (+Y)
                ^
                |
                |
                |
(0,0) ----------+--------> East (+X)
South-West
```

---

# Coordinate Units

The platform uses meters as its base unit.

```text
1 Unit = 1 Meter
```

Examples:

```text
Road Width = 6m

Building Height = 20m

Chunk Size = 256m
```

All persistent GIS data should use meters.

---

# Coordinate Format

World coordinates are stored using:

```text
(X, Y)
```

Where:

```text
X = East-West Position
Y = North-South Position
```

Example:

```text
(125000, 752000)
```

Represents:

```text
125 km East
752 km North
```

from the world origin.

---

# World Bounds

The coordinate system supports worlds of arbitrary size.

Current Middle-earth estimate:

```text
Width  = 5,760,000m
Height = 4,320,000m
```

Equivalent to:

```text
Width  = 5,760 km
Height = 4,320 km
```

These values are not hard limits.

Future worlds may be larger or smaller.

---

# Dataset Alignment

All datasets must align to the same coordinate system.

Examples:

Terrain:

```text
(125000, 752000)
```

Road Node:

```text
(125000, 752000)
```

Settlement:

```text
(125000, 752000)
```

Region Boundary Vertex:

```text
(125000, 752000)
```

Matching coordinates always reference the same location.

---

# Coordinate Independence

Datasets remain independent while sharing coordinates.

Examples:

- Moving a road does not modify terrain.
- Changing a region border does not modify terrain.
- Adding a settlement does not modify roads.

Relationships are created through shared coordinates and references.

Not through ownership.

---

# Chunk Relationship

Chunks are derived from world coordinates.

Example:

```text
Chunk Size = 256m
```

Conversion:

```text
ChunkX = floor(WorldX / 256)
ChunkY = floor(WorldY / 256)
```

The coordinate system is authoritative.

The chunk grid is derived from coordinates.

---

# Terrain Relationship

Terrain datasets use the coordinate system.

Examples:

```text
MiddleEarth_256m
MiddleEarth_128m
MiddleEarth_64m
Shire_16m
Hobbiton_4m
BagEnd_1m
```

All terrain datasets align to the same world coordinates regardless of resolution.

Resolution does not create a new coordinate system.

---

# Future Layer Support

Multiple world layers may share the same coordinates.

Examples:

```text
Surface Layer
(125000, 752000)

Moria Layer
(125000, 752000)

BagEndInterior Layer
(125000, 752000)
```

Layer identifiers distinguish overlapping spaces.

Coordinates remain unchanged.

---

# Runtime Systems

Runtime systems consume world coordinates.

Examples:

- Streaming
- Rendering
- Navigation
- Queries
- Validation

Runtime systems must never become the authoritative source of coordinates.

---

# Rules

1. The world origin is the south-west corner of the world.
2. World units are measured in meters.
3. All datasets share the same coordinate system.
4. No dataset may define its own persistent coordinate space.
5. Chunk coordinates must be derived from world coordinates.
6. Terrain resolution must not affect coordinates.
7. Runtime systems must not redefine coordinates.
8. Future world layers must remain compatible with this specification.

---

# Key Mental Model

Do not think:

```text
Terrain
=
Coordinate System
```

Think:

```text
Coordinate System
=
Shared World Space
```

and

```text
Datasets
=
Layers Within That Space
```