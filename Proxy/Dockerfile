FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /app
EXPOSE 80
COPY ./Publish .
ENTRYPOINT ["dotnet", "Proxy.dll"]