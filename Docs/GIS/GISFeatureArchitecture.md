# GISFeatureArchitecture.md

## Purpose

Define the authoritative feature architecture used by Middle_Earth_GIS.

Features are the fundamental GIS objects stored inside datasets.

Examples:

- Roads
- Rivers
- Settlements
- Regions
- Structures
- Vegetation
- World Objects
- Portals

All non-terrain world data is represented as GIS features.

---

# Core Principle

Datasets contain features.

```text
Dataset
    └── Features
```

Examples:

```text
Road Dataset
    ├── Great East Road
    ├── Greenway
    └── North Road
```

```text
Settlement Dataset
    ├── Hobbiton
    ├── Bree
    └── Minas Tirith
```

Features are the primary representation of world objects.

---

# Feature Identity

Every feature must have a unique identifier.

Example:

```text
ROAD_0001
RIVER_0001
SETTLEMENT_0001
REGION_0001
```

Feature identifiers should remain stable.

Names may change.

Identifiers should not.

---

# Feature Structure

All features share common properties.

Conceptually:

```text
Feature
├── FeatureId
├── FeatureType
├── Attributes
└── Geometry
```

Geometry varies by feature type.

---

# Geometry Types

Middle_Earth_GIS uses standard GIS geometry models.

---

## Point Features

Represent a single location.

Examples:

```text
Settlement
WorldObject
Portal
Landmark
```

Conceptually:

```text
(X,Y)
```

---

## Polyline Features

Represent connected linear geometry.

Examples:

```text
Road
River
LinearObject
TradeRoute
```

Conceptually:

```text
Point
    ↓
Point
    ↓
Point
```

A polyline may contain any number of vertices.

---

## Polygon Features

Represent enclosed areas.

Examples:

```text
Region
Structure Footprint
Vegetation Area
Biome
```

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

The first and last vertex form a closed shape.

---

# Attributes

Features may contain attributes.

Attributes store metadata.

Examples:

```text
Name
Population
Owner
HistoricalPeriod
RoadClass
RiverWidth
```

Attributes extend features without changing geometry.

---

# Feature Independence

Features remain independent objects.

Examples:

```text
Road
```

is not part of:

```text
Terrain
```

and

```text
Settlement
```

is not part of:

```text
Region
```

Relationships are expressed through references.

---

# Feature References

Features may reference:

- Other features
- Datasets
- Assets
- Layers

Examples:

```text
Settlement
    → Region

Structure
    → Asset

Portal
    → World Layer
```

References should use identifiers.

Avoid direct object dependencies.

---

# Coordinate Relationship

All features use the global coordinate system.

Example:

```text
Road Vertex
Settlement Position
Region Boundary
River Vertex
```

All share the same world coordinates.

Features must never define their own persistent coordinate space.

---

# Chunk Relationship

Features may span any number of chunks.

Examples:

```text
Great East Road
```

may cross:

```text
500 Chunks
```

```text
Gondor Region
```

may cover:

```text
Thousands Of Chunks
```

Chunks index features.

Chunks do not own features.

---

# Terrain Relationship

Terrain and features are separate layers.

Examples:

```text
Road
```

crosses terrain.

```text
Settlement
```

sits on terrain.

```text
Region
```

covers terrain.

Terrain does not own these features.

---

# Runtime Relationship

Runtime systems consume features.

Example:

```text
Feature
    ↓
Runtime Object
```

Runtime objects are temporary.

Features remain authoritative.

---

# Feature Types

Current feature types:

```text
Road
River
Settlement
Region
Structure
Vegetation
WorldObject
Portal
LinearObject
```

Future feature types may be added without modifying existing features.

---

# Validation

Features should support validation.

Examples:

- Valid geometry
- Valid coordinates
- Valid references
- Required attributes

Invalid features should not become authoritative.

---

# Serialization

Features must support persistence.

Examples:

```text
JSON
GeoJSON
Binary Formats
Future Formats
```

Serialization format is independent of feature architecture.

---

# Future Support

The architecture must support:

- New feature types
- Multiple worlds
- Vertical worlds
- Community datasets
- Distributed storage
- Engine-independent runtimes

Without redesigning the feature model.

---

# Rules

1. All features belong to a dataset.
2. Every feature must have a unique identifier.
3. Features must use the global coordinate system.
4. Features remain independent objects.
5. Features may reference other features.
6. Geometry must use standard GIS concepts.
7. Runtime objects are not authoritative.
8. Chunks index features but do not own them.
9. Features must support validation.
10. Future feature types must remain compatible with this architecture.

---

# Key Mental Model

Do not think:

```text
Feature
=
Game Object
```

Think:

```text
Feature
=
GIS Entity
```

and

```text
Runtime Object
=
Temporary Representation
```