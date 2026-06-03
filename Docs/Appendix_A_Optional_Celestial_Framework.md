# Appendix A – Optional Celestial Framework (Future Expansion)

## Purpose

Provide an optional astronomy and celestial mapping system capable of representing stars, planets, moons, constellations, and celestial events.

This system is not required for the core Middle_Earth_GIS platform.

It exists as a future extension for:

- Accurate night skies
- Astronomical simulations
- Navigation systems
- Calendar systems
- Historical sky reconstruction
- Atlas generation
- Future world simulation systems

The celestial framework must remain independent from terrain, GIS layers, and runtime rendering.

---

## Celestial Design Philosophy

Terrain and GIS datasets describe locations on the world.

Celestial datasets describe objects observed from the world.

The two systems operate in different coordinate spaces.

### World GIS

- Cartesian coordinates
- X,Y,Z positions
- Terrain-relative data

### Celestial GIS

- Celestial sphere coordinates
- Right Ascension
- Declination
- Magnitude
- Time-dependent positions

---

## Celestial Representation

The celestial system uses a conceptual celestial sphere surrounding the world.

The sphere is not a physical object.

It is a mathematical reference surface used for locating celestial bodies.

```text
World
  ↓
Observer
  ↓
Celestial Sphere
  ↓
Stars
  ↓
Constellations
  ↓
Planets
```

---

## Celestial Datasets

### Stars

Representation: Points

```json
{
  "FeatureType": "Star",
  "StarId": "STAR_00001",
  "Name": "Eärendil",
  "RightAscension": 124.56,
  "Declination": 45.78,
  "Magnitude": 1.2,
  "Color": [255, 240, 220],
  "SpectralClass": "A",
  "Attributes": {}
}
```

### Constellations

Representation: Connected Points

```json
{
  "FeatureType": "Constellation",
  "Name": "Valacirca",
  "Stars": [
    "STAR_00001",
    "STAR_00002",
    "STAR_00003"
  ]
}
```

### Planets

Representation: Time-dependent Points

```json
{
  "FeatureType": "Planet",
  "PlanetId": "PLANET_00001",
  "Name": "Mars",
  "OrbitalElements": {},
  "Attributes": {}
}
```

### Moons

Representation: Orbital Bodies

```json
{
  "FeatureType": "Moon",
  "MoonId": "MOON_00001",
  "Name": "Moon",
  "OrbitalElements": {},
  "Attributes": {}
}
```

---

## Celestial Coordinate System

Primary Coordinates:

- Right Ascension
- Declination

Optional Coordinates:

- Azimuth
- Altitude
- Ecliptic Longitude
- Ecliptic Latitude

---

## Future Dataset Classification

| Dataset | Representation |
|----------|---------------|
| Stars | Points |
| Constellations | Connected Points |
| Planets | Time-dependent Points |
| Moons | Time-dependent Points |
| Comets | Time-dependent Splines |
| Celestial Events | Points + Time |
| Deep Sky Objects | Points |
