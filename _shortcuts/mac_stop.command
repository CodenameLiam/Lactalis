
#!/bin/bash
cd -- "$(dirname "$0")"

set -e
echo "Stopping the application...";
docker-compose --project-name "lactalis" down;

until [ $(docker-compose --project-name "lactalis" ps | wc -l) -lt 3 ]
do
	sleep 1;
done

echo "=================================";
echo "Application stopped successfully!";
echo "=================================";
