# Middle_Earth_GIS_Master_Context

> Master Context, Project Bible, Architecture Reference, and Development Roadmap

---

# Project Metadata

- Project Name: Middle_Earth_GIS
- Project Type: Open-Source Geospatial World Platform
- First Implementation Engine: Unity
- Long-Term Goal: Engine Independent Platform
- Current Status: Pre-Production
- Current Phase: Phase -1 Project Setup

---

# Vision

The goal of Middle_Earth_GIS is to create a complete, data-driven representation of Middle-earth that exists independently of any game engine.

The project is not primarily a game.

It is a reusable world platform capable of supporting:

- Interactive maps
- GIS analysis
- Atlas generation
- World editors
- VR experiences
- Historical simulations
- RPG tools
- Community world building
- Future game projects

Core philosophy:

Build the world as data first. Build experiences on top of that data later.

---

# Project Description

Middle_Earth_GIS is a GIS-inspired world platform built around structured world data.

The system stores:

- Terrain
- Vegetation
- Rivers
- Roads
- Settlements
- Political Regions
- Underground Areas
- Metadata
- see Planned GIS Datasets

All world information should exist as structured data before it exists as rendered content.

Unity is the initial implementation platform because it provides a fast development environment and mature editor tooling.

However:

- Unity is not the platform.
- Middle-earth is not the platform.

The platform is the data architecture.

---

# Finished Product Description

The completed system will allow users to:

- Explore Middle-earth in real time
- Generate atlas maps
- Generate battle maps
- Create world editors
- Build simulations
- Create VR experiences
- Export to multiple engines
- Build entirely new worlds

Middle-earth should become the first dataset built on the platform rather than the platform itself.

---

# Data Architecture

World data is stored using the geometry most appropriate for the data type.

Terrain:
    Chunked raster data stored in fixed-size square chunks

Political Regions:
    Polygons

Roads:
    Splines

Rivers:
    Splines

Settlements:
    Points

All layers share the same world coordinate system.

---

# Planned GIS Datasets

Core Datasets

- Terrain
- Vegetation
- Hydrology
- Roads
- Settlements
- Political Regions

Future Datasets

- Biomes
- Climate
- Trade Routes
- NPC Populations
- Economy
- Historical Events
- Underground Networks

---

# World Coordinate System

The entire platform uses a single global coordinate system.

World Origin:
0,0 = South-West Corner of the World

All datasets must align to this coordinate system.

No subsystem may create its own local coordinate space for persistent world data.

Examples:

Terrain Chunks:
Chunk_100_50

Road Nodes:
125000, 752000

Settlement:
Hobbiton
123456, 789012

Political Borders:
Polygon coordinates in world space

---

# Terrain Source Architecture

Terrain source files are not the authoritative terrain representation.

Terrain source files are import/export formats.

Examples:

- GeoTIFF
- TIFF
- RAW Heightmaps
- PNG Heightmaps

Workflow:

Source Terrain
    ↓
Import
    ↓
Chunk Database
    ↓
Editing
    ↓
Export
    ↓
Updated Terrain Sources

The chunk database is the authoritative terrain representation during development.

---

# Chunk Architecture

Chunks are storage and streaming units.

Chunks are not regions.

Chunks are not political boundaries.

Chunks are fixed-size square areas of world space.

Examples:

Chunk_100_50
Chunk_101_50
Chunk_102_50

Responsibilities:

- Terrain storage
- Terrain streaming
- Terrain editing
- Terrain saving

Chunks should remain independent of:

- Kingdom boundaries
- Regions
- Roads
- Rivers
- Settlements

---

# Region Architecture

Regions are not terrain files.

Regions are independent GIS datasets.

Terrain:
Defines elevation.

Regions:
Define political or logical boundaries.

Example:

Terrain:
The_Shire.tif

Region:
Shire.geojson

Regions may be irregularly shaped.
Terrain files may be rectangular. The shape of a region must never depend on the shape of a terrain file.

