# VerticalWorlds.md

## Purpose

Define the architecture used to support multiple overlapping world layers.

Vertical worlds allow multiple spaces to occupy the same coordinates.

Examples:

- Surface World
- Underground Worlds
- Interior Spaces
- Future Dimensions

---

# Core Principle

Location is defined by:

```text
Coordinate
+
Layer
```

Not:

```text
Coordinate
```

alone.

---

# World Layers

Examples:

```text
Surface

Moria

BagEndInterior

MinasTirithInterior
```

Each layer occupies the same coordinate system.

---

# Shared Coordinates

Example:

```text
Surface
(125000, 752000)

Moria
(125000, 752000)

BagEndInterior
(125000, 752000)
```

Same coordinates.

Different layers.

Different locations.

---

# Layer Identity

Every layer must have a unique identifier.

Example:

```text
LAYER_SURFACE
LAYER_MORIA
LAYER_BAG_END_INTERIOR
```

---

# Dataset Relationship

Datasets belong to layers.

Example:

```text
Surface Terrain
    → Surface Layer

Moria Terrain
    → Moria Layer
```

Features inherit the layer of their dataset.

---

# Chunk Relationship

Chunks remain unchanged.

Example:

```text
Chunk_P100_P050
```

may exist in:

```text
Surface

Moria

BagEndInterior
```

Chunk coordinates are shared.

Layer identifiers distinguish worlds.

---

# Streaming

Streaming systems load data by:

```text
Coordinate
+
Layer
```

The active layer determines what data is visible.

---

# Future Support

The architecture must support:

- Interiors
- Underground worlds
- Multiple dimensions
- Multiple planets
- Community-created layers

without changing coordinates.

---

# Rules

1. Layers share coordinates.
2. Layers have unique identifiers.
3. Datasets belong to layers.
4. Chunks remain unchanged.
5. Coordinate systems remain unchanged.
6. Layer identifiers distinguish overlapping worlds.

---

# Key Mental Model

Do not think:

```text
Coordinate
=
Location
```

Think:

```text
Coordinate
+
Layer
=
Location
```