# AtlasArchitecture.md

## Purpose

Define the architecture used to generate maps and atlases from GIS datasets.

Atlases are generated products.

Atlases are not authoritative data.

---

# Core Principle

```text
GIS Data
    ↓
Atlas Generation
    ↓
Map
```

Maps are views of data.

Not the data itself.

---

# Atlas Types

Examples:

```text
Political Maps
Road Maps
River Maps
Terrain Maps
Topographic Maps
Biome Maps
Settlement Maps
Historical Maps
```

Future atlas types may be added without modifying source datasets.

---

# Data Sources

Atlas generation consumes datasets.

Examples:

```text
Terrain Dataset
Road Dataset
River Dataset
Settlement Dataset
Region Dataset
```

Datasets remain authoritative.

---

# Layer Composition

Atlas layers may be combined.

Example:

```text
Terrain
    +
Roads
    +
Settlements
```

to generate:

```text
Travel Map
```

---

# Rendering Independence

Atlas generation must remain independent of:

- Unity
- Runtime systems
- Storage systems

Atlas generation is a GIS process.

---

# Output Formats

Examples:

```text
PNG
JPEG
PDF
SVG
GeoPDF
Future Formats
```

---

# Rules

1. Atlases are generated products.
2. Datasets remain authoritative.
3. Maps do not become source data.
4. Atlas generation remains engine-independent.
5. Atlas generation consumes datasets.

---

# Key Mental Model

Do not think:

```text
Map
=
World Data
```

Think:

```text
Map
=
Generated View
```