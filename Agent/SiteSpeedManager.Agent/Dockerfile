FROM georgevella/sitespeed_dotnet:dotnet-dev-1.0.4
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "ssmagent.dll"]
