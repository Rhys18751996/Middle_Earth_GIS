# RegionArchitecture.md

## Purpose

Define the authoritative region architecture used by Middle_Earth_GIS.

Regions represent logical, political, administrative, cultural, or geographic areas within the world.

Regions are GIS polygon features.

Regions are independent of:

- Terrain
- Roads
- Rivers
- Settlements
- Chunk boundaries
- Runtime systems

---

# Core Principle

Regions define areas.

Terrain defines elevation.

These are separate concepts.

Example:

```text
Terrain
=
Height Information

Region
=
Area Definition
```

A region may cover any terrain.

A terrain dataset may contain many regions.

---

# Geometry Model

Regions use polygon geometry.

Conceptually:

```text
Vertex
    ↓
Vertex
    ↓
Vertex
    ↓
Vertex
```

The polygon forms a closed boundary.

Regions may contain:

- Outer boundaries
- Inner boundaries (holes)

---

# Region Types

Examples:

```text
Political Region
Administrative Region
Kingdom
Province
Biome
District
Territory
Cultural Region
```

Future region types may be added without modifying the architecture.

---

# Region Identity

Every region must have a unique identifier.

Example:

```text
REGION_GONDOR
REGION_ROHAN
REGION_SHIRE
```

Identifiers should remain stable.

---

# Region Attributes

Examples:

```text
Name
Population
GovernmentType
Owner
HistoricalPeriod
PrimaryRace
Language
```

Attributes describe a region.

Attributes do not define geometry.

---

# Region Hierarchy

Regions may be nested.

Example:

```text
Middle-earth
    └── Gondor
            └── Minas Tirith District
```

Parent-child relationships should be expressed through references.

---

# Terrain Relationship

Regions do not own terrain.

Terrain does not own regions.

Example:

```text
Gondor
```

may contain:

```text
Mountains
Plains
Forests
Rivers
```

The region remains a polygon dataset.

---

# Chunk Relationship

Regions may span any number of chunks.

Chunks index regions.

Chunks do not own regions.

Region boundaries must never be modified by chunk boundaries.

---

# Runtime Relationship

Runtime systems may generate:

- Region overlays
- Labels
- Political maps
- Query systems

Runtime objects are temporary.

Regions remain authoritative.

---

# Rules

1. Regions are polygon GIS features.
2. Regions use the global coordinate system.
3. Regions are independent of terrain.
4. Regions are independent of chunk boundaries.
5. Regions may be nested.
6. Runtime systems are not authoritative.
7. Region identifiers must remain stable.

---

# Key Mental Model

Do not think:

```text
Region
=
Terrain Area
```

Think:

```text
Region
=
Polygon GIS Feature
```