@echo off

rem discover id and name of container running the agent master

FOR /F "tokens=* USEBACKQ" %%F IN (
`docker image inspect sitespeedmanager/master:dev --format "{{.Id}}"`
) DO (
SET imageid=%%F
)

echo %imageid%

FOR /F "tokens=* USEBACKQ" %%F IN (
`docker ps --filter "ancestor=%imageid%" --format {{.Names}}`
) DO (
SET ssmmastername=%%F
)

echo Using '%ssmmastername%' container name

rem start all required containers

cd dockercompose\
docker-compose -p ssmwebui up

rem drop all containers used for debugging
docker-compose -p ssmwebui rm