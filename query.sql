SELECT
    d.name AS "Подразделение",
    w.name AS "Название скважины",
    m.timestamp::date AS "Дата замера",
    mt.name AS "Тип замера",
    MIN(m.value) AS "Минимальное значение замера",
    MAX(m.value) AS "Максимальное значение замера",
    COUNT(m.id) AS "Количество замеров"
FROM
    measurements m
JOIN
    wells w ON m.well_id = w.id
JOIN
    divisions d ON w.division_id = d.id
JOIN
    measurement_types mt ON m.type_id = mt.id
GROUP BY
    d.name,
    w.name,
    m.timestamp::date,
    mt.name
ORDER BY
    d.name,
    w.name,
    "Дата замера";