---

# Core Design Principles

## Data First

Data exists independently of rendering.

## Engine Independent

Unity is an implementation, not a dependency.

## Open Source Friendly

All formats must be documented.

## GIS Inspired

Use proven GIS concepts where practical.

## Streamable

The world must load incrementally.

## Editable

Every piece of data should be editable.

## Scalable

The architecture should support worlds larger than Middle-earth.

## Extensible

Future systems should be addable without redesigning existing systems.

---

## Separation of Concerns

Terrain, regions, roads, rivers, settlements, and runtime systems must remain independent layers that communicate through shared world coordinates.

Layers must be independently editable.

Modifying one layer must not require modifying another layer.

Examples:

- Moving a road does not modify terrain.
- Changing a political border does not modify terrain.
- Adding a settlement does not modify road data.

Relationships between layers should be expressed through references rather than direct coupling.

---

# Authoritative Data Sources

Terrain:
Chunk Database

Political Regions:
Region Files

Roads:
Road Network Dataset

Settlements:
Settlement Dataset

Runtime Scene:
Generated from datasets

Unity scenes must never become the authoritative source of world data.

---

# Development Philosophy

This document serves two purposes simultaneously:

1. Roadmap for the platform.
2. Roadmap for building the platform.

Every phase must therefore contain:

- Architectural goals
- Development goals
- Deliverables
- Outputs
- Implementation steps

---

# High Level Architecture

World
├── Regions
├── Chunks
│   ├── Terrain
│   ├── Vegetation
│   ├── Roads
│   ├── Rivers
│   ├── Settlements
│   └── Metadata
├── Runtime Systems
├── Editing Tools
├── Validation Tools
└── Atlas Generation Systems

---

# Repository Structure (Target)

/
├── Docs
├── Data
├── Tools
├── UnityProject
├── Validation
├── Samples
└── Exports

---

# Phase -1 – Project Setup

## Goal

Create the development environment, repository structure, documentation system, 
and Unity implementation required to build the Middle_Earth_GIS platform.

## Success Criteria

- GitHub repository exists
- Documentation structure exists
- Unity project exists
- Unity project is connected to source control
- Initial commits completed
- Development standards documented
- Project can be cloned and opened successfully on another machine

---

## Step -1.1 Create Git Repository

### Purpose

Establish source control before development begins.

### Substeps

- Create GitHub repository
- Choose repository visibility (public/private)
- Create README.md
- Create LICENSE
- Create .gitignore using Unity template
- Clone repository locally
- Verify repository structure
- Push initial commit

### Deliverables

- Repository online
- Source control functional
- Initial commit completed

---

## Step -1.2 Create Documentation Structure

### Purpose

Create a central location for all project documentation and architectural decisions.

### Substeps

- Create Docs folder
- Create Docs/Architecture folder
- Create Docs/GIS folder
- Create Docs/Standards folder
- Create Docs/Decisions folder
- Move Middle_Earth_GIS_Master_Context.md into Docs

----------------------------------------------------------------------------------------------------------------------------------------------------------
 <---- We are here - Development stage
----------------------------------------------------------------------------------------------------------------------------------------------------------

- Create placeholder documentation files where appropriate

### Deliverables

- Organized documentation system
- Documentation folder structure established

---

## Step -1.3 Create Unity Project

### Purpose

Create the first implementation environment for the platform.

### Substeps

- Install Unity Hub
- Select Unity LTS version
- Create Unity project
- Verify project launches successfully
- Configure project settings
- Close and reopen project to verify stability

### Deliverables

- Working Unity project
- Verified Unity version

---

## Step -1.4 Connect Unity Project to Repository

### Purpose

Ensure Unity project files are tracked correctly in Git.

### Substeps

- Place Unity project inside repository structure
- Verify Unity .gitignore configuration
- Confirm Library folder is ignored
- Confirm Temp folder is ignored
- Confirm Logs folder is ignored
- Confirm UserSettings folder is ignored
- Commit Unity project files

