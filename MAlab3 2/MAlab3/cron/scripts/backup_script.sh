#!/bin/bash

# Устанавливаем переменные для подключения к базе данных
export PGPASSWORD=admin
DB_USER=admin
DB_NAME=core
DATE=$(date +"%Y%m%d%H%M%S")
BACKUP_FILENAME="/scripts/backup_$DATE.sql"

# Создаем резервную копию базы данных
pg_dump -h postgres -U $DB_USER -d $DB_NAME -f $BACKUP_FILENAME

# Удаление старых резервных копий
find /scripts -type f -name 'backup_*.sql' -mtime +5 -exec rm {} \;
