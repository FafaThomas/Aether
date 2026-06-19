# Aether

Aether is a modern Windows-based home theater platform designed to unify streaming sources, local media libraries, AI-powered metadata generation, and high-fidelity surround sound playback into a single experience.

The project began with a simple goal:

**Preserve proper 5.1 surround sound playback through SPDIF and external audio decoders while delivering a modern streaming experience.**

---

## Vision

Modern streaming platforms provide convenience but often sacrifice user control, media ownership, audio flexibility, and long-term accessibility.

Aether aims to solve this by creating a personal media ecosystem that combines:

* Modern streaming UI
* Modular content providers
* Local media support
* AI-assisted organization
* Home theater audio
* Self-hosted infrastructure

The end goal is a platform that feels like Netflix while providing the flexibility of a self-managed media center.

---

## Core Principles

### Modular Content Sources

Aether does not depend on a single content source.

Content providers can be added, removed, or replaced without changing the player itself.

Examples:

* VidKing
* Local Media Libraries
* NAS Servers
* Future Streaming APIs
* Self-Hosted Sources

The player should never care where content originates.

---

### Home Theater First

Audio playback is the highest priority of the project.

Aether is being designed around support for:

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

### AI-Powered Metadata

Future versions of Aether will include AI-powered content analysis capable of generating:

* Movie summaries
* Episode summaries
* Thumbnails
* Metadata
* Categories
* Tags
* Watch recommendations

The AI layer will operate independently from playback and can be expanded through modular services.

---

## Architecture

```text
Aether
│
├── Aether.UI
│
├── Aether.Player
│
├── Aether.Core
│
├── Aether.Providers
│   │
│   ├── VidKing
│   ├── LocalLibrary
│   ├── NAS
│   └── Future Providers
│
├── Aether.Metadata
│
└── Aether.Audio
```

---

## Content Flow

```text
Content Provider
        │
        ▼
Metadata Layer
        │
        ▼
Player Engine
        │
        ▼
Audio Engine
        │
        ▼
SPDIF / HDMI
        │
        ▼
External Decoder
        │
        ▼
5.1 / 7.1 Surround System
```

---

## Planned Features

### Media Library

* Local library scanning
* NAS integration
* Automatic library updates
* Poster management
* Collection management

### Streaming

* Modular provider architecture
* Embedded streaming sources
* Remote streaming support
* Self-hosted media servers

### Playback

* Hardware accelerated video playback
* Subtitle support
* Resume playback
* Watch history
* Fullscreen theater mode

### Audio

* SPDIF passthrough
* DTS passthrough
* Dolby passthrough
* Multi-channel audio support
* External decoder integration

### AI

* Automatic metadata generation
* Thumbnail generation
* Scene detection
* Episode identification
* Personalized recommendations

---

## Technology Stack

### Frontend

* C#
* .NET 8
* WinUI 3

### Backend Services

* ASP.NET Core
* PostgreSQL

### AI Services

* Python
* Pydantic
* Local LLMs
* Future Agent Frameworks

### Playback

* MPV (planned)
* WebView2 (early development)

---

## Development Roadmap

### Phase 1

Foundation

* WinUI application
* Embedded player
* Provider architecture

### Phase 2

Media Library

* Local content management
* Metadata storage
* Poster support

### Phase 3

AI Metadata

* Automated metadata generation
* Thumbnail extraction
* Content analysis

### Phase 4

Home Streaming

* NAS support
* Self-hosted infrastructure
* Remote access

### Phase 5

Home Theater Optimization

* Advanced audio routing
* DTS passthrough
* Dolby passthrough
* Dedicated surround sound support

---

## Why Aether Exists

Aether exists because modern media consumption has become fragmented across multiple platforms, while advanced home theater audio support continues to receive less attention.

The project aims to restore user control over media, preserve high-quality surround sound playback, and provide a unified platform for both local and streamed content.

At its core, Aether is a home theater platform built around a simple idea:

**Your content. Your audio. Your system.**
