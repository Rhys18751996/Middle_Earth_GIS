# Coordinate System

## Purpose

Define the authoritative world coordinate system used by all datasets within Middle_Earth_GIS.

All terrain, roads, rivers, settlements, structures, regions, and future datasets must use this coordinate system.

---

# Design Goals

- Single global coordinate system
- Engine independent
- GIS inspired
- Consistent across all datasets
- Suitable for large worlds
- Future support for vertical layers

---

# World Origin

The world origin is located at the south-west corner of the world.

```text
Y (North)
▲
│
│
│
└──────────────────────► X (East)
(0,0)
```

Coordinates increase:

- East = +X
- North = +Y

--- 

# Coordinate Units

Unit: Meter

Examples:

```text
1 = 1 meter

100 = 100 meters

1000 = 1 kilometer
```

---

# Elevation

Elevation uses Z.

```text
(X, Y, Z)
```

Examples:

```text
(1000,500,0)

Sea Level

(1000,500,250)

250 meters above sea level
```

---

# Coordinate Precision

Coordinate values should use:

```text
double
```

for storage and calculations.

Reason:

- Large world support
- Future multi-world support
- Reduced precision errors
- Consistent calculations across tools and engines

---

# Dataset Requirements

All datasets must use the same coordinate system.

Examples:

- Terrain
- Roads
- Rivers
- Settlements
- Regions
- Structures
- Portals

---

# Future Layer Support

Future world layers may share identical X,Y coordinates.

Examples:

- Surface
- Moria
- Cave Systems
- Building Interiors

Layer identifiers distinguish overlapping spaces.

Coordinate system remains unchanged.

---

# Example Coordinates

Hobbiton

```text
(123456, 654321, 120)
```

Minas Tirith

```text
(875000, 420000, 350)
```

---

# Rule

No subsystem may create its own persistent coordinate system.

All persistent world data must use the global coordinate system.