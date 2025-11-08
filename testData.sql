-- Вставка типов замеров
INSERT INTO measurement_types (name) VALUES
('Забойное давление'),
('Пластовое давление'),
('Давление на приеме насоса');

-- Вставка подразделений
INSERT INTO divisions (name)
SELECT 'Подразделение ' || i
FROM generate_series(1, 3) i;

-- Вставка скважин
INSERT INTO wells (name, commissioning_date, division_id)
SELECT
    'Скважина ' || d.id || '-' || w,
    CURRENT_DATE - (floor(random() * 365) + 1)::int,
    d.id
FROM divisions d, generate_series(1, 5) w;

-- Вставка замеров
INSERT INTO measurements (well_id, type_id, value, timestamp)
SELECT
    w.id,
    t.id,
    random() * 1000,
    gs.day + (floor(random() * 86400) * interval '1 second')
FROM
    wells w,
    measurement_types t,
    generate_series(
        CURRENT_DATE - interval '4 days',
        CURRENT_DATE,
        '1 day'
    ) AS gs(day),
    generate_series(1, 100);

