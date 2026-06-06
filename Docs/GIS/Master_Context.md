# Middle_Earth_GIS Master Context

> Project Vision, Core Architecture, and Development Status

---

# Project Metadata

- Project Name: Middle_Earth_GIS
- Project Type: Open-Source GIS-Inspired World Platform
- First Engine: Unity
- First Unity Implementation: Fantasy_World_GIS
- Long-Term Goal: Engine-Independent World Platform

---

# Vision

Middle_Earth_GIS aims to create a complete, data-driven representation of Middle-earth that exists independently of any game engine.

The project is not primarily a game.

Middle-earth is the first world dataset built on the platform rather than the platform itself.

The platform should support:

- Interactive maps
- Atlas generation
- GIS analysis
- World editing
- Runtime exploration
- VR experiences
- Historical simulations
- RPG tools
- Future games
- User-created worlds

## Core Philosophy

> Build the world as data first.
>
> Build experiences on top of that data later.

---

# Core Principles

## Data First

World data exists independently of rendering.

## Engine Independent

Unity is an implementation, not a dependency.

## GIS Inspired

Use proven GIS concepts where practical.

## Streamable

Large worlds must load incrementally.

## Editable

All world data should remain editable.

## Extensible

New systems should be addable without redesigning the platform.

---

# World Architecture

## Coordinate System

The platform uses a single global coordinate system.

```text
Origin = South-West Corner of the World
```

All datasets share this coordinate system.

No subsystem may create its own persistent coordinate space.

---

## Chunk System

Chunks are fixed-size world indexing cells.

```text
Chunk Size = 256m × 256m
```

Chunks are responsible for:

- Spatial indexing
- Dataset discovery
- Streaming
- Caching
- Runtime lookup

Chunks are NOT:

- Terrain datasets
- Terrain resolution
- Political boundaries
- Gameplay boundaries

### Core Principle

```text
Chunk Grid
=
World Indexing System
```

---

## Terrain Architecture

Terrain is stored as overlapping GIS raster datasets.

Examples:

```text
MiddleEarth_256m
MiddleEarth_128m
MiddleEarth_64m
MiddleEarth_32m
Shire_16m
Hobbiton_4m
BagEnd_1m
```

Datasets may overlap.

Runtime systems always select:

```text
Highest Resolution Available
```

---

## Terrain Resolution Hierarchy

Terrain datasets use powers-of-two resolutions.

```text
256m
128m
64m
32m
16m
8m
4m
2m
1m
```

Higher-resolution datasets are authoritative.

Lower-resolution datasets are generated products.

### Core Principle

```text
Highest Resolution Available
=
Authoritative Truth

Lower Resolution Datasets
=
Generated Products
```

---

## GIS Data Types

| Dataset | Geometry |
|----------|----------|
| Terrain | Raster |
| Roads | Splines |
| Rivers | Splines |
| Settlements | Points |
| Regions | Polygons |
| Structures | Polygons |
| Vegetation | Points / Polygons |
| World Objects | Points |
| Linear Objects | Splines |

---

# Authoritative Data Sources

| Data Type | Authoritative Source |
|------------|---------------------|
| Terrain | Highest Resolution Dataset Available |
| Roads | Road Dataset |
| Rivers | River Dataset |
| Regions | Region Dataset |
| Settlements | Settlement Dataset |
| Structures | Structure Dataset |
| Vegetation | Vegetation Dataset |

Unity scenes are never authoritative.

Runtime worlds are generated from datasets.

---

# Planned Platform Capabilities

- Terrain management
- GIS datasets
- Terrain editing
- Validation tools
- Atlas generation
- Runtime exploration
- Vertical worlds
- Community-created worlds
- Multi-engine support
- Open-source ecosystem

---

# Development Roadmap

## Phase 0

Foundation Architecture

- Coordinate System
- Chunk System
- Terrain Schema
- Terrain Loading
- Terrain Saving
- Chunk Streaming

## Phase 1

GIS Core

- Features
- Datasets
- Registries
- Queries
- Validation
- Serialization

## Phase 2

Terrain Technology

- Importing
- Editing
- Persistence
- Exporting

## Phase 3

World Database

- Terrain
- Roads
- Rivers
- Regions
- Settlements
- Vegetation

## Phase 4

World Editor

## Phase 5

Vertical Worlds

## Phase 6

Community Edition

## Phase 7

Atlas Generation

## Phase 8

Runtime World

## Phase 9

Living World

## Phase 10

Beyond Middle-earth

---

# Current Development Focus

Current priorities:

1. Coordinate System
2. Chunk System
3. Terrain Schema
4. Terrain Loading
5. Terrain Saving
6. Chunk Streaming
7. Basic Editing Tools

Not currently in scope:

- NPC simulation
- Economy simulation
- Quest generation
- Living-world systems

These systems should only be developed after the GIS foundation is proven.

---

# Repository Structure

```text
/
├── Docs
├── Data
├── Tools
├── UnityProject
├── Validation
├── Samples
└── Exports
```

---

# Referenced Documents

Detailed architecture specifications are maintained separately.

```text
CoordinateSystem.md
ChunkSystem.md
TerrainArchitecture.md
DatasetArchitecture.md
GISFeatureArchitecture.md
RegionArchitecture.md
VerticalWorlds.md
AtlasArchitecture.md
DevelopmentRoadmap.md
```

---

# Current Status

The project is transitioning from Foundation Architecture into the GIS Core phase.

The following architectural decisions are established:

- Global coordinate system
- Fixed 256m chunk grid
- Multi-resolution terrain architecture
- Powers-of-two terrain hierarchy
- Highest-resolution-wins authority model
- Engine-independent GIS foundation

Current work is focused on building reusable GIS systems before higher-level gameplay or simulation systems.

---

# World Scale

Estimated Middle-earth dimensions:

```text
Width  = 5,760 km
Height = 4,320 km
```

The architecture should remain scalable beyond Middle-earth and support future worlds of arbitrary size.