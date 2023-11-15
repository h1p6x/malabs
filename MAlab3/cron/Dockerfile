# Используем базовый образ с установленным PostgreSQL и cron
FROM postgres:latest

# Устанавливаем необходимые пакеты
RUN apt-get update && apt-get -y install cron

# Копируем скрипт в контейнер
COPY scripts/backup_script.sh /scripts/backup_script.sh

# Устанавливаем права на выполнение скрипта
RUN chmod +x /scripts/backup_script.sh

# Добавляем крон-задачу для запуска скрипта каждые 5 часов
COPY crontab /etc/cron.d/backup-cron
RUN chmod 0644 /etc/cron.d/backup-cron
RUN crontab /etc/cron.d/backup-cron

# Запускаем PostgreSQL и cron
CMD ["cron", "-f"]