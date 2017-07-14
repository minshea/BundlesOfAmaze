FROM microsoft/dotnet:1.1-runtime
ARG source
ENV NETCORE_ENVIRONMENT development
WORKDIR /app
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "BundlesOfAmaze.dll"]
