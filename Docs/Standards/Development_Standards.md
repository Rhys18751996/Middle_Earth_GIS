# Development_Standards.md

## Purpose

This document defines the development standards used throughout the Middle_Earth_GIS project.

These standards ensure consistency across source code, datasets, documentation, tooling, validation systems, and future community contributions.

All contributors should follow these standards unless a documented architectural decision explicitly overrides them.

---

# Core Principles

## Data First

World data is the authoritative source of truth.

Rendering systems, editors, runtime systems, and export systems consume data rather than becoming the source of truth.

Examples:

- Terrain data exists independently of Unity.
- Roads exist independently of terrain meshes.
- Regions exist independently of map rendering.

---

## Engine Independent

Middle_Earth_GIS is not a Unity project.

Unity is the first implementation.

All architecture decisions should consider future implementations including:

- Unity
- Unreal
- Godot
- Web
- Headless tools

### Unity Version
This is the Unity version I used for the first implementation

Unity 6 LTS
Version: 6.4 (6000.4.9f1)

---

## GIS Inspired

Where practical, the project should adopt proven GIS concepts rather than inventing custom alternatives.

Examples:

- Layer separation
- Feature datasets
- Attribute systems
- Coordinate systems
- Geometry validation

---

## Open Source Friendly

All formats, schemas, and workflows must be documented.

Contributors should not need to reverse engineer systems.

---

## Separation of Concerns

Datasets must remain independent.

Examples:

- Roads do not own terrain.
- Regions do not own settlements.
- Terrain does not own vegetation.

Relationships should be expressed through references rather than coupling.

---

# Git Standards

# Branch Strategy

## Main Branches

```text
main
develop
```

---

## Platform Development

```text
feature/<name>
```

Examples:

```text
feature/chunk-loader
feature/terrain-import
feature/atlas-generator
feature/road-editor
```

---

## Bug Fixes

```text
bugfix/<name>
```

Examples:

```text
bugfix/chunk-coordinate-error
bugfix/terrain-save-failure
```

---

## Documentation

```text
docs/<name>
```

Examples:

```text
docs/coordinate-system
docs/chunk-architecture
```

---

## World Data Contributions

**Future Consideration**
 As the project grows and transitions to community-driven development, this branch strategy may evolve.
 The current approach categorizes branches by contribution type (feature, bugfix, docs, terrain, roads, etc.) because the project is still in its early stages.
 In the future, contributions may instead be organized around GitHub Issue IDs, with issues becoming the authoritative unit of work.
 Example:
 ```text
 feature/101-chunk-100-050
 feature/102-brandywine-river
 feature/103-hobbiton-settlement
 feature/104-fangorn-vegetation
 ```
 Where:
 - `101` is the GitHub Issue ID.
 - The remaining text is a short human-readable description of the work being performed.
This approach scales better for large collaborative projects because a single contribution may involve multiple datasets, files, or systems that would not fit cleanly into a dataset-specific branch structure.

The final branching strategy will be reviewed during Phase 6 – Community Edition.

Terrain:

```text
terrain/<chunk-id>
```

Examples:

```text
terrain/chunk-100-050
terrain/chunk-101-050
```

Roads:

```text
roads/<name>
```

Examples:

```text
roads/great-east-road
roads/north-south-road
```

Rivers:

```text
rivers/<name>
```

Examples:

```text
rivers/brandywine
rivers/anduin
```

Settlements:

```text
settlements/<name>
```

Examples:

```text
settlements/hobbiton
settlements/minas-tirith
```

Regions:

```text
regions/<name>
```

Examples:

```text
regions/the-shire
regions/gondor
```

Vegetation:

```text
vegetation/<area>
```

Examples:

```text
vegetation/fangorn
vegetation/mirkwood
```

Structures:

```text
structures/<name>
```

Examples:

```text
structures/orthanc
structures/weathertop
```

---

## Commit Message Standards

Format:

```text
[type] Description
```

Examples:

```text
[feat] Added terrain chunk schema
[fix] Corrected chunk coordinate conversion
[docs] Added coordinate system specification
[test] Added terrain validation tests
[refactor] Simplified terrain loader
[chore] Updated dependencies
```

Allowed types:

```text
feat
fix
docs
test
refactor
chore
```

---

# Naming Standards

## General Rule

Use PascalCase.

Examples:

```text
TerrainChunk
ChunkLoader
CoordinateSystem
WorldDatabase
```

Avoid:

```text
terrainchunk
terrain_chunk
terrainChunk
```

---

## Documentation Files

Use PascalCase.

Examples:

```text
CoordinateSystem.md
ChunkSystem.md
TerrainDataSystem.md
WorldDatabaseArchitecture.md
```

---

## C# Files

Examples:

```text
ChunkLoader.cs
TerrainChunk.cs
TerrainManager.cs
WorldDatabase.cs
```

---

## Folder Names

Examples:

```text
Docs
Data
Tools
Validation
Exports
Samples
```

Unity folders:

```text
Assets/Scripts/Core
Assets/Scripts/GIS
Assets/Scripts/Terrain
Assets/Scripts/UI
Assets/GIS
Assets/Data
Assets/Streaming
```

---

# Coordinate Standards

## Coordinate Axes

The platform uses:

```text
X = East-West
Y = North-South
Z = Elevation
```

---

## World Origin

World origin is located at:

```text
0,0
```

Representing:

```text
South-West corner of the world
```

All coordinates are expressed relative to this origin.

---

## Units

World units are:

```text
1 Unit = 1 Meter
```

This rule applies to:

- Terrain
- Roads
- Rivers
- Structures
- Settlements
- Runtime systems

No alternative scale systems should be introduced.

---

# JSON Standards

## Property Naming

All JSON properties use PascalCase.

Example:

```json
{
  "FeatureType": "Settlement",
  "SettlementId": "SETTLEMENT_00001"
}
```

---

## IDs

IDs are strings.

Examples:

```text
TERRAIN_00001
ROAD_00001
RIVER_00001
SETTLEMENT_00001
REGION_00001
```

---

## Attributes Object

Extensible data should be placed within:

```json
{
  "Attributes": {}
}
```

Whenever possible, avoid modifying core schema structures.

---

## Required Metadata

Every GIS dataset should include:

```json
{
  "FeatureType": "",
  "Attributes": {}
}
```

Additional fields may be added depending on dataset type.

---

# Documentation Standards

## Architecture First

New systems should be documented before implementation whenever practical.

---

## Examples Required

Documentation should include examples.

Examples may include:

- JSON
- Diagrams
- Coordinate examples
- Workflow examples

---

## Living Documentation

Documentation should evolve alongside the platform.

Outdated documentation should be updated or removed.

---

# Validation Standards

Validation should be automated whenever possible.

Validation systems should verify:

- Coordinate validity
- Geometry validity
- Schema compliance
- Reference integrity
- Dataset completeness

Validation failures should be reported clearly.

---

# Future Community Standards

All future contributions should:

- Follow naming standards
- Follow coordinate standards
- Follow schema standards
- Pass validation checks
- Include documentation where appropriate

No contribution should become authoritative without review and validation.

---

# Compliance

All project code, documentation, schemas, datasets, and tools should comply with this document unless a documented architectural decision explicitly states otherwise.