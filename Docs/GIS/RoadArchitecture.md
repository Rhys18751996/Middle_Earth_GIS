# RoadArchitecture.md

## Purpose

Define the authoritative road architecture used by Middle_Earth_GIS.

Roads represent transportation networks within the world.

Roads are stored as GIS features and remain independent of:

- Terrain
- Regions
- Settlements
- Runtime systems
- Rendering systems

Roads are authoritative world data.

---

# Core Principle

Roads are GIS spline features.

A road is a logical transportation route.

A road is not:

- Terrain geometry
- Mesh geometry
- Runtime objects
- Unity objects

Roads exist independently of any engine implementation.

---

# Geometry Model

Roads use polyline geometry.

Conceptually:

```text
Point
    ↓
Point
    ↓
Point
    ↓
Point
```

A road consists of an ordered sequence of control points.

Road geometry may contain any number of points.

---

# Road Structure

Conceptually:

```text
Road
├── RoadId
├── Name
├── Geometry
├── Attributes
└── References
```

Example:

```text
Great East Road
```

is a single road feature regardless of length.

---

# Road Identity

Every road must have a unique identifier.

Example:

```text
ROAD_0001
ROAD_0002
ROAD_0003
```

Road identifiers should remain stable.

Names may change.

Identifiers should not.

---

# Road Geometry

Road geometry is stored using world coordinates.

Example:

```text
(1000,1000)
    ↓
(2000,1050)
    ↓
(3000,1200)
```

All coordinates use the global coordinate system.

Roads must not define their own coordinate space.

---

# Road Attributes

Roads may contain attributes describing the route.

Examples:

```text
Name
RoadClass
Surface
Width
Condition
TrafficLevel
Owner
HistoricalPeriod
```

Attributes describe the road.

Attributes do not define geometry.

---

# Road Classes

Examples:

```text
MajorRoad
MinorRoad
Trail
Path
Bridge
Causeway
MountainPass
```

Projects may extend classifications as needed.

---

# Road Width

Road width is metadata.

Example:

```text
2m
4m
8m
12m
```

Road width does not alter the underlying spline geometry.

Runtime systems may use width when generating meshes.

---

# Road Surface Types

Examples:

```text
Dirt
Gravel
Stone
Cobblestone
Wood
Paved
```

Surface information is stored as attributes.

---

# Road Segmentation

A road remains a single feature regardless of length.

Example:

```text
Great East Road
```

may cross:

```text
Hundreds Of Chunks
Multiple Regions
Multiple Terrain Datasets
```

The road remains one logical feature.

Chunk boundaries must never split road ownership.

---

# Terrain Relationship

Roads are independent of terrain.

Terrain provides elevation.

Roads provide transportation geometry.

Examples:

```text
Moving Terrain
```

does not modify:

```text
Road Geometry
```

and

```text
Moving Road Geometry
```

does not modify:

```text
Terrain Dataset
```

Runtime systems may project roads onto terrain.

The datasets remain independent.

---

# Settlement Relationship

Roads may connect settlements.

Example:

```text
Hobbiton
    ↓
Bree
    ↓
Fornost
```

Settlements do not own roads.

Roads do not own settlements.

Relationships are expressed through references.

---

# Region Relationship

Roads may cross regions.

Example:

```text
Arnor
    ↓
The Shire
    ↓
Bree-land
```

Region boundaries do not alter road geometry.

Road ownership remains unchanged.

---

# Chunk Relationship

Roads may cross any number of chunks.

Example:

```text
Great East Road
```

may cross:

```text
500 Chunks
```

Chunks index roads.

Chunks do not own roads.

---

# Runtime Representation

Runtime systems may generate:

- Road meshes
- Navigation networks
- Pathfinding graphs
- Physics geometry

Example:

```text
Road Feature
    ↓
Runtime Generation
    ↓
Road Mesh
```

Runtime objects are temporary.

Road features remain authoritative.

---

# Validation

Road datasets should support validation.

Examples:

- Valid geometry
- Valid coordinates
- Valid references
- Connected topology
- Required attributes

Invalid roads should not become authoritative.

---

# Serialization

Roads must support persistence.

Examples:

```text
JSON
GeoJSON
Binary Formats
Future Formats
```

Serialization format is independent of road architecture.

---

# Future Support

The architecture must support:

- Footpaths
- Trade routes
- Roman-style roads
- Bridges
- Tunnels
- Multi-world support
- Vertical worlds

Without redesigning the road model.

---

# Rules

1. Roads are spline GIS features.
2. Roads use the global coordinate system.
3. Roads remain independent of terrain.
4. Roads remain independent of regions.
5. Roads remain independent of settlements.
6. Roads may cross any number of chunks.
7. Chunk boundaries must not alter road geometry.
8. Runtime objects are not authoritative.
9. Road identifiers must remain stable.
10. Roads must support validation and serialization.

---

# Key Mental Model

Do not think:

```text
Road
=
Road Mesh
```

Think:

```text
Road
=
Transportation GIS Feature
```

and

```text
Road Mesh
=
Runtime Representation
```