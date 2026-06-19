CREATE SCHEMA IF NOT EXISTS aether;

SET search_path TO aether;

-- =====================================================
-- APPLICATION METADATA
-- =====================================================

CREATE TABLE app_metadata
(
    key             TEXT PRIMARY KEY,
    value           TEXT
);

-- =====================================================
-- DIMENSIONS
-- =====================================================

CREATE TABLE dim_movies
(
    tmdb_id             INT PRIMARY KEY,

    title               TEXT NOT NULL,
    original_title      TEXT,

    release_date        DATE,

    poster_path         TEXT,
    backdrop_path       TEXT,

    created_at          TIMESTAMP DEFAULT NOW(),
    updated_at          TIMESTAMP DEFAULT NOW()
);

CREATE TABLE genres
(
    genre_id            INT PRIMARY KEY,
    name                TEXT NOT NULL
);

-- =====================================================
-- BRIDGES
-- =====================================================

CREATE TABLE bridge_movie_genres
(
    tmdb_id             INT NOT NULL,
    genre_id            INT NOT NULL,

    PRIMARY KEY (tmdb_id, genre_id),

    CONSTRAINT fk_bridge_movie_genres_movie
        FOREIGN KEY (tmdb_id)
        REFERENCES dim_movies (tmdb_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_bridge_movie_genres_genre
        FOREIGN KEY (genre_id)
        REFERENCES genres (genre_id)
        ON DELETE CASCADE
);

-- =====================================================
-- FACTS
-- =====================================================

CREATE TABLE fact_movie_details
(
    tmdb_id             INT PRIMARY KEY,

    overview            TEXT,

    popularity          NUMERIC(12,4),

    vote_average        NUMERIC(4,2),
    vote_count          INT,

    runtime             INT,

    last_synced         TIMESTAMP,

    CONSTRAINT fk_fact_movie_details_movie
        FOREIGN KEY (tmdb_id)
        REFERENCES dim_movies (tmdb_id)
        ON DELETE CASCADE
);

CREATE TABLE fact_movie_discovery
(
    tmdb_id             INT PRIMARY KEY,

    source              TEXT NOT NULL,

    discovered_at       TIMESTAMP DEFAULT NOW(),

    CONSTRAINT fk_fact_movie_discovery_movie
        FOREIGN KEY (tmdb_id)
        REFERENCES dim_movies (tmdb_id)
        ON DELETE CASCADE
);

CREATE TABLE fact_popularity_snapshots
(
    snapshot_id         BIGSERIAL PRIMARY KEY,

    tmdb_id             INT NOT NULL,

    source              TEXT NOT NULL,

    snapshot_date       DATE NOT NULL,

    page_number         INT NOT NULL,

    rank_position       INT NOT NULL,

    popularity          NUMERIC(12,4),

    CONSTRAINT fk_fact_popularity_snapshot_movie
        FOREIGN KEY (tmdb_id)
        REFERENCES dim_movies (tmdb_id)
        ON DELETE CASCADE
);

-- =====================================================
-- INDEXES
-- =====================================================

CREATE INDEX idx_dim_movies_title
    ON dim_movies(title);

CREATE INDEX idx_dim_movies_release_date
    ON dim_movies(release_date DESC);

CREATE INDEX idx_fact_movie_details_popularity
    ON fact_movie_details(popularity DESC);

CREATE INDEX idx_fact_movie_details_vote_average
    ON fact_movie_details(vote_average DESC);

CREATE INDEX idx_fact_popularity_snapshots_movie
    ON fact_popularity_snapshots(tmdb_id);

CREATE INDEX idx_fact_popularity_snapshots_date
    ON fact_popularity_snapshots(snapshot_date DESC);

CREATE INDEX idx_fact_popularity_snapshots_source
    ON fact_popularity_snapshots(source);