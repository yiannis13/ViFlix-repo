#!/bin/sh
set -e

echo "The Dockerfile ENTRYPOINT started. . ."

/opt/mssql-tools/bin/sqlcmd -U sa -P Passw0rd! -S viflix_sqlserver -i ./data/migration.sql

echo "The Dockerfile ENTRYPOINT is executing. . ."

exec "$@"