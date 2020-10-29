

$serverReady = 0;
$clientReady = 0;

Write-Host "Building docker containers... This may take a couple of minutes.";

try {
	docker-compose --project-name "lactalis" up -d;

	Write-Host "Docker containers are up and running";
	Write-Host "Installing, running migrations and starting application";

	do {
		Start-Sleep 1;
		$serverStatus = docker inspect --format='{{.State.Health.Status}}' lactalis_server_1

		if ($serverStatus -eq "unhealthy") {
			Write-Host "Server failed to start.";
			exit 1;
		} elseif ($ServerStatus -eq "healthy" -and $serverReady -eq 0) {
			Write-Host "Server is ready";
			$serverReady=1;
			Write-Host "Waiting on client...";
		}

		$clientStatus = docker inspect --format='{{.State.Health.Status}}' lactalis_client_1;

		if ($clientStatus -eq "unhealthy") {
			Write-Host "Client failed to start.";
			exit 1;
		} elseif($clientStatus -eq "healthy" -and $clientReady -eq 0) {
			Write-Host "Client is ready;";
			$clientReady=1;
			Write-Host "Waiting on server...";
		}
	} Until((docker-compose --project-name "lactalis" ps | Select-String 'Up \(healthy\)' | Measure-Object -Line).Lines -gt 1)

	Write-Host "=================================";
	Write-Host "Application started successfully!";
	Write-Host "---------------------------------";
	Write-Host "Access your application at http://localhost:8000";
	Write-Host "=================================";
} catch {
	Write-Host "Exception Message: $($_.Exception.Message)" -ForegroundColor Red
}
finally {
	Read-Host "Press enter to close window..."
}