# DatasetArchitecture.md

## Purpose

Define the authoritative dataset architecture used by Middle_Earth_GIS.

Datasets are the primary organizational unit of world data.

A dataset groups related GIS features into a single logical collection.

Examples:

- Terrain Dataset
- Road Dataset
- River Dataset
- Settlement Dataset
- Region Dataset
- Vegetation Dataset
- Structure Dataset

All GIS data exists within datasets.

---

# Core Principle

Features do not exist independently.

Features belong to datasets.

```text
World
    └── Datasets
            └── Features
```

Examples:

```text
Road Dataset
    ├── Great East Road
    ├── Greenway
    └── East-West Road
```

```text
Settlement Dataset
    ├── Hobbiton
    ├── Bree
    └── Minas Tirith
```

Datasets are the primary container for GIS data.

---

# Dataset Responsibilities

Datasets are responsible for:

- Organizing features
- Feature discovery
- Metadata
- Versioning
- Ownership
- Validation
- Serialization
- Querying

Datasets are NOT responsible for:

- Rendering
- Runtime objects
- Streaming implementation
- Editor implementation

---

# Dataset Types

Common dataset types include:

```text
Terrain
Roads
Rivers
Settlements
Regions
Vegetation
Structures
WorldObjects
LinearObjects
Portals
```

Future dataset types may be added without modifying existing datasets.

---

# Dataset Structure

A dataset contains:

- Dataset metadata
- Feature collection
- References
- Attributes

Conceptually:

```text
Dataset
    ├── Metadata
    ├── Features
    ├── References
    └── Attributes
```

---

# Dataset Identity

Every dataset must have a unique identifier.

Example:

```text
MiddleEarth_128m
Roads_Main
Settlements_Shire
Regions_Gondor
```

Dataset identifiers should remain stable.

References should use identifiers rather than names.

---

# Dataset Metadata

Datasets may contain metadata such as:

```text
DatasetId
DatasetType
Version
Description
Author
CreatedDate
ModifiedDate
```

Example:

```json
{
  "DatasetId": "Settlements_Shire",
  "DatasetType": "Settlement",
  "Version": 1
}
```

---

# Dataset Features

Datasets contain features.

Examples:

```text
Road Dataset
    └── Road Features

River Dataset
    └── River Features

Settlement Dataset
    └── Settlement Features
```

Feature storage depends on dataset type.

---

# Feature Independence

Features should remain independent objects.

Examples:

```text
Road
```

does not become part of:

```text
Terrain
```

and

```text
Settlement
```

does not become part of:

```text
Region
```

Relationships are expressed through references.

---

# Dataset References

Datasets may reference:

- Other datasets
- Features
- Assets

Examples:

```text
Settlement
    → Region

Structure
    → Asset

Portal
    → Layer
```

References should use identifiers.

Avoid direct object dependencies.

---

# Dataset Queries

Datasets must support querying.

Common query types:

- By identifier
- By bounds
- By geometry
- By attribute
- By feature type

Examples:

```text
Find Settlement By Id

Find Roads In Bounds

Find Rivers Crossing Region
```

Query implementation is engine-independent.

---

# Dataset Validation

Datasets must be valid before use.

Validation may include:

- Schema validation
- Coordinate validation
- Geometry validation
- Reference validation
- Attribute validation

Invalid datasets should not become authoritative.

---

# Dataset Serialization

Datasets must support persistence.

Examples:

```text
JSON
GeoJSON
Binary Formats
Future Formats
```

Serialization format is separate from dataset architecture.

The dataset remains the authoritative model.

---

# Dataset Versioning

Datasets may evolve over time.

Versioning supports:

- Migration
- Compatibility
- Validation
- Distribution

Example:

```text
Version 1
    ↓
Version 2
```

Older versions may be upgraded through migration.

---

# Dataset Registry Relationship

Datasets are discovered through registries.

Example:

```text
Dataset Registry
    ├── Terrain Datasets
    ├── Road Datasets
    ├── River Datasets
    └── Settlement Datasets
```

Systems should discover datasets through registries rather than hardcoded references.

---

# Chunk Relationship

Datasets may span many chunks.

Example:

```text
Great East Road
```

may cross:

```text
500 Chunks
```

The dataset owns the feature.

Chunks merely index it.

---

# Coordinate Relationship

All datasets use the global coordinate system.

Datasets never define their own persistent coordinate space.

Example:

```text
Road
Settlement
River
Region
```

all share the same world coordinates.

---

# Runtime Relationship

Runtime systems consume datasets.

Runtime systems do not own datasets.

Example:

```text
Dataset
    ↓
Runtime Generation
    ↓
Rendered World
```

The dataset remains authoritative.

---

# Future Support

The dataset architecture must support:

- New dataset types
- Multiple worlds
- Multiple engines
- Vertical worlds
- Community datasets
- Distributed storage

Without redesigning the architecture.

---

# Rules

1. All GIS data belongs to a dataset.
2. Every dataset must have a unique identifier.
3. Datasets own features.
4. Features should remain independent objects.
5. Datasets may reference other datasets.
6. Datasets must support validation.
7. Datasets must support serialization.
8. Datasets must use the global coordinate system.
9. Runtime systems must not become authoritative.
10. Future dataset types must remain compatible with this architecture.

---

# Key Mental Model

Do not think:

```text
World
=
Individual Objects
```

Think:

```text
World
=
Datasets
    └── Features
```

and

```text
Datasets
=
Authoritative GIS Data
```