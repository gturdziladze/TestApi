CREATE DATABASE IF NOT EXISTS testdb;

USE testdb;

CREATE TABLE IF NOT EXISTS Forecast (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Date datetime,
    TemperatureC int
);