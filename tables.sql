-- Таблица подразделений
CREATE TABLE divisions (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE
);

-- Таблица скважин
CREATE TABLE wells (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    commissioning_date DATE NOT NULL,
    division_id INT NOT NULL REFERENCES divisions(id) ON DELETE CASCADE
);

-- Таблица типов замеров
CREATE TABLE measurement_types (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE
);

-- Таблица хранения замеров
CREATE TABLE measurements (
    id BIGSERIAL PRIMARY KEY,
    well_id INT NOT NULL REFERENCES wells(id) ON DELETE CASCADE,
    type_id INT NOT NULL REFERENCES measurement_types(id) ON DELETE CASCADE,
    value DECIMAL(10, 4) NOT NULL,
    timestamp TIMESTAMP WITHOUT TIME ZONE NOT NULL
);

-- Индексы для ускорения запросов
CREATE INDEX idx_measurements_well_id_timestamp ON measurements (well_id, timestamp);
CREATE INDEX idx_measurements_type_id ON measurements (type_id);
