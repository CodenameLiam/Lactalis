
#!/bin/bash
cd -- "$(dirname "$0")"

serverReady=0;
clientReady=0;

set -e
echo "Building docker containers... This may take a couple of minutes.";
docker-compose --project-name "lactalis" up -d;

echo "Docker containers are up and running";
echo "Installing, running migrations and starting application";

until [ $(docker-compose --project-name "lactalis" ps | grep ' Up (healthy)' | wc -l) -gt 1 ]
do
	sleep 1;

	if [ "$(docker inspect --format='{{.State.Health.Status}}' lactalis_server_1)" = "unhealthy" ]; then
		echo "Server failed to start.";
		exit 1;
	elif [ "$(docker inspect --format='{{.State.Health.Status}}' lactalis_server_1)" = "healthy" ] && [ $serverReady -eq 0 ]; then
		echo "Server is ready";
		echo "Access available at http://localhost:8080";
		serverReady=1;
		echo "Waiting on client...";
	fi

	if [ "$(docker inspect --format='{{.State.Health.Status}}' lactalis_client_1)" = "unhealthy" ]; then
		echo "Client failed to start.";
		exit 1;
	elif [ "$(docker inspect --format='{{.State.Health.Status}}' lactalis_client_1)" = "healthy" ] && [ $clientReady -eq 0 ]; then
		echo "Client is ready;";
		echo "Access available at http://localhost:8000 or http://localhost:4200";
		clientReady=1;
		echo "Waiting on server...";
	fi
done

echo "=================================";
echo "Application started successfully!";
echo "---------------------------------";
echo "Access your application at http://localhost:8000";
echo "=================================";