### Files to Commit

- Assets/
- Packages/
- ProjectSettings/
- Documentation
- Schemas
- Tools
- Sample data

### Files to Ignore

- Library/
- Temp/
- Logs/
- Obj/
- Builds/
- UserSettings/

### Deliverables

- Unity project under source control
- Clean Git repository
- Correct ignore rules

---

## Step -1.5 Configure Unity Architecture

### Purpose

Create the initial Unity folder structure that mirrors the platform architecture.

### Substeps

- Create Assets/Scripts
- Create Assets/GIS
- Create Assets/Terrain
- Create Assets/Data
- Create Assets/Streaming
- Create Assets/Editor
- Create Assets/Tests
- Create Assets/Scenes
- Create Assets/Materials
- Create Assets/Prefabs

### Deliverables

- Organized Unity architecture
- Consistent project structure

---

## Step -1.6 Create Test Scene

### Purpose

Verify that the Unity environment is functioning correctly.

### Substeps

- Create startup scene
- Create camera
- Create directional light
- Create test object
- Create startup script
- Verify scene loads
- Verify scripts compile
- Commit test scene

### Deliverables

- Functional test scene
- Verified development environment

---

## Step -1.7 Establish Development Standards

### Purpose

Create standards that all future development must follow.

### Substeps

- Define Git branch strategy
- Define commit message standards
- Define naming conventions
- Define folder standards
- Define documentation standards
- Define JSON standards
- Define coordinate standards
- Create Development_Standards.md
- Store Development_Standards.md in Docs/Standards

### Deliverables

- Development_Standards.md
- Established project standards

---

## Phase Completion Criteria

Phase -1 is complete when:

- Repository is online
- Documentation structure exists
- Unity project is under source control
- Test scene runs successfully
- Development standards are documented
- Another developer could clone the repository and begin work immediately

# Phase 0 – Foundation Architecture

## Goal

Create the core architecture required for all future development.

## Success Criteria

- Terrain loads
- Terrain saves
- Chunks stream
- Architecture documented

## Step 0.1 Define World Coordinate System

Substeps:

- Research GIS coordinate systems
- Define world units
- Evaluate origins
- Select south-west origin
- Define coordinate conventions
- Create examples
- Create CoordinateSystem.md

Deliverables:

- Coordinate standard

## Step 0.2 Define Chunk System

Substeps:

- Define chunk dimensions
- Define chunk coordinates
- Define chunk naming convention
- Define chunk metadata
- Create examples
- Create ChunkSystem.md

Deliverables:

- Chunk specification

## Step 0.3 Define Terrain Data Format

Substeps:

- Create schema
- Define bounds
- Define metadata
- Define heightmap references
- Validate examples
- Create TerrainDataSystem.md

Deliverables:

- Terrain schema

## Step 0.4 Build Terrain Loader

Substeps:

- Create loader architecture
- Read JSON files
- Load heightmaps
- Generate meshes
- Test loading

Deliverables:

- Terrain loader

## Step 0.5 Build Terrain Saving

Substeps:

- Serialize terrain
- Save modifications
- Reload modifications
- Verify persistence

Deliverables:

- Terrain save system

## Step 0.6 Build Chunk Streaming

Substeps:

- Detect nearby chunks
- Load chunks
- Unload chunks
- Test memory usage
- Optimize performance

Deliverables:

- Streaming system

---

# Phase 1 – Core GIS Engine

## Goal

Create the reusable GIS foundation.

## Steps

- World database architecture
- Data registries
- Asset registries
- Metadata systems
- Validation systems

Deliverables:

- Engine-independent GIS core

---

# Phase 2 – Terrain Technology

## Goal

Build terrain technology.

## Steps

Step 2.1 Terrain Representation

- Heightmaps
- Terrain meshes
- Chunk terrain data

