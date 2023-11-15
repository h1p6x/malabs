-- Создание таблицы "usrs"
CREATE TABLE usrs (
    id serial PRIMARY KEY,
    username VARCHAR (50),
    email VARCHAR (100)
);

-- Вставка тестовых данных
INSERT INTO usrs (username, email) VALUES
('user1', 'user1@example.com'),
('user2', 'user2@example.com');