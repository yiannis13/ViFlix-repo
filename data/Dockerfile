FROM microsoft/mssql-server-linux:2017-GA

RUN mkdir /app
WORKDIR /app

COPY . .

RUN chmod +x docker-entrypoint.sh
ENTRYPOINT ["sh", "docker-entrypoint.sh"]