Step 2.2 Terrain Importing

- Import TIFF heightmaps
- Import RAW heightmaps
- Convert to chunk data

Step 2.3 Terrain Editing

- Raise terrain
- Lower terrain
- Smooth terrain

Step 2.4 Terrain Surface Painting

- Grass
- Rock
- Snow
- Sand

Step 2.5 Terrain Persistence

- Save edits
- Reload edits
- Version terrain data

Step 2.6 Terrain Export

- Export chunk terrain
- Stitch chunks together
- Generate global heightmap
- Export GeoTIFF
- Export RAW
- Export PNG heightmap
- Validate output

Deliverables:

- Editable terrain system
- MiddleEarth_v2.tif
- MiddleEarth_v2.raw
- MiddleEarth_v2.png


---

# Phase 3 – World Database

## Goal

Convert Middle-earth into structured GIS datasets.

## Steps

### Terrain Layer
### Vegetation Layer
### Hydrology Layer
### Road Layer
### Political Layer
### Settlement Layer

Deliverables:

- Data-driven Middle-earth

---

# Phase 4 – World Editor

## Goal

Create powerful editor tooling.

## Steps

- Terrain Sculpting
- Terrain Painting
- Road Editor
- River Editor
- Settlement Editor
- Metadata Editor

Deliverables:

- World creation toolkit

---

# Phase 5 – Vertical Worlds

## Goal

Support caves, dungeons and underground worlds.

## Steps

- Surface Layer
- Interior Layer
- Portal System
- Multi-Level Areas

Deliverables:

- Moria-capable architecture

---

# Phase 6 – Community Edition

## Goal

Enable open-source collaboration.

## Steps

- Data standards
- Contributor guides
- Git workflows
- Validation systems

Deliverables:

- Community-ready project

---

# Phase 7 – Atlas Generation

## Goal

Generate maps directly from world data.

## Steps

- Topographic maps
- Political maps
- Travel maps
- Battle maps

Deliverables:

- Atlas generation pipeline

---

# Phase 8 – Runtime World

## Goal

Transform GIS data into a live explorable world.

## Steps

- Runtime streaming
- Runtime LOD
- Runtime optimization

Deliverables:

- Seamless world traversal

---

# Phase 9 – Living World

## Goal

Add simulation systems.

## Steps

- NPC Layer
- Economy Layer
- Quest Layer

Deliverables:

- Dynamic simulation

---

# Phase 10 – Beyond Middle-earth

## Goal

Transform the project into a reusable platform.

## Steps

- Generic world support
- Engine independence
- Open ecosystem
- Plugin architecture
- Data packs

Deliverables:

- Universal world platform

---

# Immediate Development Focus

Only work on:

1. Git repository
2. Documentation structure
3. Unity project
4. Coordinate system
5. Chunk system
6. Terrain schema
7. Terrain loading
8. Terrain saving
9. Chunk streaming
10. Basic editing tools

Do not build:

- NPCs
- Economy systems
- Quests
- Simulations

until the foundation architecture is proven.

---

# Current Progress Tracker

## Phase -1

- [ ] Create Git Repository
- [ ] Create Documentation Structure
- [ ] Create Unity Project
- [ ] Configure Unity Architecture
- [ ] Create Test Scene
- [ ] Establish Development Standards

## Phase 0

- [ ] Define Coordinate System
- [ ] Define Chunk System
- [ ] Define Terrain Schema
- [ ] Terrain Loader
- [ ] Terrain Saving
- [ ] Chunk Streaming

---

# Current Session Context

Current Focus:
Phase -1 – Project Setup

Current Step:
Create Git Repository

Next Action:
Create GitHub repository and initial project structure.

Notes:

- Unity is the first implementation.
- Data must remain engine independent.
- 1 Unit = 1 Metre.
- Recommended Origin = South-West Corner.
- Middle-earth is the first world, not the platform.
