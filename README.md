# Aether

Aether is a modern Windows-based media platform designed to unify streaming sources, metadata aggregation, local media libraries, AI-powered content services, and high-fidelity home theater playback into a single experience.

The project began with a simple goal:

**Preserve proper surround sound playback while delivering a modern streaming experience without sacrificing user control.**

Unlike traditional streaming platforms, Aether is designed around modular content providers, local ownership, metadata warehousing, and future home theater optimization.

---

# Current Status

Aether is currently in active development and already includes a fully functional end-to-end movie discovery and playback workflow.

Current implementation includes:

* WinUI 3 desktop application
* PostgreSQL metadata warehouse
* TMDB synchronization engine
* Daily metadata refresh pipeline
* Movie catalog browsing
* Poster and backdrop support
* Movie details pages
* Embedded streaming playback
* VidKing provider integration
* Historical popularity tracking
* Genre synchronization
* Movie detail enrichment

Current user workflow:

```text
Home Page
    ↓
Movie Catalog
    ↓
Movie Details
    ↓
Play
    ↓
Embedded Playback
```

The project has successfully moved beyond prototype stage and now operates as a functional movie catalog and streaming platform.

---

# Vision

Modern streaming services provide convenience but often sacrifice:

* User control
* Media ownership
* Audio flexibility
* Hardware compatibility
* Long-term accessibility

Aether aims to solve these problems by combining:

* Modern streaming UI
* Modular content providers
* Local media support
* AI-assisted organization
* Metadata warehousing
* Home theater audio
* Self-hosted infrastructure

The long-term goal is a platform that feels like Netflix while providing the flexibility of a self-managed media center.

---

# Core Principles

## Modular Content Sources

Aether does not depend on a single content source.

Content providers can be added, removed, or replaced without changing the rest of the platform.

Examples:

* VidKing
* Local Media Libraries
* NAS Servers
* Future Streaming APIs
* Self-Hosted Sources

The player should never care where content originates.

---

## Metadata First

Aether maintains a centralized metadata warehouse that separates content discovery from content playback.

Benefits include:

* Faster UI loading
* Reduced API usage
* Historical tracking
* Recommendation systems
* AI enrichment
* Provider independence

Metadata should be owned by the platform rather than tied to any specific provider.

---

## Home Theater First

Audio playback remains a major focus of the project.

Planned support includes:

* SPDIF / Optical Audio
* HDMI Audio Passthrough
* DTS
* Dolby Digital
* Dolby Digital Plus
* PCM
* 5.1 Surround Sound
* 7.1 Surround Sound

Whenever possible, audio streams should be passed directly to supported hardware decoders without unnecessary transcoding.

---

## AI-Powered Metadata

Future versions of Aether will include AI-powered content analysis capable of generating:

* Movie summaries
* Episode summaries
* Metadata
* Categories
* Tags
* Thumbnails
* Watch recommendations

The AI layer will operate independently from playback and can be expanded through modular services.

---

# Current Architecture

```text
Aether
│
├── Aether.UI
│   ├── Home Page
│   ├── Movie Details Page
│   └── Player Page
│
├── Aether.Core
│   ├── Models
│   ├── Services
│   └── Workers
│
├── Aether.Sync
│   └── Console Sync Runner
│
├── PostgreSQL
│
└── TMDB
```

---

# Metadata Pipeline

```text
TMDB
 │
 ├── Genres
 │
 ├── Popular Movies
 │
 └── Movie Details
         │
         ▼
Aether Sync Engine
         │
         ▼
PostgreSQL Warehouse
         │
         ▼
Aether UI
```

---

# Playback Flow

```text
TMDB Metadata
        │
        ▼
PostgreSQL
        │
        ▼
Movie Catalog
        │
        ▼
Movie Details
        │
        ▼
VidKing Provider
        │
        ▼
WebView2 Player
```

---

# Database Architecture

Aether uses PostgreSQL as a centralized metadata warehouse.

## Dimensions

### dim_movies

Stores the permanent movie catalog.

### genres

Stores TMDB genre definitions.

---

## Facts

### fact_movie_discovery

Tracks how movies entered the platform.

### fact_movie_details

Stores detailed metadata including:

* Overview
* Runtime
* Popularity
* Ratings
* Vote counts

### fact_popularity_snapshots

Stores historical popularity rankings.

---

## Bridge Tables

### bridge_movie_genres

Maintains movie-to-genre relationships.

---

## System Tables

### sync_state

Tracks synchronization status and refresh schedules.

---

# Features

## Implemented

### Metadata

* TMDB synchronization
* Genre synchronization
* Movie detail enrichment
* Historical popularity tracking
* Daily sync management

### Catalog

* Popular movie browsing
* Poster support
* Backdrop support
* Movie details pages

### Streaming

* VidKing integration
* Embedded playback
* WebView2 player

### Infrastructure

* Dockerized PostgreSQL
* Dapper
* Npgsql
* Modular synchronization engine

---

## Planned

### Media Library

* Local library scanning
* NAS integration
* Collection management
* Automatic updates

### Streaming

* Multiple provider support
* Self-hosted sources
* Remote streaming

### Playback

* MPV integration
* Native video rendering
* Subtitle support
* Resume playback
* Watch history

### Home Theater

* SPDIF passthrough
* DTS passthrough
* Dolby passthrough
* External decoder integration

### AI

* Metadata generation
* Thumbnail generation
* Scene detection
* Personalized recommendations

---

# Technology Stack

## Desktop Application

* C#
* .NET 9
* WinUI 3

## Database

* PostgreSQL 17
* Docker

## Metadata Services

* TMDB API
* Dapper
* Npgsql

## Streaming

* WebView2
* VidKing

## Future Playback

* MPV
* Native Audio Passthrough

## AI Services

* Python
* FastAPI
* Local LLMs
* Recommendation Systems

---

# Development Roadmap

## Phase 1 — Foundation

✅ Complete

* WinUI application
* Core architecture
* Navigation
* Player integration

## Phase 2 — Metadata Warehouse

✅ Complete

* PostgreSQL warehouse
* TMDB synchronization
* Genre mapping
* Historical popularity tracking
* Detail enrichment

## Phase 3 — Streaming Platform UI

🚧 In Progress

* Movie details pages
* Streaming workflow
* Catalog improvements
* Search

## Phase 4 — Local Media Libraries

⬜ Planned

* Local media support
* NAS integration
* Collection management

## Phase 5 — AI Metadata Services

⬜ Planned

* Metadata generation
* Recommendations
* Content analysis

## Phase 6 — Native Playback Engine

⬜ Planned

* MPV integration
* Native playback controls
* Subtitle system

## Phase 7 — Home Theater Optimization

⬜ Planned

* SPDIF passthrough
* DTS passthrough
* Dolby passthrough
* Surround sound optimization

---

# Why Aether Exists

Modern media consumption has become fragmented across multiple platforms while advanced home theater audio support continues to receive less attention.

Aether exists to restore user control over media, preserve high-quality playback, and provide a unified platform for both streamed and locally owned content.

At its core, Aether is built around a simple idea:

**Your Content. Your Audio. Your System.**
