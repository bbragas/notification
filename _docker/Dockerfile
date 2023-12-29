FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS builder
WORKDIR /app

RUN apk update && apk add --no-cache make tzdata
ENV USER=appuser
ENV UID=10001

# See https://stackoverflow.com/a/55757473/12429735
RUN adduser \
    --disabled-password \
    --gecos "" \
    --home "/nonexistent" \
    --shell "/sbin/nologin" \
    --no-create-home \
    --uid "${UID}" \
    "${USER}"

# Copy csproj and restore as distinct layers
COPY . .
RUN make publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app

COPY --from=builder /usr/share/zoneinfo /usr/share/zoneinfo
COPY --from=builder /etc/passwd /etc/passwd
COPY --from=builder /etc/group /etc/group

COPY --from=builder /app/src/Notification.Api/publish/ .

USER appuser:appuser

EXPOSE 80
ENTRYPOINT ["dotnet", "Notification.Api.dll"]
